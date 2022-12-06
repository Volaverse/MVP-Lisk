// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using agora_gaming_rtc;
// using agora_utilities;
// using UnityEngine.UI;

// public class AgoraContr : MonoBehaviour
// {
//     public string AppID;
//     public string ChannelName;
//     VideoSurface myView;
//     VideoSurface remoteView;
//     IRtcEngine mRtcEngine;
//     public bool videoOn=false;

//     void Awake()
//     {
//         Debug.Log("Awake from agora");
//         SetupUI();
//     }

//     void Update(){
//         bool Epressed = Input.GetKeyDown(KeyCode.E);
//         if (Epressed)
//         {
//             Debug.Log("E pressed");
//             if (!videoOn)
//             {
//                 videoOn=true;
//                 Join();
//             }
//             else
//             {
//                 videoOn=false;
//                 Leave();
//             }
//         }
//     }
//     void Start()
//     {
//         Debug.Log("Start Agora");
//         SetupAgora();
//     }

//     void SetupUI()
//     {
//         GameObject go = GameObject.Find("MyView");
//         myView = go.AddComponent<VideoSurface>();
//         go = GameObject.Find("LeaveButton");
//         go?.GetComponent<Button>()?.onClick.AddListener(Leave);
//         go = GameObject.Find("JoinButton");
//         go?.GetComponent<Button>()?.onClick.AddListener(Join);
//     }

//       void SetupAgora()
//     {
//         mRtcEngine = IRtcEngine.GetEngine(AppID);
//         mRtcEngine.OnUserJoined = OnUserJoined;
//         mRtcEngine.OnUserOffline = OnUserOffline;
//         mRtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccessHandler;
//         mRtcEngine.OnLeaveChannel = OnLeaveChannelHandler;
//         Debug.Log("Agora Set Up");
//     }

//     void Join()
//     {
//         Debug.Log("Video on");
//         mRtcEngine.EnableVideo();
//         mRtcEngine.EnableLocalAudio(true);
//         mRtcEngine.EnableVideoObserver();
//         myView.SetEnable(true);
//         mRtcEngine.JoinChannel(ChannelName, "", 0);
//     }

//     void Leave()
//     {
//         Debug.Log("Video off");
//         mRtcEngine.LeaveChannel();
//         mRtcEngine.DisableVideo();
//         mRtcEngine.DisableVideoObserver();
//     }

//     void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
//     {
//            // can add other logics here, for now just print to the log
//         Debug.LogFormat("Joined channel {0} successful, my uid = {1}", channelName, uid);
//     }

//     void OnLeaveChannelHandler(RtcStats stats)
//     {
//         myView.SetEnable(false);
//         if (remoteView != null)
//         {
//             remoteView.SetEnable(false);
//         }
//     }
    
//     void OnUserJoined(uint uid, int elapsed)
//     {
//           GameObject go = GameObject.Find("RemoteView");

//         if (remoteView == null)
//         {
//             remoteView = go.AddComponent<VideoSurface>();
//         }

//         remoteView.SetForUser(uid);
//         remoteView.SetEnable(true);
//         remoteView.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
//         remoteView.SetGameFps(30);
//     }

//     void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
//     // void OnUserOffline(uint uid)

//     {
//            remoteView.SetEnable(false);
//     }

//     void OnApplicationQuit()
//     {
//          if (mRtcEngine != null)
//         {
//             mRtcEngine.LeaveChannel();
//         	mRtcEngine.DisableVideoObserver(); 
//             IRtcEngine.Destroy(); 
//             mRtcEngine = null;
//         }
//     }
// }
