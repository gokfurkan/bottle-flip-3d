using System;
using System.Collections.Generic;
using Game.Dev.Scripts;
using Template.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Shop_V1.Scripts
{
    public class ShopManager : Singleton<ShopManager>
    {
        public ShopOptions shopOptions;
        public PageSwiper pageSwiper;
        public GameObject skinButton;
        
        [Space(10)]
        public GameObject exclamationMark;
        public TextMeshProUGUI costText;
        
        [Space(10)]
        public List<RectTransform> rarityHolders;
        public List<ShopButton> shopButtons;

        private int totalCreatedButton;
        

        private void OnEnable()
        {
            BusSystem.OnSetMoneys += CallSetExclamationMarkEnabledDelayed;
            BusSystem.OnChangeShopPanelPage += OnChangeShopPage;
        }

        private void OnDisable()
        {
            BusSystem.OnSetMoneys -= CallSetExclamationMarkEnabledDelayed;
            BusSystem.OnChangeShopPanelPage -= OnChangeShopPage;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            exclamationMark.SetActive(false);
            InitShop();
        }

        private void InitShop()
        {
            InitializeShopButtons();
            InitializeSkinUnlockStatus();
            InitSetSkin(SaveManager.instance.saveData.currentSkin);
        }

        private void InitSetSkin(int selectedItem)
        {
            SetItemUnlockStatus();
            SetItemSelectStatus(selectedItem);
            
            BusSystem.CallSetPlayerSkin();
        }
        
        public void OnSetSkin(int buttonIndex)
        {
            SaveManager.instance.saveData.currentSkin = buttonIndex;
            SaveManager.instance.Save();

            SetItemUnlockStatus();
            SetItemSelectStatus(buttonIndex);
            
            BusSystem.CallSetPlayerSkin();
        }
        
        public void UnlockRandomSkin()
        {
            var currentRarity = shopOptions.rarityOptions[pageSwiper.currentPage - 1];
            var currentRarityCost = currentRarity.rarityCost;
            var buttonAmount = currentRarity.buttonAmount;

            if (SaveManager.instance.saveData.GetMoneys() < currentRarityCost) return;

            var startIndex = (pageSwiper.currentPage - 1) * buttonAmount;
            var endIndex = pageSwiper.currentPage * buttonAmount;

            var lockedItemIndices = FindLockedItems(startIndex, endIndex);

            if (lockedItemIndices.Count == 0)
            {
                Debug.Log("All items are already unlocked.");   
                return;
            }

            var randomLockedIndex = lockedItemIndices[Random.Range(0, lockedItemIndices.Count)];
            SaveManager.instance.saveData.skinsUnlockStatus[randomLockedIndex] = true;
            SaveManager.instance.Save();

            SetItemUnlockStatus();
            BusSystem.CallAddMoneys(-currentRarityCost);
        }
        
        private void OnChangeShopPage()
        {
            SetUnlockCostText();
            SetItemUnlockStatus();
        }
        
        private void SetUnlockCostText()
        {
            costText.text = shopOptions.rarityOptions[pageSwiper.currentPage - 1].rarityCost.ToString();
        }
        
        private void SetItemUnlockStatus()
        {
            for (var i = 0; i < shopButtons.Count; i++)
            {
                var isSkinUnlocked = SaveManager.instance.saveData.skinsUnlockStatus[i];
                shopButtons[i].ChangeButtonLockStatus(!isSkinUnlocked);
            }
        }

        private void SetItemSelectStatus(int selectedItem)
        {
            for (int i = 0; i < shopButtons.Count; i++)
            {
                shopButtons[i].ChangeButtonSelectStatus(selectedItem);
            }
        }
        
        private void InitializeShopButtons()
        {
            // Loop through each rarity option
            for (int rarityIndex = 0; rarityIndex < shopOptions.rarityOptions.Count; rarityIndex++)
            {
                // Loop through each button amount
                for (int buttonIndex = 0; buttonIndex < shopOptions.rarityOptions[rarityIndex].buttonAmount; buttonIndex++)
                {
                    // Instantiate a new button and add it
                    GameObject newSkinButtonObject = Instantiate(skinButton, rarityHolders[rarityIndex]);
                    ShopButton newSkinButtonComponent = newSkinButtonObject.GetComponent<ShopButton>();
                    
                    newSkinButtonComponent.buttonOptions.buttonIndex = totalCreatedButton;
                    newSkinButtonComponent.buttonOptions.skinRarity = shopOptions.rarityOptions[rarityIndex].skinRarity;
                    
                    shopButtons.Add(newSkinButtonComponent);
                    totalCreatedButton++;
                }
            }

            // A message indicating all shop buttons have been created could be shown when applicable
            // Debug.Log("All shop buttons created");
        }
        
        private void InitializeSkinUnlockStatus()
        {
            if (SaveManager.instance.saveData.firstSetUnlockStatus == 0)
            {
                // Clear existing skin unlock status
                SaveManager.instance.saveData.skinsUnlockStatus.Clear();

                // Initialize skin unlock status for each button
                for (int i = 0; i < shopButtons.Count; i++)
                {
                    SaveManager.instance.saveData.skinsUnlockStatus.Add(i == 0);
                }

                // Update first set unlock status
                SaveManager.instance.saveData.firstSetUnlockStatus = 1;

                // Save the updated data
                SaveManager.instance.Save();
            }
        }
        
        private void CallSetExclamationMarkEnabledDelayed()
        {
            Invoke(nameof(SetExclamationMarkEnabled), shopOptions.markControlDelay);
        }
        
        private void SetExclamationMarkEnabled()
        {
            // Check if the exclamation mark should be shown
            bool shouldShowExclamationMark = ShouldShowExclamationMark();
            exclamationMark.SetActive(shouldShowExclamationMark);
        }
        
        private List<int> FindLockedItems(int startIndex, int endIndex)
        {
            var lockedItems = new List<int>();

            for (var i = startIndex; i < endIndex; i++)
            {
                if (!SaveManager.instance.saveData.skinsUnlockStatus[i])
                {
                    lockedItems.Add(i);
                }
            }

            return lockedItems;
        }

        private int[] GetPageEndAmounts()
        {
            int[] pageEndAmounts = new int[shopOptions.rarityOptions.Count];

            // Calculate the end amounts for each rarity option
            for (int i = 0; i < shopOptions.rarityOptions.Count; i++)
            {
                if (i == 0)
                {
                    pageEndAmounts[i] = shopOptions.rarityOptions[i].buttonAmount;
                }
                else
                {
                    pageEndAmounts[i] = pageEndAmounts[i - 1] + shopOptions.rarityOptions[i].buttonAmount;
                }
            }

            return pageEndAmounts;
        }

        private bool[] GetPageFullBuyStatus()
        {
            int[] pageEndAmounts = GetPageEndAmounts();
            
            bool[] pageFullBuyStatuses = new bool[pageEndAmounts.Length];

            // Check if each page is fully bought
            for (int i = 0; i < pageEndAmounts.Length; i++)
            {
                int startAmount = (i == 0) ? 0 : pageEndAmounts[i - 1];
                int endAmount = pageEndAmounts[i];
                pageFullBuyStatuses[i] = CheckPageFullBuy(SaveManager.instance.saveData.skinsUnlockStatus, startAmount, endAmount);
            }

            return pageFullBuyStatuses;
        }

        private bool CheckPageFullBuy(IReadOnlyList<bool> skinsUnlockStatus, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                if (!skinsUnlockStatus[i])
                {
                    return false;
                }
            }
            return true;
        }

        private int[] GetRarityCosts()
        {
            int[] rarityCosts = new int[shopOptions.rarityOptions.Count];

            // Get the cost of each rarity option
            for (int i = 0; i < shopOptions.rarityOptions.Count; i++)
            {
                rarityCosts[i] = shopOptions.rarityOptions[i].rarityCost;
            }

            return rarityCosts;
        }
        
        private bool ShouldShowExclamationMark()
        {
            int money = SaveManager.instance.saveData.GetMoneys();
            
            int[] rarityCosts = GetRarityCosts();
            
            bool[] pageFullBuyStatuses = GetPageFullBuyStatus();
                
            for (int i = 0; i < rarityCosts.Length; i++)
            {
                if (!pageFullBuyStatuses[i] && money >= rarityCosts[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
    
    [Serializable]
    public class ShopButtonOptions
    {
        public int buttonIndex;
        public SkinRarity skinRarity;
        
        [Space(10)] 
        public Image skinIcon;
        public Image lockIcon;
        public Image buttonBg;

        [Space(10)]
        public Image darkOutline;
        public Image whiteOutline;
        public Image yellowOutline;
    }

    [Serializable]
    public class ShopRarityOptions
    {
        public SkinRarity skinRarity;
        public int buttonAmount;
        public int rarityCost;
        public Color activeButtonColor;
        public Color deActiveButtonColor;
    }
}