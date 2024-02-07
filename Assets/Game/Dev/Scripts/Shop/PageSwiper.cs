using System;
using DG.Tweening;
using Game.Dev.Scripts.ScriptableSO;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Dev.Scripts.Shop
{
    public class PageSwiper : MonoBehaviour , IEndDragHandler
    {
        public ShopOptions shopOptions;
        public RectTransform levelPagesRect;
        private int currentPage = 1;

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
        }
    }
}