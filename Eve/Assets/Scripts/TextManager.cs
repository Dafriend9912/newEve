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
        }else if (textlines[currline].ToCharArray()[0] == '%') //to change the dictionary
        {
            if(!choicesdict.ContainsKey(textlines[currline].Substring(1)))
            {
                choicesdict.Add(textlines[currline].Substring(1), true);
            }
        }
        else if (textlines[currline].ToCharArray()[0] == '^') //to look up something in the dictionary
        {
            bool t = true;
            if (choicesdict.TryGetValue(textlines[currline].Substring(1), out t)) //for extra dialogue
            {
                        
            } else 
            {
                        
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
                    if(!choicesdict.ContainsKey(textlines[currline].Substring(1)))
                    {
                        choicesdict.Add(textlines[currline].Substring(1), true);
                    }
                }
                else if (textlines[currline].ToCharArray()[0] == '^') //to look up something in the dictionary
                {
                    bool t = true;
                    if (choicesdict.TryGetValue(textlines[currline].Substring(1), out t)) //for extra dialogue
                    {
                        
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
