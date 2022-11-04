using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private bool debug = true;
    [SerializeField] private Color validColor = Color.green;
    [SerializeField] private Color invalidColor = Color.red;
    [SerializeField, Range(-10, 10)] private float height = 1;
    [SerializeField, Range(0, 5)] private float debugSphereSize = 2;
    
    private T instance = default(T);
    public T Instance => instance;

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
