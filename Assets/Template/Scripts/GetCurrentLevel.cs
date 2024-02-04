using Game.Dev.Scripts;
using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class GetCurrentLevel : MonoBehaviour
    {
        [SerializeField] private LevelTextType textType;
        
        private TextMeshProUGUI text;

        private void Start()
        {
            InitLevelTextType();
        }

        private void InitLevelTextType()
        {
            text = GetComponent<TextMeshProUGUI>();
            
            SettingsData settingsData = InitializeManager.instance.settingsData;
            
            string levelNumber = " " + (SaveManager.instance.saveData.GetLevel() + (textType != LevelTextType.LevelCompleted ? 1 : 0));
            switch (textType)
            {
                case LevelTextType.Level:
                    text.text = settingsData.levelText + levelNumber;
                    break;
                case LevelTextType.LevelCompleted:
                    text.text = settingsData.levelText + " " + settingsData.levelCompletedText;
                    break;
                case LevelTextType.LevelFailed:
                    text.text = settingsData.levelText + " " + settingsData.levelFailedText;
                    break;
                default:
                    break;
            }
        }
    }
}