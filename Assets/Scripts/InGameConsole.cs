using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using VRC.SDKBase;

public class InGameConsole : MonoBehaviour
{
    private Dictionary<string, string> debugLogs = new Dictionary<string, string>();
    private TMP_Text display;
    private VRCPlayerApi locPlayer;
    private const string CONSOLE_KEY = "IGS";

    private void Awake() {
        display = GetComponentInChildren<TMP_Text>();
        locPlayer = Networking.LocalPlayer;
    }
    
    private void Update() {
        Debug.Log("IGS@Right Hand Tracking Data: " + locPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).position);
        Debug.Log("IGS@Right Hand Bone Position: " + locPlayer.GetBonePosition(HumanBodyBones.RightHand));
    }

    void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable() {
        Application.logMessageReceived -= HandleLog;
    }
    
    void HandleLog(string logString, string stackTrace, LogType type){
        if(type != LogType.Log) return;

        string[] splitString = logString.Split(new char[] {'@'}, 2);
        if(splitString[0] != CONSOLE_KEY) return;

        string[] debugMessage = splitString[1].Split(new char[] {':'}, 2);
        string debugKey = debugMessage[0];
        string debugValue = debugMessage[1];

        if(debugLogs.ContainsKey(debugKey)){
            debugLogs[debugKey] = debugValue;
        }
        else{
            debugLogs.Add(debugKey, debugValue);
        }

        StringBuilder displayText = new StringBuilder();
        foreach(var log in debugLogs){
            if(log.Value == ""){
                displayText.AppendLine(log.Key);
            }
            else{
                displayText.AppendLine(log.Key + ":" + log.Value);
            }
        }
        display.text = displayText.ToString();
    }
}
