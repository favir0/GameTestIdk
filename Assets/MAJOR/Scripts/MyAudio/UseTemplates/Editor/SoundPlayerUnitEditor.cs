using UnityEditor;

namespace MyAudio.UseTemplates.Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SoundPlayerUnit))] // Замените на ваш тип скрипта, например, CaliperController
    public class SoundPlayerUnitEditor : UnityEditor.Editor
    {
        private int _clipGroupIndex;
        private int _clipTypeIndex;
        private int _certainClipType;

        private const int CERTAIN_CLIP_TYPE_INDEX = 1;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            // General sound settings
            EditorGUILayout.PropertyField(serializedObject.FindProperty("soundType"));
            
            // Choose sound clip
            EditorGUILayout.PropertyField(serializedObject.FindProperty("clipGroup"));
            
            _clipGroupIndex = serializedObject.FindProperty("clipGroup").intValue;
            switch (_clipGroupIndex)
            {
                case 0:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("weightsActions"));
                    break;
                case 1:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("generalSounds"));
                    break;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            // Clip to use settings
            EditorGUILayout.PropertyField(serializedObject.FindProperty("clipToUse"));
            _clipTypeIndex = serializedObject.FindProperty("clipToUse").intValue;
            
            // If certain clip type used
            if (_clipTypeIndex == CERTAIN_CLIP_TYPE_INDEX)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("certainClipTypes"));
                _certainClipType = serializedObject.FindProperty("certainClipTypes").intValue;
                switch (_certainClipType)
                {
                    case 0:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("clipIndex"));
                        break;
                    case 1:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("clipIndexSeries"));
                        break;
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            // Local sound settings
            EditorGUILayout.PropertyField(serializedObject.FindProperty("volumeMultiplier"));
            
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}