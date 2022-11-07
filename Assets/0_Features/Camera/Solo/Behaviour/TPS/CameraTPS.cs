using _0_Features.Camera.Solo.Behaviour;
using UnityEngine;

public class CameraTPS : CameraBehaviour
{
    protected override void FollowTarget(Vector3 targetPosition)
    {
        if (!IsValid) return;
        
        
    }

    protected override void LookAtTarget(Vector3 positionToLook)
    {
        if (!IsValid) return;

        transform.LookAt(_cameraSettings.TargetOffsetLookAt);
    }
}