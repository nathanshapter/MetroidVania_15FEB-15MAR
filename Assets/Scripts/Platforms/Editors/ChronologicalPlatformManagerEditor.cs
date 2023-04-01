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

    SerializedProperty startAllSpawned;
    SerializedProperty disablePlatformOnJump;
    SerializedProperty spawnAll;
    SerializedProperty letPlatformsRespawn;
    SerializedProperty platformRespawnTime;

    bool spawnAsTimerEnabled = false;
    bool isCrumbleTimedEnabled = false;
    bool allOptions = false;
    bool helpMessage = true;
    

    private void OnEnable()
    {
        spawnAsTimer = serializedObject.FindProperty("spawnAsTimer");
        timeBetweenPlatformSpawn = serializedObject.FindProperty("timeBetweenPlatformSpawn");
        timeInBetweenWaves = serializedObject.FindProperty("timeInBetweenWaves");
        isCrumbleTimed = serializedObject.FindProperty("isCrumbleTimed");
        timeUntilCrumble = serializedObject.FindProperty("timeUntilCrumble");
      
        startAllSpawned = serializedObject.FindProperty("startAllSpawned");
        disablePlatformOnJump = serializedObject.FindProperty("disablePlatformOnJump");
        spawnAll = serializedObject.FindProperty("spawnAll");
        letPlatformsRespawn = serializedObject.FindProperty("letPlatformsRespawn");
        platformRespawnTime = serializedObject.FindProperty("platformRespawnTime");
    }

    public void GenerateToolTip(string text)
    {
        var propRect = GUILayoutUtility.GetLastRect();
        GUI.Label(propRect,new GUIContent("", text));
    }

    public override void OnInspectorGUI()
    {
        helpMessage = EditorGUILayout.BeginFoldoutHeaderGroup(helpMessage, "Help Message");
        if (helpMessage)
        {
            EditorGUILayout.LabelField("Options to change for the Chronological platform Manager.");
            EditorGUILayout.LabelField("These all need to be changed when the game is not in session.");
            EditorGUILayout.LabelField("The array that stores the platforms is in the other script connected because of");
            EditorGUILayout.LabelField("a bug that Unity will not fix");
            EditorGUILayout.LabelField("All platforms must be added manually");
            EditorGUILayout.LabelField("Insert YT Tutorial link here");
        }
        EditorGUILayout.EndFoldoutHeaderGroup();




        GenerateToolTip("You need to add every platform in here manually, and chronologically, there is no limit of platforms.");
        allOptions = EditorGUILayout.BeginFoldoutHeaderGroup(allOptions, "All Option");
        GenerateToolTip("These enable/disable other options further down");
        serializedObject.Update();
       if(allOptions)
        {
            EditorGUILayout.Space(20);
            EditorGUILayout.PropertyField(startAllSpawned);
            EditorGUILayout.PropertyField(disablePlatformOnJump);
            EditorGUILayout.PropertyField(spawnAll);
            GenerateToolTip("Spawn all when jumping on the first platform, to reset the course");
           
         

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
                if(spawnAsTimer.boolValue)
                {
                    EditorGUILayout.PropertyField(timeBetweenPlatformSpawn);
                    EditorGUILayout.PropertyField(timeInBetweenWaves);
                }
               
            }
           
            EditorGUILayout.Space(20);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        isCrumbleTimedEnabled = EditorGUILayout.BeginFoldoutHeaderGroup(isCrumbleTimedEnabled, "Crumble Timer");
        GenerateToolTip("When landing on the platform, the timer decides how long until it crumbles");
        if (isCrumbleTimedEnabled && !spawnAsTimer.boolValue)
        {
           
           
            if (!disablePlatformOnJump.boolValue)
            {
                EditorGUILayout.Space(20);
                EditorGUILayout.PropertyField(isCrumbleTimed);
                EditorGUILayout.PropertyField(timeUntilCrumble);
                if(isCrumbleTimed.boolValue)
                {
                    EditorGUILayout.PropertyField(letPlatformsRespawn);
                    if (letPlatformsRespawn.boolValue)
                    {
                        EditorGUILayout.PropertyField(platformRespawnTime);
                    }
                }
                
            }
           
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        serializedObject.ApplyModifiedProperties();
    }
}
