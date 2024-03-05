using Template.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dev.Scripts
{
    public class LevelProgressManager : MonoBehaviour
    {
        public Image levelProgressFill;
        public TextMeshProUGUI currentLevelText;
        public TextMeshProUGUI nextLevelText;

        private bool canFill;
        private float totalDistanceX;
        private Transform playerPos;
        private Transform finishPos;
       
        
        private void OnEnable()
        {
            BusSystem.OnSetLevelProgressPlayerPos += SetPlayerPos;
            BusSystem.OnSetLevelProgressFinishPos += SetFinishPos;
        }

        private void OnDisable()
        {
            BusSystem.OnSetLevelProgressPlayerPos -= SetPlayerPos;
            BusSystem.OnSetLevelProgressFinishPos -= SetFinishPos;
        }

        private void Start()
        {
            canFill = true;
            
            SetLevelTexts();
        }

        private void Update()
        {
            SetCurrentDistance();
        }
        
        private void SetCurrentDistance()
        {
            if (!GameManager.instance.isLevelPlaying) return;
            if (!canFill) return;
            
            float currentDistanceX = Mathf.Abs(playerPos.position.x - finishPos.position.x);
            float progressPercentageX = 1 - (currentDistanceX / totalDistanceX);
            levelProgressFill.fillAmount = progressPercentageX;
            if (levelProgressFill.fillAmount >= 0.995f)
            {
                canFill = false;
            }
        }
        
        private void SetPlayerPos(Transform player)
        {
            playerPos = player;
            SetTotalDistance();
        }

        private void SetFinishPos(Transform finish)
        {
            finishPos = finish;
            SetTotalDistance();
        }
        
        private void SetTotalDistance()
        {
            if (playerPos == null || finishPos == null) return;
            
            totalDistanceX = Mathf.Abs(playerPos.position.x - finishPos.position.x);
        }

        private void SetLevelTexts()
        {
            var level = SaveManager.instance.saveData.level;
            
            currentLevelText.text = (level + 1).ToString();
            nextLevelText.text = (level + 2).ToString();
        }
    }
}