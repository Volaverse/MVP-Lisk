// using UnityEngine;
// using System.Collections;

// public class authentication : MonoBehaviour {
//     [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
//     [SerializeField]
//     private byte maxPlayersPerRoom = 5;

//     private const float API_CHECK_MAXTIME = 2f; //seconds
//     private float apiCheckCountdown = API_CHECK_MAXTIME;

//     public static gameManager manager;
    
//     public string publicKey="0x3363f66D380b57F16f29f7C6A2d9250994eDD9c1";
//     public string publicKey="ce18224de15bd432dab97c15405cbf9d6c4dab6e8a96eda262fc23b21af101d6";

//     public bool wallet = false;
//     bool checkingForPublicKey = false;

//     [DllImport("__Internal")]
//     private static extern int ConnectToMetamask();

//     [DllImport("__Internal")]
//     private static extern string GetAccount();

//     launcher photonScript;



// public void Start(){
//     Debug.Log("Started authentication script!");
// }

// public void onLogin(bool _wallet)
//     {
//         wallet = _wallet;
//         manager = gameManager.Instance;
//         if (!wallet)
//         {
//             manager.setPlayerInfo(wallet, publicKeyInput.text, privateKeyInput.text);
//             // Connect();
//             photonScript.Connect();
//         }
//         else
//         {
//             manager.setPlayerInfo(wallet, "", "");
//             loading.text = "Please check browser Metamask extension for connection request";
//             int metamaskError = ConnectToMetamask();
//             if (metamaskError != 0)
//             {
//                 if (metamaskError == 1 || metamaskError == 2)
//                 {
//                     loading.text = "Could not find Metamask!";
//                 }
//                 else if (metamaskError == 3)
//                 {
//                     loading.text = "Please switch to Rinkedby!";
//                 }
//                 else
//                 {
//                     loading.text = "Unknown connection error!";
//                 }
//             }
//             else
//             {
//                 if (!checkingForPublicKey)
//                 {
//                     checkingForPublicKey = true;
//                     //StartCoroutine(CheckForPublicKey());
//                 }

//             }

//         }

//     }
// }