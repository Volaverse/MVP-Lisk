using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#pragma warning disable XY

public class gameManager : MonoBehaviourPunCallbacks
{
    // private static Vector3 tp = new Vector3(0, 0, 0);
    // private static string publicKey = "";
    // private static string privateKey = "";
    // private static bool wallet = false;
    // private static string roomNo = "";
    [SerializeField]    
    private string lskAdd;
    public string userName;
    public GameObject loadingMenu;


    [Tooltip("The prefab to use for representing the player")]
    public static gameManager Instance;

    void Start()
    {
        if(Instance==null){
        Instance = this;
        Debug.Log("Started Game Manager!");
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(loadingMenu);
        }

    }
    public void startLoading(){
        loadingMenu.SetActive(true);
    }

    public void stopLoading(){
        loadingMenu.SetActive(false);
        
    }

    void Update()
    {
        
    }

    public void setPlayerInfo(string add,string name){
        lskAdd=add;
        userName=name;
    }

    public string getPlayerAddress(){
        return lskAdd;
    }

    void LoadWorld()
    {
        SceneManager.LoadScene(1);
    }


    // public void settpinfo(float a, float b)
    // {
    //     tp[0] = 1f;
    //     tp[1] = a;
    //     tp[2] = b;
    // }

    // public Vector3 tpinfo()
    // {
    //     Vector3 tp2 = tp;
    //     tp[0] = 0;
    //     if (tp2[0] == 1)
    //     {
    //         Debug.Log(tp2);
    //     }
    //     return tp2;
    // }

    // public void setPlayerInfo(bool _wallet, string _publicKey, string _privateKey)
    // {
    //     wallet = _wallet;
    //     publicKey = _publicKey;
    //     privateKey = _privateKey;
    //     Debug.Log("Received public and private keys");
    //     Debug.Log("Received public: " + publicKey);
    // }

    // public bool isWallet()
    // {
    //     return wallet; 
    // }
    // public string getPublicKey()
    // {
    //     return publicKey;
    // }

    // public string getPrivateKey()
    // {
    //     return privateKey;
    // } 

    // public string getRoomNo()
    // {
    //     return roomNo;
    // }

    // public void setRoomNo(string _roomNo)
    // {
    //     Debug.Log(_roomNo);
    //     roomNo = _roomNo;
    // }
}
