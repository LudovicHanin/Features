using System.Collections.Generic;
using _0_Features.Camera.Solo.Behaviour;
using _0_Features.Utils.Const;
using _0_Features.Utils.IsValid;
using _0_Features.Utils.Manager.Interfaces;
using _0_Features.Utils.Singleton;
using UnityEngine;
using UnityEngine.Events;
using Debug = _0_Features.Utils.Extensions.Debug.Debug;

namespace _0_Features.Camera.Solo.Manager
{
    public class CameraManager : Singleton<CameraManager>, IHandler<string, CameraBehaviour>, IValid
    {
        #region Event

        public UnityAction<CameraBehaviour> OnCameraRegisterRequest;
        public UnityAction<CameraBehaviour> OnCameraUnregisterRequest;

        #endregion

        #region F/P

        #region Serialize

        [Header("Is Script Valid")] [SerializeField]
        private string notValidMessage = "CameraManager Is Not Valid";

        #endregion

        #region Private

        private readonly Dictionary<string, CameraBehaviour> _handlerItems = new Dictionary<string, CameraBehaviour>();
        private UnityEngine.Camera _mainCamera = null;
        #endregion

        #region Public

        public Dictionary<string, CameraBehaviour> HandlerItems => _handlerItems;

        public string NotValidMessage => notValidMessage;
        public bool IsValid => true;

        #endregion

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            RegisterPrivateEvent();
        }

        private void Start()
        {
            RegisterPublicEvent();
        }

        private void OnDestroy()
        {
            UnregisterEvent();
        }

        #endregion

        #region Custom Methods

        #region Private

        private void RegisterPrivateEvent()
        {
            OnCameraRegisterRequest += RegisterItem;
            OnCameraUnregisterRequest += UnregisterItem;
        }

        private void RegisterPublicEvent()
        {
            
        }

        private void UnregisterEvent()
        {
            OnCameraRegisterRequest -= RegisterItem;
            OnCameraUnregisterRequest -= UnregisterItem;
        }
        #endregion

        #region Public

        public CameraBehaviour Get(string id)
        {
            if (!Debug.Assert(Exist(id), $"{id} is not register !"))
                return null;

            return HandlerItems[id];
        }

        public string Get(CameraBehaviour item)
        {
            if (Debug.Assert(item || Exist(item), $"{(item ? item.ID + " is not register" : "Item doesn't exist!")}"))
                return null;

            return item.ID;
        }


        public bool Exist(string id)
        {
            if (HandlerItems.ContainsKey(id))
                return true;
            return false;
        }

        public bool Exist(CameraBehaviour item)
        {
            if (HandlerItems.ContainsKey(item.ID) || HandlerItems.ContainsValue(item))
                return true;
            return false;
        }


        public void RegisterItem(CameraBehaviour item)
        {
            if (!Debug.Assert(item && !Exist(item),
                    $"{GetType().Name} Try to register already register Item {(item != null ? item.ID : "")}",
                    Debug.DebugType.WARNING))
                return;

            Debug.Log($"Register {item.ID}", DebugColor.Success);

            _handlerItems.Add(item.ID, item);
        }

        public void UnregisterItem(CameraBehaviour item)
        {
            if (!Debug.Assert(item && Exist(item),
                    $"{GetType().Name} Try to unregister Item {(item != null ? item.ID : "")} when it's not register",
                    Debug.DebugType.WARNING))
                return;

            Debug.Log($"Unregister {item.ID}", DebugColor.Success);

            _handlerItems.Remove(item.ID);
        }

        public void UnregisterItem(string id)
        {
            if (!Debug.Assert(Exist(id), $"{GetType().Name} Try to unregister Item {id} when it's not register",
                    Debug.DebugType.WARNING))
                return;

            _handlerItems.Remove(id);
        }


        public void NotValidError()
        {
            UnityEngine.Debug.LogError(NotValidMessage);
        }

        #endregion

        #endregion
    }
}