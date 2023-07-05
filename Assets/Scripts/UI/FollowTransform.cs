using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0, 0.04f, 0);

    void LateUpdate()
    {
        transform.position = Target.transform.position + Offset;
    }
}
