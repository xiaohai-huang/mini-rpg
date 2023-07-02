using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GenericListener<>), true)]
public class GenericListenerEditor : Editor
{
    private SerializedProperty targetProperty;
    private SerializedProperty eventNameProperty;
    private SerializedProperty responseProperty;

    private void OnEnable()
    {
        targetProperty = serializedObject.FindProperty("Target");
        eventNameProperty = serializedObject.FindProperty("EventName");
        responseProperty = serializedObject.FindProperty("Response");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(targetProperty);
        EventPicker();
        EditorGUILayout.PropertyField(responseProperty);



        serializedObject.ApplyModifiedProperties();
    }

    private void EventPicker()
    {
        // Get the available event names from the Target object
        var target = targetProperty.objectReferenceValue as MonoBehaviour;
        if (target != null)
        {
            var eventNames = GetEventNames(target);
            if (eventNames.Length > 0)
            {
                int selectedIndex = Mathf.Max(0, System.Array.IndexOf(eventNames, eventNameProperty.stringValue));
                selectedIndex = EditorGUILayout.Popup("Event Name", selectedIndex, eventNames);

                if (selectedIndex >= 0 && selectedIndex < eventNames.Length)
                {
                    eventNameProperty.stringValue = eventNames[selectedIndex];
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No events found on the Target object.", MessageType.Warning);
            }
        }

    }

    private string[] GetEventNames(MonoBehaviour target)
    {
        var targetType = target.GetType();
        var eventNames = new List<string>();

        var fields = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var field in fields)
        {
            if (field.FieldType == GetEventType())
            {
                eventNames.Add(field.Name);
            }
        }

        return eventNames.ToArray();
    }

    private Type GetEventType()
    {
        var listener = serializedObject.targetObject.GetType();
        var eventType = listener.GetField("Response").FieldType;
        return eventType;
    }

}
