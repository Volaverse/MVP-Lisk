// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AgoraButtons : MonoBehaviour
// {
//  public void OnButtonClick()
// {
//     Debug.Log("Button Clicked: " + name);

//     // determin which button
//     if (name.CompareTo("JoinButton") == 0)
//     {
//         // join chat
//         OnJoinButtonClicked();
//     }
//     else if (name.CompareTo("LeaveButton") == 0)
//     {
//         // leave chat
//         OnLeaveButtonClicked();
//     }
// }

// private void OnJoinButtonClicked()
// {
//     Debug.Log("Join button clicked");

//     // get channel name from text input
//     GameObject go = GameObject.Find("ChannelName");
//     InputField input = go.GetComponent<InputField>();
// }

// private void OnLeaveButtonClicked()
// {
//     Debug.Log("Leave button clicked");
// }
// }
