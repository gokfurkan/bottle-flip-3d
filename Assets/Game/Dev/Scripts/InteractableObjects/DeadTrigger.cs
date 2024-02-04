using Game.Dev.Scripts.Interfaces;
using UnityEngine;

namespace Game.Dev.Scripts.InteractableObjects
{
    public class DeadTrigger : MonoBehaviour , IInteractable
    {
        public void Interact()
        {
            BusSystem.CallPlayerDead();
        }
    }
}