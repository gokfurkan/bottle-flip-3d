using System;
using Game.Dev.Scripts.ScriptableSO;
using UnityEngine;

namespace Game.Dev.Scripts.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        public PlayerOptions playerOptions;
        public Rigidbody rb;
        
        private bool isJumping;
        private float jumpStartTime;
        private int remainingJump;
        
        private Vector3 jumpTarget;
        private Vector3 initialPosition;
        
        private void OnEnable()
        {
            BusSystem.OnJumpPlayer += JumpPlayer;
            BusSystem.OnMouseClickDown += JumpPlayer;
            BusSystem.OnResetJumpAmount += ResetJumpAmount;
        }

        private void OnDisable()
        {
            BusSystem.OnJumpPlayer -= JumpPlayer;
            BusSystem.OnMouseClickDown -= JumpPlayer;
            BusSystem.OnResetJumpAmount -= ResetJumpAmount;
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
        
        private void JumpPlayer()
        {
            if (GameManager.Instance.isLevelEnd) return;
            
            if (remainingJump > 0)
            {
                remainingJump--;
                
                StartArcJump();
            } 
        }
        
        private void StartArcJump()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            
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
            
            //set pos
            var newZPosition =  transform.position.z;
            var newPosition = new Vector3(targetPosition.x, targetPosition.y + verticalOffset, newZPosition);
            
            transform.position = newPosition;
            
            
            //set rot
            var rotationAngle = playerOptions.playerJumpOptions.rotateAmountPerJump * normalizedTime;
            
            transform.eulerAngles = new Vector3(rotationAngle, 90f, 0f);
            
            //
            
            if (normalizedTime >= 1.0f)
            {
                isJumping = false;
                
                transform.eulerAngles = new Vector3(0, 90f, 0f);
            }
        }

        private void ResetJumpAmount()
        {
            remainingJump = playerOptions.playerJumpOptions.totalJumpAmount;
        }
    }
}