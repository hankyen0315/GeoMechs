#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;



//[CustomPropertyDrawer(typeof(EventObjectContainer))]
public class EventObjectContainerEditor : PropertyDrawer
{
    private SerializedProperty eventTypeProperty;
    //private SerializedProperty constructorParametersProperty;
    private int a;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        //EditorGUI.indentLevel = 0;

        // Calculate rects
        Rect eventTypeRect = new Rect(position.x, position.y, position.width - 100f, position.height);
        Rect constructorParamsRect = new Rect(position.x, position.y+ EditorGUIUtility.singleLineHeight, 100f, EditorGUIUtility.singleLineHeight);
        // Find the 'eventObject' and 'constructorParameters' properties
        SerializedProperty eventTypeNameProperty = property.FindPropertyRelative("EventType");

        // Display the event type dropdown
        EditorGUI.PropertyField(eventTypeRect, eventTypeNameProperty, GUIContent.none);
        string eventTypeName = eventTypeNameProperty.stringValue;
        DisplayConstructorParameters(eventTypeName, constructorParamsRect);
        //Debug.Log(eventTypeName);
        EditorGUI.EndProperty();
    }

    //private Type GetSelectedEventType()
    //{
    //    return Type.GetType(eventTypeProperty.stringValue);
    //}


    private void DisplayConstructorParameters(string eventTypeName, Rect paramRect)
    {
        // Display dropdown for selecting the IGameEvent type

        EditorGUI.indentLevel++;

        //EditorGUILayout.PropertyField(constructorParametersProperty);

        // Display additional fields based on the selected IGameEvent type
        // You might need to customize this based on your specific needs

        switch (eventTypeName)
        {
            case "UnlockPartEvent":
                Debug.Log("unlock part");
                EditorGUI.IntField(paramRect, "Parameter A", a);
                break;
            default:
                break;
        }

        EditorGUI.indentLevel--;
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight;

        //// Check if the IGameEvent type is selected, then add extra height for constructor parameters
        //System.Type eventType = GetSelectedEventType(property.FindPropertyRelative("eventObject"));
        //if (eventType != null)
        //{
        //    height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("constructorParameters"));
        //}

        return height*2;
    }
    private void DisplayGameEventAParameters()
    {
        // Display additional fields specific to GameEventA
        // For example:
        // EditorGUILayout.IntField("Parameter A", gameEventAParameter);
    }

    private void DisplayGameEventBParameters()
    {
        // Display additional fields specific to GameEventB
        // For example:
        // EditorGUILayout.FloatField("Parameter B", gameEventBParameter);
    }
}
#endif
