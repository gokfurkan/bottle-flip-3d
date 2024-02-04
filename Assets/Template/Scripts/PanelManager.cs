using System;
using System.Collections.Generic;
using System.Linq;
using Game.Dev.Scripts;
using UnityEngine;

namespace Template.Scripts
{
    public class PanelManager : Singleton<PanelManager>
    {
        private List<PanelTypeHolder> allPanels = new List<PanelTypeHolder>();

        protected override void Initialize()
        {
            base.Initialize();
            
            InitializePanelSystem();
        }

        private void OnEnable()
        {
            BusSystem.OnLevelStart += ActivateGamePanel;
            BusSystem.OnLevelEnd += ActivateEndPanel;
        }

        private void OnDisable()
        {
            BusSystem.OnLevelStart -= ActivateGamePanel;
            BusSystem.OnLevelEnd -= ActivateEndPanel;
        }

        private void InitializePanelSystem()
        {
            GetAllPanels();
            ActivateMenuPanel();
        }

        private void ActivateMenuPanel()
        {
            DisableAll();
            
            Activate(PanelType.Money);
            Activate(PanelType.Level);
            Activate(PanelType.OpenSettings);
            Activate(PanelType.Restart);
        }

        private void ActivateGamePanel()
        {
            Activate(PanelType.LevelProgress);
        }

        private void ActivateEndPanel(bool win)
        {
            DisableAll();
            
            Activate(PanelType.Money);
            
            if (win)
            {
                Activate(PanelType.Win);
            }
            else
            {
                Activate(PanelType.Lose);
            }
        }

        public void ActivateSettingsPanel()
        {
            Activate(PanelType.OpenSettings , false);
            Activate(PanelType.Settings);
        }

        public void DeActivateSettingsPanel()
        {
            Activate(PanelType.Settings , false);
            Activate(PanelType.OpenSettings);
        }

        public void ActivateDevPanel()
        {
            
        }

        public void LoadLevel()
        {
            BusSystem.CallLevelLoad();
        }
        
        public void Activate(PanelType panelType, bool activate = true)
        {
            PanelTypeHolder panel = FindPanel(panelType);

            if (panel != null)
            {
                panel.gameObject.SetActive(activate);
            }
            else
            {
                Debug.LogWarning("Panel not found: " + panelType.ToString());
            }
        }
        
        public void DisableAll()
        {
            foreach (var panel in allPanels)
            {
                panel.gameObject.SetActive(false);
            }
        }
        
        private PanelTypeHolder FindPanel(PanelType panelType)
        {
            return allPanels.Find(panel => panel.panelType == panelType);
        }
        
        private void GetAllPanels()
        {
            allPanels = transform.root.GetComponentsInChildren<PanelTypeHolder>(true).ToList();
        }
    }
}