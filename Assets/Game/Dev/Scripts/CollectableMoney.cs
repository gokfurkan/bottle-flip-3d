using Game.Dev.Scripts.Interfaces;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class CollectableMoney : MonoBehaviour , IInteractable
    {
        public int incomeAmount;
        
        public void Interact()
        {
            BusSystem.CallAddMoneys(incomeAmount);
        }
    }
}