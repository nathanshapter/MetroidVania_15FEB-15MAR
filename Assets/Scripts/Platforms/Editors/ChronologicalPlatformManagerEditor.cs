using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChronologicalPlatformManager))]

public class ChronologicalPlatformManagerEditor : Editor
{
    SerializedProperty spawnAsTimer;
    SerializedProperty timeBetweenPlatformSpawn;
    SerializedProperty timeInBetweenWaves;
    SerializedProperty isCrumbleTimed;
    SerializedProperty timeUntilCrumble;
    SerializedProperty platform;
    SerializedProperty startAllSpawned;
    SerializedProperty disablePlatformOnJump;
    SerializedProperty spawnAll;

    bool spawnAsTimerEnabled = false;
    bool isCrumbleTimedEnabled = false;
    bool allOptions = false;

    Rect rect;

    private void OnEnable()
    {
        spawnAsTimer = serializedObject.FindProperty("spawnAsTimer");
        timeBetweenPlatformSpawn = serializedObject.FindProperty("timeBetweenPlatformSpawn");
        timeInBetweenWaves = serializedObject.FindProperty("timeInBetweenWaves");
        isCrumbleTimed = serializedObject.FindProperty("isCrumbleTimed");
        timeUntilCrumble = serializedObject.FindProperty("timeUntilCrumble");
        platform = serializedObject.FindProperty("platform");
        startAllSpawned = serializedObject.FindProperty("startAllSpawned");
        disablePlatformOnJump = serializedObject.FindProperty("disablePlatformOnJump");
        spawnAll = serializedObject.FindProperty("spawnAll");
    }

    public void GenerateToolTip(string text)
    {
        var propRect = GUILayoutUtility.GetLastRect();
        GUI.Label(propRect,new GUIContent("", text));
    }

    public override void OnInspectorGUI()
    {

        EditorGUILayout.LabelField("Options to change for the Chronological platform Manager ");
        EditorGUILayout.LabelField("These all need to changed when the game is not in session");
           
        
        EditorGUILayout.PropertyField(platform);
        
        GenerateToolTip("You need to add every platform in here manually, and chronologically, there is no limit of platforms.");
        allOptions = EditorGUILayout.BeginFoldoutHeaderGroup(allOptions, "All Option");
        GenerateToolTip("These enable/disable other options further down");
        serializedObject.Update();
       if(allOptions)
        {
            EditorGUILayout.Space(20);
            EditorGUILayout.PropertyField(startAllSpawned);
            EditorGUILayout.PropertyField(disablePlatformOnJump);
            GenerateToolTip("Spawn all when jumping on the first platform, to reset the course");
            EditorGUILayout.PropertyField(spawnAll);
           
            EditorGUILayout.Space(20);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        spawnAsTimerEnabled = EditorGUILayout.BeginFoldoutHeaderGroup(spawnAsTimerEnabled, "Spawn As Timer");
        GenerateToolTip("This sets the platforms to spawn using a timer, rather than stepping on them");
        if (spawnAsTimerEnabled )
        {
            
           
            
            if(!startAllSpawned.boolValue && !spawnAll.boolValue)
            {
                EditorGUILayout.Space(20);
                EditorGUILayout.PropertyField(spawnAsTimer);
                EditorGUILayout.PropertyField(timeBetweenPlatformSpawn);
                EditorGUILayout.PropertyField(timeInBetweenWaves);
            }
           
            EditorGUILayout.Space(20);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        isCrumbleTimedEnabled = EditorGUILayout.BeginFoldoutHeaderGroup(isCrumbleTimedEnabled, "Crumble Timer");
        GenerateToolTip("When landing on the platform, the timer decides how long until it crumbles");
        if (isCrumbleTimedEnabled)
        {
            if(!disablePlatformOnJump.boolValue)
            {
                EditorGUILayout.Space(20);
                EditorGUILayout.PropertyField(isCrumbleTimed);
                EditorGUILayout.PropertyField(timeUntilCrumble);
            }
           
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        serializedObject.ApplyModifiedProperties();
    }
}
