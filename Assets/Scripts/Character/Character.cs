using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [NonSerialized] public Vector3 Velocity;
    [NonSerialized] public bool AttackInput;
    [NonSerialized] public Vector2 HorizontalInput;
    [NonSerialized] public bool AbilityOneInput;
    [NonSerialized] public Vector2 AbilityOneDirection;
}