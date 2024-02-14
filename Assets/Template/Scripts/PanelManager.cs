using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Dev.Scripts;
using Game.Dev.Scripts.ScriptableSO;
using UnityEngine;

namespace Template.Scripts
{
    public class PanelManager : Singleton<PanelManager>
    {
        public GameOptions gameOptions;
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
            // Activate(PanelType.Level);
            Activate(PanelType.OpenSettings);
            Activate(PanelType.OpenShop);
            Activate(PanelType.Restart);
            Activate(PanelType.LevelProgress);
        }

        private void ActivateGamePanel()
        {
            Activate(PanelType.OpenShop , false);
            Activate(PanelType.LevelProgress);
        }

        private void ActivateEndPanel(bool win)
        {
            DisableAll();
            
            Activate(PanelType.Money);

            StartCoroutine(ActivateEndPanelDelay(win));
        }

        private IEnumerator ActivateEndPanelDelay(bool win)
        {
            if (win)
            {
                yield return new WaitForSeconds(InitializeManager.instance.settingsData.winPanelDelay);
                
                Activate(PanelType.Win);
                
                BusSystem.CallAddMoneys(gameOptions.winIncome);
                
                BusSystem.CallSpawnMoneys();
            }
            else
            {
                yield return new WaitForSeconds(InitializeManager.instance.settingsData.losePanelDelay);
                
                Activate(PanelType.Lose);
                
                BusSystem.CallAddMoneys(gameOptions.loseIncome);
            }
            
            yield return new WaitForSeconds(InitializeManager.instance.settingsData.endContinueDelay);
                
            Activate(PanelType.EndContinue);
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
        
        public void ActivateShopPanel()
        {
            Activate(PanelType.OpenShop , false);
            Activate(PanelType.Shop);
        }

        public void DeActivateShopPanel()
        {
            Activate(PanelType.Shop , false);
            Activate(PanelType.OpenShop);
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