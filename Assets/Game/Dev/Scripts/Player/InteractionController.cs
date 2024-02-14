using System;
using Game.Dev.Scripts.Interfaces;
using UnityEngine;

namespace Game.Dev.Scripts.Player
{
    public class InteractionController : MonoBehaviour
    {
        public ParticleSystem interactionSmoke;

        private void OnCollisionEnter(Collision collision)
        {
            if (GameManager.instance.isLevelEnd) return;
            
            var interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
            
            BusSystem.CallResetJumpAmount();
            interactionSmoke.Play();
        }
    }
}