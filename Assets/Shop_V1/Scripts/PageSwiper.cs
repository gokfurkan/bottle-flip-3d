using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Dev.Scripts;
using Template.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shop_V1.Scripts
{
    public class PageSwiper : MonoBehaviour , IEndDragHandler
    {
        public ShopOptions shopOptions;
        public RectTransform levelPagesRect;
        public GameObject previousPageChanger;
        public GameObject nextPageChanger;
        public int currentPage = 1;
        public List<GameObject> pagePoints;

        private void Start()
        {
            SetPageForSelectedSkin();
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if (MathF.Abs(eventData.position.x - eventData.pressPosition.x) > shopOptions.dragThreshHold)
            {
                if (eventData.position.x > eventData.pressPosition.x)
                {
                    PreviousPage();
                }
                else
                {
                    NextPage();
                }
            }
            else
            {
                MovePage();
            }
        }
        
        public void NextPage()
        {
            if (currentPage < shopOptions.maxPage)
            {
                currentPage++;
                MovePage();
            }
        }

        public void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                MovePage();
            }
        }

        private void MovePage()
        {
            levelPagesRect.DOAnchorPosX((currentPage - 1) * shopOptions.pageStep, shopOptions.pageChangeSpeed)
                .SetEase(shopOptions.pageChangeEase);
            
            pagePoints.ActivateAtIndex(currentPage - 1);

            SetPageChangerEnabled();
            
            BusSystem.CallChangeShopPanelPage();
        }

        private void SetPageChangerEnabled()
        {
            previousPageChanger.SetActive(true);
            nextPageChanger.SetActive(true);
            
            switch (currentPage)
            {
                case 1:
                    previousPageChanger.SetActive(false);
                    break;
                case 3:
                    nextPageChanger.SetActive(false);
                    break;
            }
        }
        
        private void SetPageForSelectedSkin()
        {
            var currentSkin = SaveManager.instance.saveData.currentSkin;
            
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
            
            if (currentSkin < pageEndAmounts[0])
            {
                currentPage = 1;
            }
            else if (currentSkin >= pageEndAmounts[0] && currentSkin < pageEndAmounts[1])
            {
                currentPage = 2;
            }
            else
            {
                currentPage = 3;
            }

            MovePage();
        }
    }
}