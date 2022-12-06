using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour
{
 
    public GameObject loginCanvas;
    public GameObject completeLoginCanvas;

    // Start is called before the first frame update
    void Start()
    {
    completeLoginCanvas.SetActive(false);
    loginCanvas.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
