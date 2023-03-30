using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;

public class BonePosition : MonoBehaviour
{
    private GameObject playerCamera_;
    private GameObject rightArmPosition_;
    private GameObject leftArmPosition_;

    private const float STANDING_HEIGHT_ = 1.6f;
    VRCPlayerApi.TrackingData data;

    private void Awake() 
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        VRCPlayerApi locPlayer = Networking.LocalPlayer;
        // locPlayer.GetBonePosition(HumanBodyBones.Chest);
        // locPlayer.GetPosition();
        data = locPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        data = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand);
        transform.position = data.position;
        // Debug.Log("__TESTING:" + data.position);
        // Debug.Log("__TESTING:" + data.position);
        // Debug.Log("__TESTING:" + data.position);
    }
}
