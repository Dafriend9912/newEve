using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ChoiceManager : MonoBehaviour
{
    public GameObject Dialoguepanel;
    public TextMeshProUGUI Button1Text;
    public TextMeshProUGUI Button2Text;
    public TextMeshProUGUI Button3Text;
    public TextMeshProUGUI Button4Text;
    public TextMeshProUGUI Button5Text;
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
        Button2Text.text = textlines[1];
        Button3Text.text = textlines[2];
        Button4Text.text = textlines[3];
        Button5Text.text = textlines[4];
        active = true;
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
    }
}
