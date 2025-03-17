//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(WaveCleaner))]
//public class EventManagerEditor : Editor
//{
//    private SerializedProperty eventsProperty;

//    private void OnEnable()
//    {
//        eventsProperty = serializedObject.FindProperty("events");
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();

//        EditorGUILayout.PropertyField(eventsProperty, true);

//        if (eventsProperty.isExpanded)
//        {
//            EditorGUI.indentLevel++;

//            for (int i = 0; i < eventsProperty.arraySize; i++)
//            {
//                SerializedProperty eventElement = eventsProperty.GetArrayElementAtIndex(i);
//                SerializedProperty eventObject = eventElement.FindPropertyRelative("eventObject");

//                EditorGUILayout.PropertyField(eventObject);

//                // Add additional fields for constructor parameters based on the selected event type

//                EditorGUI.indentLevel++;
//                // Additional fields for constructor parameters
//                EditorGUI.indentLevel--;

//                EditorGUILayout.Space();
//            }

//            EditorGUI.indentLevel--;
//        }

//        serializedObject.ApplyModifiedProperties();
//    }
//}
//#endif


