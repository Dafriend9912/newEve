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
    public ChoiceManager Choices;
    public CharController Chars;
    public string speaker;
    public Dictionary<string, bool> choicesdict = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
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
        currline = 0;
        if (text != null)
        {
            textlines = (text.text.Split('\n'));
        }

        if (endline == 0)
        {
            endline = textlines.Length;
        }
        if (textlines[currline].ToCharArray()[0] == '@')
        {
            nameText.text = textlines[currline].Substring(1);
            speaker = textlines[currline].Substring(1).ToString();
            currline++;
        }
        else if (textlines[currline].ToCharArray()[0] == '#')
        {
            Choices.EnableDialoguePanel(textlines[currline].Substring(1));
            DisableTextBox();
        }
        else if (textlines[currline].ToCharArray()[0] == '$')
        {
            Choices.EnableChoicePanel(textlines[currline].Substring(1));
            DisableTextBox();
        }
        else if (textlines[currline].ToCharArray()[0] == '%') //to change the dictionary
        {
            print("OKAY IT SEES THE %");
            if (!choicesdict.ContainsKey(textlines[currline].Substring(1)))
            {
                print("EEEEEEEEEEEEEEEEEEEEKTHISWORKS");
                choicesdict.Add(textlines[currline].Substring(1).Trim(), true);
                string ret = choicesdict[textlines[currline].Substring(1).Trim()].ToString();
                print(ret);
                currline++;
                nameText.text = textlines[currline].Substring(1);
                speaker = textlines[currline].Substring(1).ToString();
                currline++;
            }
        }
        textbox.SetActive(true);
        active = true;
        Chars.Chars();
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
        currline = 0;
        endline = 0;
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
                    speaker = textlines[currline].Substring(1);
                    currline++;
                }
                else if (textlines[currline].ToCharArray()[0] == '#')
                {
                    Choices.EnableDialoguePanel(textlines[currline].Substring(1));
                    DisableTextBox();
                }
                else if (textlines[currline].ToCharArray()[0] == '$')
                {
                    Choices.EnableChoicePanel(textlines[currline].Substring(1));
                    DisableTextBox();
                }
                else if (textlines[currline].ToCharArray()[0] == '%') //to change the dictionary
                {
                    print("OKAY IT SEES THE %");
                    if(!choicesdict.ContainsKey(textlines[currline].Substring(1).Trim()))
                    {
                        print("EEEEEEEEEEEEEEEEEEEEKTHISWORKS");
                        print(textlines[currline]);
                        choicesdict.Add(textlines[currline].Substring(1).Trim(), true);
                        currline++;
                    }
                }
                else if (textlines[currline].ToCharArray()[0] == '^') //to look up something in the dictionary
                {
                    bool t = true;
                    if (!choicesdict.ContainsKey(textlines[currline].Substring(1))) //for extra dialogue
                    {
                        print("heythisruns");
                    } else 
                    {
                        
                    }
                }
                //% change
                //^ look up 
                Chars.Chars();
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
    IEnumerator PlayText2()
    {
        CR = true;
        foreach (char c in story) 
        {
            if (c == '*')
            {
                
                theText.text += "<color=yellow>" + c + "</color>";
            }
            else
            {
                theText.text += c;
            }
            yield return new WaitForSeconds (0.050f);
        }
        CR = false;
    }

    IEnumerator PlayText()
    {
        CR = true;
        for (int i = 0; i < story.Length; i++ )
        {
            string color = "";
            char c = story[i];
            if (c == '<')
            {
                for (int j = 0; j < 7; j++)
                {
                    color += story[i+j+7];
                }
                print("Next color is: " + color);
                if (color == "#6EEFFF") // cyan
                {
                    i += 14;
                    c = story[i];
                    while (c != '<')
                    {
                        i++;
                        c = story[i];
                        theText.text += "<color=#6EEFFF>" + c + "</color>";
                        yield return new WaitForSeconds(0.050f);
                    }
                    i += 13;
                }
                if (color == "#FDFF81") //light yellow
                {
                    i += 14;
                    c = story[i];
                    while (c != '<')
                    {
                        i++;
                        c = story[i];
                        theText.text += "<color=#FDFF81>" + c + "</color>";
                        yield return new WaitForSeconds(0.050f);
                    }
                    i += 13;
                }
                if (color == "#FF0000") // red
                {
                    i += 14;
                    c = story[i];
                    while (c != '<')
                    {
                        i++;
                        c = story[i];
                        theText.text += "<color=#FF0000>" + c + "</color>";
                        yield return new WaitForSeconds(0.050f);
                    }
                    i += 13;
                }

            }
            else
            {
                theText.text += c;
            }
            yield return new WaitForSeconds(0.050f);
        }
        CR = false;
    }

}
