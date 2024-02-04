using System.Collections.Generic;
using Game.Dev.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Template.Scripts
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "SettingsData", order = 0)]
    public class SettingsData : ScriptableObject
    {
        [Header("Load")]
        public SceneType nextSceneAfterLoad;
        public List<LoadFillOptions> loadFillOption;
        
        [Header("Game")]
        public int targetFPS;

        [Header("Save")] 
        public SaveData defaultSave;

        [Header("UI")] 
        public string levelText;
        public string levelCompletedText;
        public string levelFailedText;

        [Header("Economy")] 
        public bool useMoneyAnimation;
        [ShowIf(nameof(useMoneyAnimation))]
        public float moneyAnimationDuration;
    }
}