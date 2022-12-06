using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LandButtonMap : MonoBehaviour
{
    public string id;
    public string name;
    public string value;
    public string ownerAddress;
    public string minPurchaseMargin;
    public string category;
    public GameObject landInfoCanvas;


    public TMP_Text nameField;
    public TMP_Text valueField;
    public TMP_Text ownerAddressField;
    public TMP_Text minPurchaseMarginField;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   
    public void clck(){
        // Debug.Log(value);
        // Debug.Log(name);
        // Debug.Log("clicked");
        landInfoCanvas.SetActive(true);
        nameField.text=name;
        valueField.text=value;
        ownerAddressField.text=ownerAddress;
        minPurchaseMarginField.text=minPurchaseMargin;
    }
}
