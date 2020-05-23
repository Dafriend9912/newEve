using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ChoiceManager : MonoBehaviour
{
    public GameObject Dialoguepanel;
    public GameObject mapPanel;
    public GameObject locations;
    public TextMeshProUGUI Button1Text;
    public TextMeshProUGUI Button2Text;
    public TextMeshProUGUI Button3Text;
    public TextMeshProUGUI Button4Text;
    public TextMeshProUGUI Button5Text;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public Button Button5;
    public GameObject ChoicePanel;
    public TextMeshProUGUI Choice1Text;
    public TextMeshProUGUI Choice2Text;
    public string[] textlines;
    public int currline;
    public int endline;
    public bool active;
    public TextAsset blank;
    public Text nameText;
    public TextManager texting;
    public string path;
    public bool choosing = false;

    // Start is called before the first frame update
    void Start()
    {
        DisableDialoguePanel();
        DisableChoicePanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableDialoguePanel(string x)
    {
        path = x;
        print("Choices");
        blank = Resources.Load<TextAsset>(x);
        textlines = (blank.text.Split('\n'));
        Dialoguepanel.SetActive(true);
        Button1Text.text = textlines[0];
        if (textlines.Length >= 2)
        {
            Button2Text.text = textlines[1];
            Button2.interactable = true;
        }
        else
        {
            Button2Text.text = "";
            Button2.interactable = false;
        }
        if (textlines.Length >= 3)
        {
            Button3Text.text = textlines[2];
            Button3.interactable = true;
        }
        else
        {
            Button3Text.text = "";
            Button3.interactable = false;
        }
        if (textlines.Length >= 4)
        {
            if (textlines[3].ToCharArray()[0] == '^') //to look up something in the dictionary
            {
                if (path == @"Dialogue\Dancer\DNOPTIONS3")
                {
                    print(textlines[5]);
                    print(texting.choicesdict.ContainsKey("Call2"));
                    if (texting.choicesdict.ContainsKey("Call2"))
                    {
                        Button4Text.text = textlines[4];
                        Button4.interactable = true;
                    }
                    else
                    {
                        Button4Text.text = "";
                        Button4.interactable = false;
                    }
                }
            }
            else
            {
                Button4Text.text = "";
                Button4.interactable = false;
            }
        }
        else
        {
            Button5Text.text = "";
            Button5.interactable = false;
        }
        bool t = true;
        if (textlines.Length >= 5)
        {
            if (textlines[4].ToCharArray()[0] == '^') //to look up something in the dictionary
            {
                if (path == @"Dialogue\Owner\ONOPTIONS3")
                {
                    print(textlines[5]);
                    print(texting.choicesdict.ContainsKey("ONgala"));
                    if (texting.choicesdict.ContainsKey("ONgala") && texting.choicesdict.ContainsKey("ONjob"))
                    {
                        Button5Text.text = textlines[5];
                        Button5.interactable = true;
                    }
                    else
                    {
                        Button5Text.text = "";
                        Button5.interactable = false;
                    }
                }
            }
            if (textlines[5].ToCharArray()[0] == '^') //to look up something in the dictionary
            {
                if (path == @"Dialogue\Dancer\DNOPTIONS3")
                {
                    if (texting.choicesdict.ContainsKey("DNstaff"))
                    {
                        Button5Text.text = textlines[6];
                        Button5.interactable = true;
                    }
                    else
                    {
                        Button5Text.text = "";
                        Button5.interactable = false;
                    }
                }
            }
            else
            {
                Button5Text.text = "";
                Button5.interactable = false;
            }
        }
        else
        {
            Button5Text.text = "";
            Button5.interactable = false;
        }
        choosing = true;
    }

    public void EnableChoicePanel(string x)
    {
        path = x;
        print("Choices");
        blank = Resources.Load<TextAsset>(x);
        textlines = (blank.text.Split('\n'));
        ChoicePanel.SetActive(true);
        Choice1Text.text = textlines[0];
        Choice2Text.text = textlines[1];
        active = true;
    }

    public void DisableDialoguePanel()
    {
        Dialoguepanel.SetActive(false);
    }

    public void DisableChoicePanel()
    {
        ChoicePanel.SetActive(false);
    }

    public void StayClick()
    {
        print(path);
        if (path == @"Dialogue\Bartender\BTOPTIONS1")
        {
            print("working");
            texting.text = Resources.Load<TextAsset>(@"Dialogue\Bartender\BTstay");
            DisableChoicePanel();
            texting.EnableTextBox();
        }
        if (path == @"Dialogue\Owner\ONOPTIONS1")
        {
            print("working");
            texting.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONstay");
            DisableChoicePanel();
            texting.EnableTextBox();
        }
        if (path == @"Dialogue\Dancer\DNOPTIONS1")
        {
            print("working");
            texting.text = Resources.Load<TextAsset>(@"Dialogue\Dancer\DNstay");
            DisableChoicePanel();
            texting.EnableTextBox();
        }
    }

    public void LeaveClick()
    {
        print("leaving");
        //texting.text = Resources.Load<TextAsset>(@"Dialoguepanel\Bartender\BTleave");
        for (int i = 0; i < locations.transform.childCount; i++)
        {
            var child = locations.transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(false);
        }
        mapPanel.SetActive(true);
        print(mapPanel.activeSelf ? "Active" : "Inactive");
        DisableChoicePanel();
    }

    public void DialogueClickButton1()
    {
        if (choosing)
        {
            print(Button1Text.text);
            print(textlines[0]);
            if (path == @"Dialogue\Bartender\BTOPTIONS3")
            {
                print("working");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Bartender\BTgala");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Owner\ONOPTIONS3")
            {
                print("working");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONjob");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Owner\ONOPTIONS4")
            {
                print("working???");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONlazy");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Dancer\DNOPTIONS3")
            {
                print("working???");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Dancer\DNphoneCall");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Dancer\DNOPTIONS4")
            {
                print("working???");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Dancer\DNphoneCall2");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
        }
    }

    public void DialogueClickButton2()
    {
        if (choosing)
        {
            if (path == @"Dialogue\Bartender\BTOPTIONS3")
            {
                print("working");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Bartender\BTmoreSelf");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Owner\ONOPTIONS3")
            {
                print("working");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONgala");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Owner\ONOPTIONS4")
            {
                print("working?????????");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONmurderer");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Dancer\DNOPTIONS3")
            {
                print("working???");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Dancer\DNgala");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Dancer\DNOPTIONS4")
            {
                print("working???");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Dancer\DNforget");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
        }
    }

    public void DialogueClickButton3()
    {
        if (choosing)
        {
            if (path == @"Dialogue\Bartender\BTOPTIONS3")
            {
                print("working");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Bartender\BTlateHome");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Owner\ONOPTIONS3")
            {
                print("working");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONvictims");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Owner\ONOPTIONS4")
            {
                print("working?");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONforget");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Dancer\DNOPTIONS3")
            {
                print("working???");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Dancer\DNstaff");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
        }
    }
    public void DialogueClickButton4()
    {
        if (choosing)
        {
            if (path == @"Dialogue\Bartender\BTOPTIONS3")
            {
                print("working");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Bartender\BTpeople");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Owner\ONOPTIONS3")
            {
                print("working");
                texting.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONmurderer");
                DisableDialoguePanel();
                texting.EnableTextBox();
                choosing = false;
            }
            else if (path == @"Dialogue\Dancer\DNOPTIONS3")
            {
                if (texting.choicesdict.ContainsKey("Call2"))
                {
                    texting.text = Resources.Load<TextAsset>(@"Dialogue\Dancer\DNpatrons");
                    DisableDialoguePanel();
                    texting.EnableTextBox();
                    choosing = false;
                }
                else
                {
                    return;
                }
            }

        }
    }

    public void DialogueClickButton5()
    {
        if (choosing)
        {
            if (path == @"Dialogue\Bartender\BTOPTIONS3")
            {
                return;
            }
            else if (path == @"Dialogue\Owner\ONOPTIONS3")
            {
                if (texting.choicesdict.ContainsKey("ONgala") && texting.choicesdict.ContainsKey("ONjob"))
                {
                    texting.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONwork");
                    DisableDialoguePanel();
                    texting.EnableTextBox();
                    choosing = false;
                }
                else
                {
                   return;
                }
            }
            else if (path == @"Dialogue\Dancer\DNOPTIONS3")
            {
                if (texting.choicesdict.ContainsKey("DNstaff"))
                {
                    texting.text = Resources.Load<TextAsset>(@"Dialogue\Dancer\DNpatrons");
                    DisableDialoguePanel();
                    texting.EnableTextBox();
                    choosing = false;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
