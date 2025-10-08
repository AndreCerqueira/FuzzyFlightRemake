using System.Collections;
using MoreMountains.Feedbacks;
using Unity.Cinemachine;
using UnityEngine;

namespace Project.Runtime.Scripts.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _camera;
        
        [SerializeField] private Transform _cameraGameTarget;
        
        [SerializeField] private MMF_Player _startFeedback;
        
        private void Start()
        {
            StartCoroutine(SetCameraTargetAfterDelay());
        }
        
        private IEnumerator SetCameraTargetAfterDelay()
        {
            yield return new WaitForSeconds(2f);
            if (_camera != null && _cameraGameTarget != null)
                _camera.Follow = _cameraGameTarget;
            
            yield return new WaitForSeconds(0.2f);
            _startFeedback?.PlayFeedbacks();
        }
    }
}
