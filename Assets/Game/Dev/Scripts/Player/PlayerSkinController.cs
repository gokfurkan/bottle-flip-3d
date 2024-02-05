using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template.Scripts;
using UnityEngine;

namespace Game.Dev.Scripts.Player
{
    public class PlayerSkinController : MonoBehaviour
    {
        [Header("UnBroken")]
        public GameObject skinsParent;
        public List<GameObject> skins;
        
        [Header("Broken")]
        public GameObject brokenPartsParent;
        public List<Material> brokenMaterials;
        [ReadOnly] public List<MeshRenderer> brokenPartMeshes;
        
        private void OnEnable()
        {
            BusSystem.OnPlayerDead += OnPlayerDead;
            BusSystem.OnSetPlayerSkin += SetPlayerSkin;
        }

        private void OnDisable()
        {
            BusSystem.OnPlayerDead -= OnPlayerDead;
            BusSystem.OnSetPlayerSkin -= SetPlayerSkin;
        }

        private void Start()
        {
            InitSkin();
        }

        private void OnPlayerDead()
        {
            skinsParent.SetActive(false);
            brokenPartsParent.SetActive(true);
        }

        private void SetPlayerSkin()
        {
            SetUnBrokenSkin();
            SetBrokenSkin();
        }

        private void SetUnBrokenSkin()
        {
            skins.ActivateAtIndex(SaveManager.instance.saveData.currentSkin);
        }

        private void SetBrokenSkin()
        {
            for (var i = 0; i < brokenPartMeshes.Count; i++)
            {
                brokenPartMeshes[i].material = brokenMaterials[SaveManager.instance.saveData.currentSkin];
            }
        }
        
        private void InitSkin()
        {
            brokenPartsParent.SetActive(false);
            
            brokenPartMeshes.Clear();
            foreach (var mesh in brokenPartsParent.GetComponentsInChildren<MeshRenderer>(true))
            {
                brokenPartMeshes.Add(mesh);       
            }
            
            SetPlayerSkin();
        }
    }
}