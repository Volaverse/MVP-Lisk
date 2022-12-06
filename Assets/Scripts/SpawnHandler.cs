using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnHandler : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject AgoraController;
    public GameObject vCam;
    public GameObject playerCamScript;     
    public GameObject VideoCanvas;

    

    void Start()
    {
        Debug.Log("Started Spawn Handler script!");
        Invoke("initPlayer",2f);
    }
    void Update(){
            // GameObject player = GameObject.Find("localPlayer");
            // // GameObject Ground = GameObject.Find("Ground");
            // // Debug.Log(player);
            // if(player==null){
            // if (playerPrefab == null)
            // {
            //     Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            // }
            // else
            // {
            //     Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);

            //     try{

            //     PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity, 0);
            //     // Ground.SetActive(true);
            //     }catch{
            //         Debug.Log("error in instantiate inside spawn handle");
            //     }
            // }
            // }
    }
    void initPlayer(){
         GameObject player = GameObject.Find("localPlayer");
            // GameObject Ground = GameObject.Find("Ground");
            // Debug.Log(player);
            if(player==null){
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);

                try{
                Debug.Log(this.playerPrefab.name);
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(10f,0f,0f), Quaternion.identity, 0);
                AgoraController.SetActive(true);
                vCam.SetActive(true);
                playerCamScript.SetActive(true);
                VideoCanvas.SetActive(true);
                // Ground.SetActive(true);
                }catch{
                    Debug.Log("error in instantiate inside spawn handle");
                }
            }
            }
    }
}