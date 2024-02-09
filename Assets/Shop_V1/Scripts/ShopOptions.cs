using DG.Tweening;
using UnityEngine;

namespace Shop_V1.Scripts
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