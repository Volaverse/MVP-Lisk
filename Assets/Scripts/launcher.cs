using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.Collections;


public class launcher : MonoBehaviourPunCallbacks
{
    public GameObject SpawnHandler;
    public GameObject Volocator;
    public static gameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started launcher script!");
        Screen.fullScreen = true;
        // manager=GameObject.Find("GameManager");
        // GameObject.Find("GameManager").GetComponent<gameManager>();

        manager = gameManager.Instance;
         if(manager==null){
            SceneManager.LoadScene("Login");
        }

        // manager.GetComponent<gameManager>().startLoading();
        
    }

    void Awake(){
        Debug.Log("inside awake");
        // PhotonNetwork.ConnectUsingSettings();
        // Debug.Log("connecting using settings");
        // DontDestroyOnLoad(gameObject);
        // GameObject Volocator = GameObject.Find("Volocator");
        // Volocator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect(){
          if(manager==null){
            SceneManager.LoadScene("Login");
        }
       else{

        manager.startLoading();
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("connecting using settings");
       }
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinRoom("Room12345");

    }
    

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
        StartCoroutine(Disconnect());
        // Connect();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed() was called by PUN with error code: " + returnCode + " and message: " + message);
        RoomOptions options = new RoomOptions();
        options.PlayerTtl = 60000; // 60 sec
        options.EmptyRoomTtl = 60000; // 60 sec
        options.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom("Room12345", options, null);
        // PhotonNetwork.CreateRoom("Room12345", new RoomOptions { MaxPlayers = 10 });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Create Room Failed");
        manager.stopLoading();
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in room: " + PhotonNetwork.CurrentRoom);
        // PhotonNetwork.LoadLevel(1);

        GameObject Canvas = GameObject.Find("Canvas");
        Canvas.SetActive(false);
       
        Volocator.SetActive(true);
        // GameObject SpawnHandler = GameObject.Find("SpawnHandler");
        SpawnHandler.SetActive(true);
        manager.stopLoading();

        // manager.GetComponent<gameManager>().stopLoading();
        // GameObject.Find("GameManager").GetComponent<gameManager>().startLoading();

    }

    public override void OnLeftRoom()
    {
        // loading.text = "You left the room";
        Debug.Log("You left the room");
        
    }

    IEnumerator Disconnect()
    {
    PhotonNetwork.Disconnect();
    while (PhotonNetwork.IsConnected)
    {
    yield return null;
    Debug.Log("Disconnecting. . .");
    }
    Debug.Log("DISCONNECTED!");
    }
    
    // void OnApplicationQuit()
    // {
    //     Debug.Log("Appication Quit");
    //     PhotonNetwork.LeaveRoom();
    //    StartCoroutine(Disconnect());
    // }
    

    }
