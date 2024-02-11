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
        public List<GameObject> pagePoints;
        public int currentPage = 1;

        private void Start()
        {
            currentPage = 1;
            MovePage();
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
            
            BusSystem.CallChangeShopPanelPage();
        }
    }
}