using System;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public Transform playerStartPos;

        private void Start()
        {
            BusSystem.CallPlayerMove(playerStartPos);
        }
    }
}