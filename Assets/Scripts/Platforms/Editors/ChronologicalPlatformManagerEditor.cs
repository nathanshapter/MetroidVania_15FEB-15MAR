using UnityEditor;

[CustomEditor(typeof(ChronologicalPlatformManager))]

public class ChronologicalPlatformManagerEditor : Editor
{
    SerializedProperty spawnAsTimer;
    SerializedProperty timeBetweenPlatformSpawn;
    SerializedProperty timeInBetweenWaves;
    SerializedProperty isCrumbleTimed;
    SerializedProperty timeUntilCrumble;

    bool spawnAsTimerEnabled = false;
    bool isCrumbleTimedEnabled = false;

    private void OnEnable()
    {
        spawnAsTimer = serializedObject.FindProperty("spawnAsTimer");
        timeBetweenPlatformSpawn = serializedObject.FindProperty("timeBetweenPlatformSpawn");
        timeInBetweenWaves = serializedObject.FindProperty("timeInBetweenWaves");
        isCrumbleTimed = serializedObject.FindProperty("isCrumbleTimed");
        timeUntilCrumble = serializedObject.FindProperty("timeUntilCrumble");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Options to change for the Chronological platform Manager");
       

        serializedObject.Update();
        spawnAsTimerEnabled = EditorGUILayout.BeginFoldoutHeaderGroup(spawnAsTimerEnabled, "Spawn As Timer");
       
        if (spawnAsTimerEnabled)
        {
            EditorGUILayout.Space(20);
            EditorGUILayout.PropertyField(spawnAsTimer);
            EditorGUILayout.PropertyField(timeBetweenPlatformSpawn);
            EditorGUILayout.PropertyField(timeInBetweenWaves);
            EditorGUILayout.Space(20);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        isCrumbleTimedEnabled = EditorGUILayout.BeginFoldoutHeaderGroup(isCrumbleTimedEnabled, "Crumble Timer");
        
        if (isCrumbleTimedEnabled)
        {
            EditorGUILayout.Space(20);
            EditorGUILayout.PropertyField(isCrumbleTimed);
            EditorGUILayout.PropertyField(timeUntilCrumble);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        serializedObject.ApplyModifiedProperties();
    }
}
