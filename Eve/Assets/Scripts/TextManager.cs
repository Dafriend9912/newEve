using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public GameObject textbox;
    public TextMeshProUGUI theText;
    public TextAsset text;
    public string[] textlines;
    public int currline;
    public int endline;
    public bool active;
    public TextAsset blank;
    public string story;
    public bool CR = false;
    public Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        if (text != null)
        {
            textlines = (text.text.Split('\n'));
        }

        if (endline == 0)
        {
            endline = textlines.Length;
        }
        if (active)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        }
    }


    public void EnableTextBox()
    {

        
        textbox.SetActive(true);
        active = true;
        theText.text = textlines[currline];
        story = theText.text; 
        theText.text = "";
        StartCoroutine ("PlayText");
        currline++;
    }

    public void DisableTextBox()
    {
        textbox.SetActive(false);
        active = false;
        textlines = new string[]{"  ", "  "};
        theText.text = "";
    }
    
    public void Reload(TextAsset texts)
    {
        if (texts != null)
        {
            textlines = new string[1];
            textlines = (texts.text.Split('\n'));
        }
    }

    public void Continue()
    {
        print("testing");
        if (active)
        {
            if (CR)
            {
                Skip();
                return;
            }
            if(currline < endline)
            {
                if(textlines[currline].ToCharArray()[0] == '@')
                {
                    nameText.text = textlines[currline].Substring(1);
                    currline++;
                }
                theText.text = textlines[currline];
                story = theText.text; 
                theText.text = "";
                StartCoroutine ("PlayText");
                currline++;
            }
            else
            {
                DisableTextBox();
            }
           
        }
    }

    public void Skip()
    {
        if (active)
        {
            CR = false;
            StopCoroutine("PlayText");
            theText.text = "";
            theText.text = story;
        }
    }
    IEnumerator PlayText()
    {
        CR = true;
        foreach (char c in story) 
        {
            theText.text += c;
            yield return new WaitForSeconds (0.050f);
        }
        CR = false;
    }
}
