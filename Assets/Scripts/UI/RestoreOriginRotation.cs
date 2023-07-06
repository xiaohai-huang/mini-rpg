using UnityEngine;
using UnityEngine.Animations;

public class RestoreOriginRotation : MonoBehaviour
{
    void Start()
    {
        var origin = new GameObject("origin");
        var constraint = gameObject.AddComponent<RotationConstraint>();

        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = origin.transform;
        source.weight = 1;
        constraint.AddSource(source);

        constraint.constraintActive = true;
    }
}
