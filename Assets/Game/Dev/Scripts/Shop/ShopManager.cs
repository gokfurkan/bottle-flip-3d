using System;
using System.Collections.Generic;
using Game.Dev.Scripts.ScriptableSO;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dev.Scripts.Shop
{
    public class ShopManager : MonoBehaviour
    {
        public ShopOptions shopOptions;
        public PageSwiper pageSwiper;
        public GameObject skinButton;
        
        [Space(10)]
        public List<ShopRarityOptions> rarityOptions;
        
        [Space(10)]
        public Text unlockFeeText;
        public GameObject exclamationMark;

        [Space(10)] 
        public List<ShopButton> shopButtons;
        
        private void Start()
        {
            InitShop();
        }

        private void InitShop()
        {
            CreateButtons();
        }

        private void CreateButtons()
        {
            for (int i = 0; i < rarityOptions.Count; i++)
            {
                for (int j = 0; j < rarityOptions[i].buttonAmount; j++)
                {
                    var createdSkin = Instantiate(skinButton, rarityOptions[i].rarityHolder);
                    createdSkin.GetComponent<ShopButton>().buttonOptions.skinRarity = rarityOptions[i].skinRarity;
                }
            }

            // Debug.Log("All shop buttons created");
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