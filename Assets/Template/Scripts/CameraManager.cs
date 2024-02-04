using System.Collections.Generic;
using Cinemachine;
using Game.Dev.Scripts;
using UnityEngine;
using CameraType = Game.Dev.Scripts.CameraType;

namespace Template.Scripts
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineBrain cineMachineBrain;
        [SerializeField] private List<CinemachineVirtualCamera> virtualCameras;

        private void OnEnable()
        {
            BusSystem.OnChangeCameraType += ChangeCamera;
            BusSystem.OnSetCameraFollow += SetCameraFollowTarget;
        }

        private void OnDisable()
        {
            BusSystem.OnChangeCameraType -= ChangeCamera;
            BusSystem.OnSetCameraFollow -= SetCameraFollowTarget;
        }
        
        private void DeactivateAllCameras()
        {
            foreach (var virtualCamera in virtualCameras)
            {
                virtualCamera.gameObject.SetActive(false);
            }
        }

        private void ChangeCamera(CameraType playerCameraType , float duration)
        {
            DeactivateAllCameras();

            cineMachineBrain.m_DefaultBlend.m_Time = duration;
            virtualCameras[(int)playerCameraType].gameObject.SetActive(true);
        }

        private void SetCameraFollowTarget(CameraType playerCameraType, Transform followTarget)
        {
            virtualCameras[(int)playerCameraType].Follow = followTarget;
        }
    }
}