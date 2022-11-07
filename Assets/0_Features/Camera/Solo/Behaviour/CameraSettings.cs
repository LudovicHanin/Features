using _0_Features.Utils.Attributes.ShowIf;
using UnityEngine;

namespace _0_Features.Camera.Solo.Behaviour
{
    [CreateAssetMenu(menuName = "Camera/Settings", fileName = "CameraSettings")]
    public class CameraSettings : ScriptableObject
    {
        #region F/P

        #region Serialize

        [SerializeField] private CameraType _cameraType = CameraType.TPS; 

        [SerializeField] private Transform _target = null;

        [SerializeField, ShowIf("_canFollowTarget")]
        private Vector3 _offsetTargetPosition = Vector3.zero;

        [SerializeField, ShowIf("_canLookAtTarget")]
        private Vector3 _offsetTargetLookAt = Vector3.zero;

        [SerializeField, ShowIf("_canFollowTarget"), Range(0, 200)]
        private float _cameraMovementSpeed = 100;

        [SerializeField, ShowIf("_canLookAtTarget"), Range(0, 500)]
        private float _cameraLookAtSpeed = 200;

        [SerializeField] private bool _canFollowTarget = true;
        [SerializeField] private bool _canLookAtTarget = true;

        #endregion

        #region Public

        public Transform Target => _target;
        public Vector3 OffsetTargetPosition => _offsetTargetPosition;
        public Vector3 OffsetTargetLookAt => _offsetTargetLookAt;

        public Vector3 TargetOffsetPosition => (Target ? Target.position : Vector3.zero) + OffsetTargetPosition;
        public Vector3 TargetOffsetLookAt => (Target ? Target.position : Vector3.zero) + OffsetTargetLookAt;

        public float CameraMovementSpeed => _cameraMovementSpeed;
        public float CameraLookAtSpeed => _cameraLookAtSpeed;

        public bool CanFollowTarget
        {
            get => _canFollowTarget;
            set => _canFollowTarget = value;
        }

        public bool CanLookAtTarget
        {
            get => _canLookAtTarget;
            set => _canLookAtTarget = value;
        }

        #endregion

        #endregion


        public void SetTarget(Transform target) => _target = target;
    }
}