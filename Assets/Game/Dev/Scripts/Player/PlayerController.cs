﻿using System;
using DG.Tweening;
using Game.Dev.Scripts.SO;
using UnityEngine;

namespace Game.Dev.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerOptions playerOptions;
        
        //jump sequence
        private bool isJumping;
        private float jumpStartTime;
        
        private Vector3 jumpTarget;
        private Vector3 initialPosition;
        
        //jump amount
        public int remainingJump;
        public float timer;
        
        //rotate
        private Tween rotationTween;
        
        private void OnEnable()
        {
            BusSystem.OnJumpPlayer += JumpPlayer;
            BusSystem.OnMouseClickDown += JumpPlayer;
        }

        private void OnDisable()
        {
            BusSystem.OnJumpPlayer -= JumpPlayer;
            BusSystem.OnMouseClickDown -= JumpPlayer;
        }

        private void Start()
        {
            remainingJump = playerOptions.playerJumpOptions.totalJumpAmount;
        }

        private void Update()
        {
            if (isJumping)
            {
                PerformArcJump();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            remainingJump = playerOptions.playerJumpOptions.totalJumpAmount;
        }


        #region Jump Sequence

        private void JumpPlayer()
        {
            if (remainingJump > 0)
            {
                remainingJump--;
                
                StartArcJump();
            } 
        }
        
        private void StartArcJump()
        {
            var jumpOptions = playerOptions.playerJumpOptions;
            
            initialPosition = transform.position;
            jumpTarget = new Vector3(initialPosition.x + jumpOptions.jumpDistanceX,initialPosition.y + jumpOptions.jumpDistanceY, initialPosition.z);
            
            isJumping = true;
            jumpStartTime = Time.time;
        }

        private void PerformArcJump()
        {
            var timeSinceJumpStarted = Time.time - jumpStartTime;
            var normalizedTime = Mathf.Clamp01(timeSinceJumpStarted / playerOptions.playerJumpOptions.jumpDuration);
            var verticalOffset = Mathf.Sin(normalizedTime * Mathf.PI) * playerOptions.playerJumpOptions.jumpHeight;
            var targetPosition = Vector3.Lerp(initialPosition, jumpTarget, normalizedTime);
            var newZPosition =  transform.position.z;
            
            var newPosition = new Vector3(targetPosition.x, targetPosition.y + verticalOffset, newZPosition);
            transform.position = newPosition;
            
            if (normalizedTime >= 1.0f)
            {
                isJumping = false;
            }
        }

        #endregion
    }
}