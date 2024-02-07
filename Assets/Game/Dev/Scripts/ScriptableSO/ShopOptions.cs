using DG.Tweening;
using UnityEngine;

namespace Game.Dev.Scripts.ScriptableSO
{
    [CreateAssetMenu(fileName = "ShopOptions", menuName = "ScriptableObjects/ShopOptions")]
    public class ShopOptions : ScriptableObject
    {
        [Header("Page Swiper")]
        public int maxPage;
        public float pageStep;
        public float pageChangeSpeed;
        public Ease pageChangeEase;

        [Space(10)] 
        public float dragThreshHold;
    }
}