using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
 
public class CameraFollow : MonoBehaviour
{
 
    public GameObject tPlayer;
    public Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;
 
    void Start()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
    }
 
    void Update()
    {
        if (tPlayer!= GameObject.Find("localPlayer")    )
        {
            tPlayer = GameObject.Find("localPlayer");
        }
        Debug.Log(tPlayer);
        tFollowTarget = tPlayer.transform;
        vcam.LookAt = tFollowTarget;
        vcam.Follow = tFollowTarget;
    }
}

// 1.54 4.33