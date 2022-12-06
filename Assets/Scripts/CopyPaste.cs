using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CopyPaste : MonoBehaviour
{ 
    public TMP_InputField createAccountAdd;
    public TMP_InputField createAccountPass;
    public TMP_InputField lskPassphrase;


    public void copyLiskAddress()
    {
       TextEditor textEditor=new TextEditor();
       textEditor.text=createAccountAdd.text;
       textEditor.SelectAll();
       textEditor.Copy();

    }

    public void copyLiskPass()
    {
        TextEditor textEditor=new TextEditor();
        textEditor.text=createAccountPass.text;
        textEditor.SelectAll();
        textEditor.Copy();

    }

    public void pasteLiskPass()
    {
        TextEditor textEditor=new TextEditor();
        textEditor.multiline=true;
        textEditor.Paste();
        lskPassphrase.text=textEditor.text;

    }
}
