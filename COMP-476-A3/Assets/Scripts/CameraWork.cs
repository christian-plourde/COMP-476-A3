using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    [Tooltip("The distance in the local x-z plane to the target.")]
    [SerializeField]
    private float distance = 7.0f;

    [Tooltip("The height we want the camera to be above the target.")]
    [SerializeField]
    private float height = 3.0f;

    [Tooltip("The smooth time lag for the height of the camera.")]
    [SerializeField]
    private float heightSmoothLag = 0.3f;

    [Tooltip("Allow the camera to be offset vertically from the target.")]
    [SerializeField]
    private Vector3 centerOffset = Vector3.zero;

    [Tooltip("Set this as false if a component of a prefab is being instantiated by Photon Network.")]
    [SerializeField]
    private bool followOnStart = false;

    //cached transform of the target
    Transform cameraTransform;

    //internal flag to reconnect if target is lost or camera has switched
    bool isFollowing;

    //Represents current velocity. Modified by SmoothDamp() every time it is called.
    private float heightVelocity;

    //Represents position we are trying to reach using smooth damp
    private float targetHeight = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        if(followOnStart)
        {
            OnStartFollowing();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }

        if(isFollowing)
        {
            Apply();
        }
    }

    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;
        Cut();
    }

    private void Apply()
    {
        Vector3 targetCenter = transform.position;
        float originalTargetAngle = transform.eulerAngles.y;
        float currentAngle = cameraTransform.eulerAngles.y;
        float targetAngle = originalTargetAngle;
        currentAngle = targetAngle;
        targetHeight = targetCenter.y + height;

        float currentHeight = cameraTransform.position.y;
        currentHeight = Mathf.SmoothDamp(currentHeight, targetHeight, ref heightVelocity, heightSmoothLag);
        Quaternion currentRotation = Quaternion.Euler(0, currentAngle, 0);
        cameraTransform.position = targetCenter;
        cameraTransform.position += currentRotation * Vector3.back * distance;
        cameraTransform.position = new Vector3(cameraTransform.position.x, currentHeight, cameraTransform.position.z);
        SetUpRotation(targetCenter);
    }

    private void Cut()
    {
        float oldSmoothHeight = heightSmoothLag;
        heightSmoothLag = 0.001f;
        Apply();
        heightSmoothLag = oldSmoothHeight;
    }

    private void SetUpRotation(Vector3 centerPos)
    {
        Vector3 cameraPos = cameraTransform.position;
        Vector3 offsetToCenter = centerPos - cameraPos;

        Quaternion yRotation = Quaternion.LookRotation(new Vector3(offsetToCenter.x, 0, offsetToCenter.z));
        Vector3 relativeOffset = Vector3.forward * distance + Vector3.down * height;
        cameraTransform.rotation = yRotation * Quaternion.LookRotation(relativeOffset);
    }
}
