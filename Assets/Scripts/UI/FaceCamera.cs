using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public enum Mode
    {
        LookAtCamera,
        LookAtCameraInverted,
        CameraForward,
        CameraForwardInverted
    }

    public Mode LookAtMode;

    // Update is called once per frame
    void LateUpdate()
    {
        switch (LookAtMode)
        {
            case Mode.LookAtCamera:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtCameraInverted:
                transform.rotation = Quaternion.LookRotation(
                    transform.position - Camera.main.transform.position
                );
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
