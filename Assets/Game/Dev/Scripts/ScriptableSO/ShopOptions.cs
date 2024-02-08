using System.Collections.Generic;
using DG.Tweening;
using Game.Dev.Scripts.Shop;
using UnityEngine;

namespace Game.Dev.Scripts.ScriptableSO
{
    [CreateAssetMenu(fileName = "ShopOptions", menuName = "ScriptableObjects/ShopOptions")]
    public class ShopOptions : ScriptableObject
    {
        [Header("Shop")] 
       
        
        [Header("Page Swiper")]
        public int maxPage;
        public float pageStep;
        public float dragThreshHold;
        public float pageChangeSpeed;
        public Ease pageChangeEase;
        
        
        
    }
}