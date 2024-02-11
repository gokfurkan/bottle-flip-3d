using System;
using System.Collections.Generic;
using Game.Dev.Scripts;
using Template.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop_V1.Scripts
{
    public class ShopManager : MonoBehaviour
    {
        public ShopOptions shopOptions;
        public PageSwiper pageSwiper;
        public GameObject skinButton;
        
        [Space(10)]
        public TextMeshProUGUI costText;
        public GameObject exclamationMark;
        
        [Space(10)]
        public List<ShopRarityOptions> rarityOptions;
        public List<ShopButton> shopButtons;

        private void OnEnable()
        {
            BusSystem.OnSetMoneys += CallSetExclamationMarkEnabledDelayed;
        }

        private void OnDisable()
        {
            BusSystem.OnSetMoneys -= CallSetExclamationMarkEnabledDelayed;
        }

        private void Awake()
        {
            exclamationMark.SetActive(false);
        }

        private void Start()
        {
            InitShop();
        }

        private void InitShop()
        {
            InitializeShopButtons();
            InitializeSkinUnlockStatus();
        }

        private void InitializeShopButtons()
        {
            // Loop through each rarity option
            for (int rarityIndex = 0; rarityIndex < rarityOptions.Count; rarityIndex++)
            {
                // Loop through each button amount
                for (int buttonIndex = 0; buttonIndex < rarityOptions[rarityIndex].buttonAmount; buttonIndex++)
                {
                    // Instantiate a new button and add it
                    GameObject newSkinButtonObject = Instantiate(skinButton, rarityOptions[rarityIndex].rarityHolder);
                    ShopButton newSkinButtonComponent = newSkinButtonObject.GetComponent<ShopButton>();
                    newSkinButtonComponent.buttonOptions.skinRarity = rarityOptions[rarityIndex].skinRarity;
                    shopButtons.Add(newSkinButtonComponent);
                }
            }

            // A message indicating all shop buttons have been created could be shown when applicable
            // Debug.Log("All shop buttons created");
        }
        
        private void CallSetExclamationMarkEnabledDelayed()
        {
            Invoke(nameof(SetExclamationMarkEnabled), shopOptions.markControlDelay);
        }
        
        private void SetExclamationMarkEnabled()
        {
            int money = SaveManager.instance.saveData.GetMoneys();

            int[] pageEndAmounts = new int[rarityOptions.Count];

            // Calculate the end amounts for each rarity option
            for (int i = 0; i < rarityOptions.Count; i++)
            {
                if (i == 0)
                {
                    pageEndAmounts[i] = rarityOptions[i].buttonAmount;
                }
                else
                {
                    pageEndAmounts[i] = pageEndAmounts[i - 1] + rarityOptions[i].buttonAmount;
                }
            }

            bool[] pageFullBuyStatuses = new bool[pageEndAmounts.Length];

            // Check if each page is fully bought
            for (int i = 0; i < pageEndAmounts.Length; i++)
            {
                int startAmount = (i == 0) ? 0 : pageEndAmounts[i - 1];
                int endAmount = pageEndAmounts[i];
                pageFullBuyStatuses[i] = CheckPageFullBuy(SaveManager.instance.saveData.skinsUnlockStatus, startAmount, endAmount);
            }

            int[] rarityCosts = new int[rarityOptions.Count];

            // Get the cost of each rarity option
            for (int i = 0; i < rarityOptions.Count; i++)
            {
                rarityCosts[i] = rarityOptions[i].rarityCost;
            }

            // Determine if the exclamation mark should be shown
            bool shouldShowExclamationMark = ShouldShowExclamationMark();
            exclamationMark.SetActive(shouldShowExclamationMark);

            // Function to check if the exclamation mark should be shown
            bool ShouldShowExclamationMark()
            {
                for (int i = 0; i < rarityCosts.Length; i++)
                {
                    if (!pageFullBuyStatuses[i] && money >= rarityCosts[i])
                    {
                        return true;
                    }
                }

                return false;
            }

            // Function to check if a page is fully bought
            bool CheckPageFullBuy(IReadOnlyList<bool> skinsUnlockStatus, int startIndex, int endIndex)
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
    }
    
    [Serializable]
    public class ShopButtonOptions
    {
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
        public RectTransform rarityHolder;
        public SkinRarity skinRarity;
        public int buttonAmount;
        public int rarityCost;
    }
}