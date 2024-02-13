using DG.Tweening;
using UnityEngine;

namespace Game.Dev.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private void OnEnable()
        {
            BusSystem.OnPlayerMove += PlayerMove;
            BusSystem.OnPlayerDead += PlayerDead;
        }

        private void OnDisable()
        {
            BusSystem.OnPlayerMove -= PlayerMove;
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

        private void PlayerMove(Transform movePos , float duration = 0)
        {
            transform.DOMove(movePos.position, duration);
        }
    }
}