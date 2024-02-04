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
        
        public static Action <int,int> OnSetMoneys;
        public static void CallSetMoneys(int oldAmount,int newAmount) { OnSetMoneys?.Invoke(oldAmount,newAmount); }
        
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
    }
}