using System;
using UnityEngine;

namespace Game.Dev.Scripts.SO
{
    [CreateAssetMenu(fileName = "PlayerOptions", menuName = "ScriptableObjects/PlayerOptions")]
    public class PlayerOptions : ScriptableObject
    {
        public PlayerJumpOptions playerJumpOptions;
    }

    [Serializable]
    public class PlayerJumpOptions
    {
        public int totalJumpAmount;
        
        [Space(10)]
        public float jumpDistanceX;
        public float jumpDistanceY;
        
        [Space(10)]
        public float jumpHeight;
        public float jumpDuration;

        [Header("Rotate On Jump")] 
        public float rotateAmountPerJump;
    }
}