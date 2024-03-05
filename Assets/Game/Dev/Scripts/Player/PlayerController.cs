using DG.Tweening;
using UnityEngine;

namespace Game.Dev.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public TrailRenderer trail;
        
        private void OnEnable()
        {
            BusSystem.OnPlayerMove += PlayerMove;
            BusSystem.OnPlayerDead += PlayerDead;
            BusSystem.OnLevelStart += ActivateTrail;
        }

        private void OnDisable()
        {
            BusSystem.OnPlayerMove -= PlayerMove;
            BusSystem.OnPlayerDead -= PlayerDead;
            BusSystem.OnLevelStart -= ActivateTrail;
        }

        private void Start()
        {
            trail.enabled = false;
            BusSystem.CallSetLevelProgressPlayerPos(transform);
        }
        
        private void ActivateTrail()
        {
            trail.enabled = true;
        }
        
        private void PlayerDead()
        {
            trail.enabled = false;
            BusSystem.CallLevelEnd(false);
        }

        private void PlayerMove(Transform movePos , float duration = 0)
        {
            transform.DOMove(movePos.position, duration);
        }
    }
}