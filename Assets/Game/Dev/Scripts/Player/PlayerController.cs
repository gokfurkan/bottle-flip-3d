using UnityEngine;

namespace Game.Dev.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private void OnEnable()
        {
            BusSystem.OnPlayerDead += PlayerDead;
        }

        private void OnDisable()
        {
            BusSystem.OnPlayerDead -= PlayerDead;
        }

        private void Start()
        {
            BusSystem.CallSetLevelProgressPlayerPos(transform);
        }
        
        private void PlayerDead()
        {
            BusSystem.CallLevelEnd(false);
        }
    }
}