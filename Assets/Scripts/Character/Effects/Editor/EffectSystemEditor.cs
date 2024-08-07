using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EffectSystem))]
public class EffectSystemEditor : Editor
{
    private EffectSystem _system;

    private void OnEnable() { }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);
        _system = (EffectSystem)target;
        var effects = _system.Effects;
        for (int i = 0; i < effects.Length; i++)
        {
            EditorGUILayout.LabelField($"{effects[i].Name}\t {effects[i].CoolDownTime}ms");
        }
        // for (int i = 0; i < _effectsProperty.arraySize; i++)
        // {
        //     SerializedProperty effectElement = _effectsProperty.GetArrayElementAtIndex(i);
        //     EditorGUILayout.PropertyField(effectElement);
        // }

        // serializedObject.ApplyModifiedProperties();
    }
}
