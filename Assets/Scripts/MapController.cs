using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject MapPanel;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Map Controller started");
        // MapPanel=GameObject.Find("MapPanel");
    }

    // // Update is called once per frame
    void Update()
    {
        bool Mpressed = Input.GetKeyDown(KeyCode.M);
        if (Mpressed)
        {
            Debug.Log("M pressed");
            // MapPanel.LoadMap();
            MapPanel.SetActive(!MapPanel.activeSelf);
            if(!MapPanel.activeSelf){
                Cursor.lockState = CursorLockMode.Locked;
            }
            // MapPanel.SetActive(true);

        }

        bool Ppressed = Input.GetKeyDown(KeyCode.P);
        if (Ppressed)
        {
            Debug.Log("P pressed");
            player=GameObject.Find("localPlayer");
            player.transform.position=new Vector3(50f,0f,0f);
            // MapPanel.LoadMap();
            // MapPanel.SetActive(!MapPanel.activeSelf);
            // if(!MapPanel.activeSelf){
            //     Cursor.lockState = CursorLockMode.Locked;
            // }
            // MapPanel.SetActive(true);

        }
        bool Vpressed = Input.GetKeyDown(KeyCode.V);
        if (Vpressed)
        {
            Debug.Log("V pressed");
            player=GameObject.Find("localPlayer");
            player.transform.position=new Vector3(10f,0f,0f);
            // MapPanel.LoadMap();
            // MapPanel.SetActive(!MapPanel.activeSelf);
            // if(!MapPanel.activeSelf){
            //     Cursor.lockState = CursorLockMode.Locked;
            // }

           
            // MapPanel.SetActive(true);

        }
        if(MapPanel.activeSelf){
            Cursor.lockState = CursorLockMode.None; 
        }
    }
}
