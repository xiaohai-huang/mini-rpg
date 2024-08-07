using UnityEngine;
using UnityEngine.Animations;

public class RestoreOriginRotation : MonoBehaviour
{
    private static GameObject _origin;

    void Awake()
    {
        if (_origin == null)
        {
            _origin = new GameObject("origin");
        }
        var constraint = gameObject.AddComponent<RotationConstraint>();

        ConstraintSource source = new ConstraintSource
        {
            sourceTransform = _origin.transform,
            weight = 1
        };
        constraint.AddSource(source);
        constraint.constraintActive = true;
    }
}
