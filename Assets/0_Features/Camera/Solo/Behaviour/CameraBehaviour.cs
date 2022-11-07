using System;
using _0_Features.Camera.Solo.Manager;
using _0_Features.Utils.Attributes.ShowIf;
using _0_Features.Utils.IsValid;
using _0_Features.Utils.Manager.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace _0_Features.Camera.Solo.Behaviour
{
    public abstract class CameraBehaviour : MonoBehaviour, IHandlerItem<string>, IValid
    {
        #region Event

        public UnityAction OnEnableRequest;
        public UnityAction OnDisableRequest;

        public UnityAction OnStartFollowTargetRequest;
        public UnityAction OnStopFollowTargetRequest;

        public UnityAction OnStartLookAtTargetRequest;
        public UnityAction OnStopLookAtTargetRequest;

        #endregion

        #region F/P

        #region Serialize

        [SerializeField] protected UnityEngine.Camera _camera;
        
        [SerializeField] protected string id = "Camera";
        [SerializeField] protected CameraSettings _cameraSettings = null;

        [SerializeField] protected string _notValidMessage = "Is not valid";
        
        [SerializeField, ShowIf("_showDebug")] Color _targetOffsetPositionColor = Color.blue;
        [SerializeField, ShowIf("_showDebug")] private float _targetOffsetPositionRadius = 2;
        
        [SerializeField, ShowIf("_showDebug")] Color _targetOffsetLookAtColor = Color.cyan;
        [SerializeField, ShowIf("_showDebug")] private float _targetOffsetLookAtRadius = 2;

        [SerializeField] private bool _showDebug = true;

        [SerializeField] private bool _isDefaultMainCamera = false;
        #endregion

        #region Private

        #endregion

        #region Public
        public string ID => id;

        public string NotValidMessage => _notValidMessage;

        public bool IsDefaultMainCamera => _isDefaultMainCamera;
        public bool IsValid => _camera && _cameraSettings;

        #endregion

        #endregion

        #region Unity Methods

        private void Awake()
        {
            RegisterPrivateEvent();

        }

        protected void Start()
        {
            if (!_camera)
                _camera = GetComponent<UnityEngine.Camera>();
            
            if (!_isDefaultMainCamera)
                _camera.enabled = false;
            
            if (!CameraManager.Instance)
                CameraManager.OnInstanceReady += RegisterHandlerItem;
            else
                RegisterHandlerItem();
        }

        protected void Update()
        {
            FollowTarget(_cameraSettings.TargetOffsetPosition);
            LookAtTarget(_cameraSettings.TargetOffsetLookAt);
        }

        protected void OnDrawGizmos()
        {
            if (!IsValid || !_showDebug) return;
            
            Gizmos.color = _targetOffsetPositionColor;
            Gizmos.DrawSphere(_cameraSettings.TargetOffsetPosition, _targetOffsetPositionRadius);
            Vector3 position = transform.position;
            Gizmos.DrawLine(_cameraSettings.TargetOffsetPosition, position);
            Gizmos.color = _targetOffsetLookAtColor;
            Gizmos.DrawSphere(_cameraSettings.TargetOffsetLookAt, _targetOffsetLookAtRadius);
            Gizmos.DrawLine(_cameraSettings.TargetOffsetLookAt, position);
            Gizmos.color = Color.white;
        }

        private void OnDestroy()
        {
            UnregisterEvent();
            UnregisterHandlerItem();
        }

        #endregion

        #region Custom Methods

        #region Private

        private void RegisterPrivateEvent()
        {
            OnEnableRequest += Enable;
            OnDisableRequest += Disable;

            OnStartFollowTargetRequest += () => _cameraSettings.CanFollowTarget = true;
            OnStopFollowTargetRequest += () => _cameraSettings.CanFollowTarget = false;

            OnStartLookAtTargetRequest += () => _cameraSettings.CanLookAtTarget = true;
            OnStartLookAtTargetRequest += () => _cameraSettings.CanLookAtTarget = false;
        }

        private void UnregisterEvent()
        {
            OnEnableRequest -= Enable;
            OnDisableRequest -= Disable;
            
            OnStartFollowTargetRequest -= () => _cameraSettings.CanFollowTarget = true;
            OnStopFollowTargetRequest -= () => _cameraSettings.CanFollowTarget = false;

            OnStartLookAtTargetRequest -= () => _cameraSettings.CanLookAtTarget = true;
            OnStartLookAtTargetRequest -= () => _cameraSettings.CanLookAtTarget = false;

        }

        #endregion

        #region Public

        public void RegisterHandlerItem()
        {
            CameraManager.Instance.OnCameraRegisterRequest?.Invoke(this);
        }

        public void UnregisterHandlerItem()
        {
            CameraManager.Instance.OnCameraUnregisterRequest?.Invoke(this);
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }

        #endregion

        #region Protected

        protected abstract void FollowTarget(Vector3 targetPosition);

        protected abstract void LookAtTarget(Vector3 positionToLook);

        #endregion

        #endregion

  
        public void NotValidError()
        {
        }
    }
}