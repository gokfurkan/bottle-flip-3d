using Game.Dev.Scripts;
using UnityEngine;
using AudioType = Game.Dev.Scripts.AudioType;

namespace Template.Scripts
{
    public class Development : PersistentSingleton<Development>
    {
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Break();
            }
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                BusSystem.CallLevelEnd(true);
            }
            
            if (Input.GetKeyDown(KeyCode.H))
            {
                BusSystem.CallLevelEnd(false);
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                BusSystem.CallLevelLoad();
            }
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                AudioManager.instance.Play(AudioType.GameStart);
            }
        }
#endif
    }
}