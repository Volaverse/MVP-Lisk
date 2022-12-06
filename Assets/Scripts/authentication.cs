using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class authentication : MonoBehaviour {

    [SerializeField]
    public gameManager manager;
    private string userName="";
    private string lskAdd="";
    private string passphrase="";
    private bool oldUser = false;
    private bool existingUser = false;

    public TMP_InputField fullNameField;
    public TMP_InputField emailField;
    public TMP_InputField phoneNumberField;
    public TMP_InputField userNameField;
    public TMP_InputField ageField;
    public TMP_InputField passField;
    public TMP_Text errorText;
    public TMP_InputField createAccountAdd;
    public TMP_InputField createAccountPass;
    public int guestSignIn = 1;
    public GameObject liskAccCanvas;
    public GameObject liskAccFieldsCanvas;
    public GameObject guestCanvas;
    public GameObject phoneNumCanvas;
    public GameObject loginCanvas;
    public GameObject completeLoginCanvas;
    // private string url="http://localhost:8000";
    private string url = "https://backend.volaverse.com/block";

    // public GameObject createAccountCanvas;


public void Start(){
    Debug.Log("Started authentication script!!");
    Screen.fullScreen = true;

    // createAccountCanvas.SetActive(false);
    guestCanvas.SetActive(true);
    phoneNumCanvas.SetActive(true); 
    liskAccCanvas.SetActive(false);
    liskAccFieldsCanvas.SetActive(false);
    // completeLoginCanvas.SetActive(false);

    manager = gameManager.Instance;
    Debug.Log(manager);
}

public void onLogin(){
        Debug.Log("Login Request");
        passphrase=passField.text;
       
        // if(!userName.Equals("") && !passphrase.Equals("")){
        if(!passphrase.Equals("")){
            manager.startLoading();      
            StartCoroutine(loginRequest(url+"/login"));
        }
        else{
            errorText.text="Please enter a username and passphrase";
            Debug.Log("Please enter a username and passphrase");
        }

}

public void createAccount(){
    manager.startLoading();
    liskAccCanvas.SetActive(false);
    liskAccFieldsCanvas.SetActive(true);
    guestSignIn=0;
    errorText.text="";
    Debug.Log("Create Account Request");
    StartCoroutine(CreateAccountRequest(url+"/create"));
}

public void createAccountScene(){
    SceneManager.LoadScene("SignUp");
}

public void loginScene(){
    SceneManager.LoadScene("Login");
}

 IEnumerator CreateAccountRequest(string uri){
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
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    var userObj = JObject.Parse( webRequest.downloadHandler.text);
                    var pass=userObj["password"].ToString();
                    lskAdd=userObj["display_add"].ToString();
                    Debug.Log(lskAdd);
                    passphrase=pass;
                    passField.text=pass;
                    // createAccountCanvas.SetActive(true);
                    createAccountAdd.text=lskAdd;
                    createAccountPass.text=passphrase;
                    break;
            }
            manager.stopLoading();
        }
}

[System.Serializable]
public class UserNameInfo
{
    public string liskAddress;
}


//  IEnumerator GetUsernameFromLsk(string uri,string add){
//     Debug.Log("Fetching Username for "+add);
//         UserNameInfo myObject = new UserNameInfo();
//         myObject.liskAddress =add;
//         string jsonStringTrial = JsonUtility.ToJson(myObject);
//         Debug.Log(jsonStringTrial);

//         using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
//         {
//             // Request and wait for the desired page.
//         byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringTrial);
//         webRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
//         webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
//         webRequest.SetRequestHeader("Content-Type", "application/json");
//             yield return webRequest.SendWebRequest();

//             string[] pages = uri.Split('/');
//             int page = pages.Length - 1;

//             switch (webRequest.result)
//             {
//                 case UnityWebRequest.Result.ConnectionError:
//                 case UnityWebRequest.Result.DataProcessingError:
//                     Debug.LogError(pages[page] + ": Error: " + webRequest.error);
//                     break;
//                 case UnityWebRequest.Result.ProtocolError:
//                     Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
//                     break;
//                 case UnityWebRequest.Result.Success:
//                     Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
//                     var userObj = JObject.Parse( webRequest.downloadHandler.text);
//                     var message=userObj["message"].ToString();
//                     if(message!="No user found"){
//                         oldUser=true;
//                         userName=message;
//                     }


//                      if(oldUser){
//                         manager.GetComponent<gameManager>().setPlayerInfo(add,userName);
//                         Debug.Log("loading volocator"); 
//                         SceneManager.LoadScene("Volocator");
//                         manager.stopLoading();

//                     }else{
//                         guestSignIn=0;
//                         existingUser=true;
//                         loginCanvas.SetActive(false);
//                         completeLoginCanvas.SetActive(true);
//                         manager.stopLoading();

