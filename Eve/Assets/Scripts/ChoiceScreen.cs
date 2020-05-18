using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;

public class ChoiceScreen : MonoBehaviour
{
    public TextMeshProUGUI text1;
    private string path = @"Assets/Resources/Choice Scene/";
    public string path1;
    private string text;
    // Start is called before the first frame update
    void Start()
    {
        using (StreamReader file = new StreamReader(path + path1+".txt"))
        {
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                text += ln;
            }
            file.Close();
        }
    }

    public void onClick(){
        PlayerPrefs.SetString("culprit", path1);
        text1.text = text;
    }
    public void onSubmit(){
        Debug.Log(PlayerPrefs.GetString("culprit"));
    }
    // Update is called once per frame
}
