using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class P3FSparkSettingsEditorWindow : EditorWindow
{
    public static string test, test1;
    public int selectedIndex = 0;
    public string[] dropdownOptions = { "Alchemy", "Infura", "Node (Not Implemented)" };
    private bool showLabelAndInput = false; // For the checkbox state
    int updateFreq;
    private float sliderValue = 0.5f; // Default value for the slider
    public static string apiKey;
    private bool showPassword;
    private bool debugging;

    [MenuItem("Project 3 Fusion/Spark/Settings")]
    public static void ShowWindow()
    {
        // Show an existing window instance. If one doesn't exist, make one.
        GetWindow(typeof(P3FSparkSettingsEditorWindow), false, "P3F Spark Settings");
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Connection Settings", EditorStyles.whiteLargeLabel);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Connection Provider");
        selectedIndex = EditorGUILayout.Popup(selectedIndex, dropdownOptions);
        EditorGUILayout.EndHorizontal();

        EditorGUI.indentLevel++;
        switch (selectedIndex)
        {
            case 0: // Option 1
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("API Key");
                GUIContent toggleContentAPI = new GUIContent("Show API Key");
                if(!showPassword) apiKey = EditorGUILayout.PasswordField(apiKey);
                else apiKey = EditorGUILayout.TextField(apiKey);
                EditorGUILayout.EndHorizontal();
                showPassword = EditorGUILayout.Toggle(toggleContentAPI, showPassword);
                break;
            case 1: // Option 2
                EditorGUILayout.HelpBox("This option is not usable, and will be implemented in the future", MessageType.Info);
                break;
            case 2: // Option 3
                EditorGUILayout.HelpBox("This option is not usable, and will be implemented in the future", MessageType.Info);
                break;
        }
        EditorGUI.indentLevel--;

        GUIContent toggleContent = new GUIContent("Continous Check", "Smart contract data update frequency in seconds");
        showLabelAndInput = EditorGUILayout.Toggle(toggleContent, showLabelAndInput);

        // If checkbox is checked, show label and input
        if (showLabelAndInput)
        {
            // Slider with tooltip
            GUIContent sliderContent = new GUIContent("Interval (Minutes)", "Slide to adjust the value.");
            sliderValue = EditorGUILayout.Slider(sliderContent, sliderValue, 0.1f, 30f);
            EditorGUILayout.HelpBox("You can continously listen smart contract changes.", MessageType.Info);
        }

        GUIContent debuggingContent = new GUIContent("Transaction Debugging", "Debug all transactions");
        debugging = EditorGUILayout.Toggle(debuggingContent, debugging);
    }
}