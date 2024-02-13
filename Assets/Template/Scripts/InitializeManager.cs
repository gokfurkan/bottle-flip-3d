using System;
using UnityEngine;

namespace Template.Scripts
{
    public class InitializeManager : PersistentSingleton<InitializeManager>
    {
        public SettingsData settingsData;

        protected override void Initialize()
        {
            base.Initialize();

            SetFrameRate();
            
            AddOperation(typeof(Development));
            AddOperation(typeof(HapticManager));
        }
        
        private void AddOperation(Type type)
        {
            gameObject.AddComponent(type);
        }
        
        private void SetFrameRate()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = settingsData.targetFPS;
        }
    }
}
