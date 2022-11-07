using _0_Features.Utils.Attributes.ShowIf;
using UnityEngine;
using UnityEngine.Events;

namespace _0_Features.Utils.Singleton
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static UnityAction OnInstanceReady;

        [SerializeField] private bool debug = true;
        [SerializeField, ShowIf("debug")] private Color validColor = Color.green;
        [SerializeField, ShowIf("debug")] private Color invalidColor = Color.red;
        [SerializeField, ShowIf("debug"), Range(-10, 10)] private float height = 1;
        [SerializeField, ShowIf("debug"), Range(0, 5)] private float debugSphereSize = 2;
    
        private static T instance = default(T);
        public static T Instance => instance;

        private bool IsValid => instance;

        protected virtual void Awake()
        {
            if (instance && instance != this)
            {
                Destroy(this);
                return;
            }

            instance = this as T;
            name += $"[{typeof(T).Name}]";
            OnInstanceReady?.Invoke();
        }

        protected void OnDrawGizmos()
        {
            if (!debug) return;
        
            Gizmos.color = IsValid ? validColor : invalidColor; 
        
            Vector3 position = transform.position;
            Vector3 debugPosition = position + Vector3.up * height;
        
            Gizmos.DrawSphere(debugPosition, debugSphereSize);
            Gizmos.DrawLine(position, debugPosition);
        
            Gizmos.color = Color.white;
        }
    }
}
