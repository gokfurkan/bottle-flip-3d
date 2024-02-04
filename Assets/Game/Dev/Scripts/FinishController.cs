using System;
using Game.Dev.Scripts.Interfaces;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class FinishController : MonoBehaviour , IInteractable
    {
        private void Start()
        {
            BusSystem.CallSetLevelProgressFinishPos(transform);
        }

        public void Interact()
        {
            BusSystem.CallLevelEnd(true);
        }
    }
}