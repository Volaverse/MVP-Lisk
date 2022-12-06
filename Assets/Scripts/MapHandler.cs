using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Text;
using TMPro;

public class MapHandler : MonoBehaviour
{
    public GameObject canvas; //I use this to set the canvas after applying this script to an empty gameobject
    public GameObject panel; // I do the same for the panel
    public GameObject landInfoCanvas;

    public Vector2 LandViewCount; // 6 * 4
    public Vector2 LandViewSize; // 80 * 80
    public Vector2 InitialLandViewPosition; // -300 * 250
    public Vector2 LandViewPositionOffset; // 125 * 200
    public GameObject landOnSale;
    public GameObject landNotOnSale;
    public GameObject landMine;
    public GameObject player;
    public string currentLandSelected;
    // private string url="http://localhost:8000";
    private string url = "https://backend.volaverse.com/block";

    public LandButtonMap currentLandButtonSelected;

    public static gameManager manager;
    public string lskAdd;
    string tempUserName;

    [System.Serializable]
    public class landTokenInfo
    {
        public string id;
        public string value;
        public string ownerAddress;
        public string minPurchaseMargin;
        public string name;
        public string category;
        public string imgUrl;
        public string[] tokenHistory;
    }
    public landTokenInfo[] landTokens;
     bool reqComplete=false;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Map Started");
        player=GameObject.Find("localPlayer");
        landInfoCanvas.SetActive(false);

        // player.GetComponent<CursorController>().enabled=false;
        manager = gameManager.Instance;
        manager.startLoading();
        lskAdd=manager.getPlayerAddress();
        // lskAdd="lskvscucmny9fut8e5x9vxcfd6hmw9rb46kyp8drn";
        fetchTokens();
        // Debug.Log("innn");
        StartCoroutine(GenerateLand());
        manager.stopLoading();
    }

      IEnumerator GenerateLand()
    {
        // land.SetActive(false);
        // landOnSale.SetActive(false);
        // landNotOnSale.SetActive(false);
        // landMine.SetActive(false);
        yield return new WaitUntil(() => reqComplete);
        GenerateLandView();
        Debug.Log("Map Loaded!");
    }


    async void fetchTokens(){
        StartCoroutine(fetchTokenRequest(url+"/tokenInfo"));
        return;



    }

     IEnumerator fetchTokenRequest(string uri){
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    reqComplete=true;

                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    reqComplete=true;

                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    var userObj = JObject.Parse( webRequest.downloadHandler.text);
                    // Debug.Log(userObj["data"]);
                    landTokens = userObj["data"].ToObject<landTokenInfo[]>();
                    // Debug.Log(landTokens[0].id);
                    reqComplete=true;
                    break;
            }
        }
}


    void GenerateLandView()
    {
        int landCount=0;
        int totalLand=landTokens.Length;
        string landType;
        for (int a = 0; a < LandViewCount.y; a++)
        {
            for (int b = 0; b < LandViewCount.x; b++)
            {
                // landType="mine";
                // landType="notOnSale";
                // landType="onSale";
                LandViewBuilder(LandViewSize, 
                new Vector2(InitialLandViewPosition.x + (LandViewPositionOffset.x * b), 
                InitialLandViewPosition.y - (LandViewPositionOffset.x * a)),
                panel.transform,landCount);
                landCount++;
            }
        }
    }

    [System.Serializable]
public class UserNameInfo
{
    public string liskAddress;
}

 IEnumerator GetUsernameFromLsk(string uri,string add,LandButtonMap tempLandButton){
    Debug.Log("Fetching Username for "+add+uri);
        UserNameInfo myObject = new UserNameInfo();
        myObject.liskAddress =add;
        string jsonStringTrial = JsonUtility.ToJson(myObject);
        Debug.Log(jsonStringTrial);

        // using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        // {
        var webRequest = new UnityWebRequest(uri, "POST");
            // Request and wait for the desired page.
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringTrial);
        webRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest);
                Debug.Log(webRequest.error);
                // errorText.text=request.error;
                // errorText.text="Something went wrong";
                // manager.stopLoading();
            }
            else
            {
                Debug.Log("herereerer");
                    var userObj = JObject.Parse( webRequest.downloadHandler.text);
                    var message=userObj["message"].ToString();
                    if(message!="No user found"){
                        tempLandButton.ownerAddress=message;

                    }

                        else{
                        // tempUserName=message;
                        tempLandButton.ownerAddress="NIL";
                    }

               

            }
        // }
}

    void LandViewBuilder(Vector2 size, Vector2 position, Transform objectToSetLandView,int landCount)
    {
       GameObject landButtonTemp;
        if(landTokens[landCount].ownerAddress==lskAdd){
            landButtonTemp=landMine;
        }
        else {
            if(landTokens[landCount].minPurchaseMargin=="0"){
                landButtonTemp=landNotOnSale;

            }
            else{
                landButtonTemp=landOnSale;
            }
        }
      

        GameObject landButton=GameObject.Instantiate(landButtonTemp);
        RectTransform rectTransform = landButton.GetComponent<RectTransform>();
        rectTransform.sizeDelta = size;
        rectTransform.anchoredPosition = position;
        landButton.transform.SetParent(objectToSetLandView, false);
        // TMP_Text landText=landButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        // TMP_Text landText=landButton.GetComponent<TMP_Text>();
        // landText.text=landTokens[landCount].name;
        landButton.SetActive(true);
        // TMP_InputField landText=landButton.transform.GetChild(0).gameObject.GetComponent<TMP_InputField>();;
        currentLandButtonSelected=landButton.GetComponent<LandButtonMap>();
        currentLandButtonSelected.id=landTokens[landCount].id;
        currentLandButtonSelected.value=landTokens[landCount].value;
        currentLandButtonSelected.minPurchaseMargin=landTokens[landCount].minPurchaseMargin;
        currentLandButtonSelected.name=landTokens[landCount].name;
        currentLandButtonSelected.category=landTokens[landCount].category;
        StartCoroutine(GetUsernameFromLsk(url+"/getUsername",landTokens[landCount].ownerAddress,currentLandButtonSelected));
        // currentLandButtonSelected.ownerAddress=landTokens[landCount].ownerAddress;
        // currentLandButtonSelected.ownerAddress=tempUserName;
       
        // // currentLandButtonSelected.setLandData(landTokens[landCount]);
        // currentLandButtonSelected.setLandData(
        //     landTokens[landCount].id,
        //     landTokens[landCount].name,
        //     landTokens[landCount].value,
        //     landTokens[landCount].ownerAddress,
        //     landTokens[landCount].category,
        //     landTokens[landCount].imgUrl,
        //     landTokens[landCount].minPurchaseMargin);

    }

}