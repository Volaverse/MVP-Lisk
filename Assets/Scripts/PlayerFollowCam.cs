using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFollowCam : MonoBehaviour
{
    private GameObject player1;
    private GameObject player;
    public CinemachineFreeLook cam;

    // Start is called before the first frame update
    void Start()
    {
        player1= GameObject.Find("localPlayer"); 
        // player=GetChildWithName(player,"CameraLookAt"); 
        player=player1.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // if(player!= GameObject.Find("localPlayer")){
        //     player = GameObject.Find("localPlayer");
        // }
        if(player != null){
            // Vector3 position = player.transform.position;
            // position.y += 1.5;
            Transform playerPos=player.transform;
            // playerPos.position= new Vector3(playerPos.position.x, playerPos.position.y+1.5f, playerPos.position.z);
            // cam.transform.position = player.transform.position;
            cam.LookAt = playerPos;
            cam.Follow= playerPos;
        }
    }
}
