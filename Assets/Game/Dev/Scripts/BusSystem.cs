using System;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public static class BusSystem
    {
        //Economy
        public static Action <int> OnAddMoneys;
        public static void CallAddMoneys(int amount) { OnAddMoneys?.Invoke(amount); }
        
        public static Action OnResetMoneys;
        public static void CallResetMoneys() { OnResetMoneys?.Invoke(); }
        
        public static Action OnSetMoneys;
        public static void CallSetMoneys() { OnSetMoneys?.Invoke(); }
        
        //Camera

        public static Action <CameraType , float> OnChangeCameraType;
        public static void CallChangeCameraType(CameraType playerCameraType , float duration) { OnChangeCameraType?.Invoke(playerCameraType , duration); }
        
        public static Action <CameraType , Transform> OnSetCameraFollow;
        public static void CallSetCameraFollow(CameraType playerCameraType , Transform hitPerson) { OnSetCameraFollow?.Invoke(playerCameraType , hitPerson); }
        
        //Game Manager
        
        public static Action OnLevelStart;
        public static void CallLevelStart() { OnLevelStart?.Invoke(); }
     
        public static Action <bool> OnLevelEnd;
        public static void CallLevelEnd(bool win) { OnLevelEnd?.Invoke(win); }
     
        public static Action OnLevelLoad;
        public static void CallLevelLoad() { OnLevelLoad?.Invoke(); }
        
        //Input
        
        public static Action OnMouseClickDown;
        public static void CallMouseClickDown() { OnMouseClickDown?.Invoke(); }
        
        public static Action OnMouseClicking;
        public static void CallMouseClicking() { OnMouseClicking?.Invoke(); }
        
        public static Action OnMouseClickUp;
        public static void CallMouseClickUp() { OnMouseClickUp?.Invoke(); }
        
        
        //Player
        
        public static Action OnPlayerDead;
        public static void CallPlayerDead() { OnPlayerDead?.Invoke(); }
        
        public static Action OnJumpPlayer;
        public static void CallJumpPlayer() { OnJumpPlayer?.Invoke(); }
        
        public static Action OnResetJumpAmount;
        public static void CallResetJumpAmount() { OnResetJumpAmount?.Invoke(); }
        
        public static Action <Transform , float> OnPlayerMove;
        public static void CallPlayerMove(Transform movePos , float duration = 0) { OnPlayerMove?.Invoke(movePos , duration); }
        
        public static Action OnSetPlayerSkin;
        public static void CallSetPlayerSkin() { OnSetPlayerSkin?.Invoke(); }

        
        //Finish
        
        public static Action <Transform> OnSetLevelProgressPlayerPos;
        public static void CallSetLevelProgressPlayerPos(Transform player) { OnSetLevelProgressPlayerPos?.Invoke(player); }
        
        public static Action <Transform> OnSetLevelProgressFinishPos;
        public static void CallSetLevelProgressFinishPos(Transform finish) { OnSetLevelProgressFinishPos?.Invoke(finish); }
        
        //Shop
        
        public static Action OnChangeShopPanelPage;
        public static void CallChangeShopPanelPage() { OnChangeShopPanelPage?.Invoke(); }
    }
}