//                     }
//                     break;
//             }
//         }
// }

 IEnumerator GetUsernameFromLsk(string uri,string add){
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
                errorText.text="Something went wrong";
                manager.stopLoading();
            }
            else
            {
                Debug.Log("herereerer");
                    var userObj = JObject.Parse( webRequest.downloadHandler.text);
                    var message=userObj["message"].ToString();
                    if(message!="No user found"){
                        oldUser=true;
                        userName=message;
                    }


                     if(oldUser){
                        manager.GetComponent<gameManager>().setPlayerInfo(add,userName);
                        Debug.Log("loading volocator"); 
                        SceneManager.LoadScene("Volocator");
                        manager.stopLoading();

                    }else{
                        guestSignIn=0;
                        existingUser=true;
                        loginCanvas.SetActive(false);
                        completeLoginCanvas.SetActive(true);
                        manager.stopLoading();

                    }
               

            }
        // }
}


[System.Serializable]
public class LoginInfo
{
    public string passphrase;
}

 IEnumerator loginRequest(string url1){
        Debug.Log(url1);
        Debug.Log("login request");
      
        LoginInfo myObject = new LoginInfo();
        myObject.passphrase =passphrase;
        string jsonStringTrial = JsonUtility.ToJson(myObject);
        Debug.Log(jsonStringTrial);


     var request = new UnityWebRequest(url1, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringTrial);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        
            

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request);
                Debug.Log(request.error);
                // errorText.text=request.error;
                errorText.text="Something went wrong";
                manager.stopLoading();
            }
            else
            {
                var res=request.downloadHandler.text;
                var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(res);
                string display_add = dic["display_add"];
                lskAdd=display_add;
                StartCoroutine(GetUsernameFromLsk(url+"/getUsername",display_add));
               

                // manager.setPlayerInfo(display_add,userName);
                
            }
            // manager.stopLoading();


}

[System.Serializable]
public class AddUserInfo{
    public string fullName;
    public string userName;
    public string phoneNumber;
    public string age;
    public string email;
    public string liskAddress;
}

public void askForLisk(){
    manager.startLoading();
      if(fullNameField.text.Equals("")||emailField.text.Equals("")||phoneNumberField.text.Equals("")||ageField.text.Equals("")){
        errorText.text="Please enter all the fields";
        manager.stopLoading();
        return;
    }
    liskAccCanvas.SetActive(true);
    guestCanvas.SetActive(false);
    phoneNumCanvas.SetActive(false);
    manager.stopLoading();

}
public void saveDetailsToDB(){
    manager.startLoading();
    Debug.Log("Add Info to DB Request");
    errorText.text="";

    if(fullNameField.text.Equals("")||emailField.text.Equals("")||phoneNumberField.text.Equals("")||ageField.text.Equals("")){
        errorText.text="Please enter all the fields";
        manager.stopLoading();
        return;
    }

    if(guestSignIn==0){
        if(userNameField.text.Equals("")){
            errorText.text="Please enter a username";
            manager.stopLoading();
            return;
        }
    }
    // if(existingUser){
    //     createAccountAdd.text=lskAdd;
    // }

    StartCoroutine(addUserRequest(url+"/addInfo"));
}


 IEnumerator addUserRequest(string url){
    Debug.Log("Add User Request");

        AddUserInfo myObject = new AddUserInfo();
        myObject.fullName =fullNameField.text;
        if(guestSignIn==1){
            // Random generator = new Random();
            int randomNumber = Random.Range(1000, 9999);
            // String userNameTemp = generator.Next(0, 10000).ToString("D4");
            myObject.userName=fullNameField.text+randomNumber;
            Debug.Log("Assigned Guest User Name: "+myObject.userName);
            myObject.liskAddress ="";

        }else{
            myObject.userName =userNameField.text;
            myObject.liskAddress =lskAdd;

        }
        myObject.phoneNumber =phoneNumberField.text;
        myObject.age =ageField.text;
        myObject.email =emailField.text;

        string jsonStringTrial = JsonUtility.ToJson(myObject);
        Debug.Log(jsonStringTrial);


     var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringTrial);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        
            

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.result);
                Debug.Log(request.error);
                Debug.Log(request.downloadHandler.text);
                errorText.text="Something went wrong. Please try again later";
                
            }
            else
            {
                Debug.Log("add info request success");
                var res=request.downloadHandler.text;
                Debug.Log(res);
                var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(res);
                Debug.Log(dic);
                string message = dic["message"];
                Debug.Log(message);
                    // if(message=="Username already taken"){
                    //     Debug.
                    // }
                // Debug.Log(message);
                if(message=="Username already taken"){
                    errorText.text="Username Already Taken";
                }else{
                manager.GetComponent<gameManager>().setPlayerInfo(myObject.liskAddress,myObject.userName);
                Debug.Log("loading volocator");
                SceneManager.LoadScene("Volocator");

                }
                
            }
            manager.stopLoading();


}




}
