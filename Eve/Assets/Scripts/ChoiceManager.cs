using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void EnableDialoguePanel()
    {
        Dialoguepanel.SetActive(true);
        active = true;
        currline++;
    }

    public void EnableChoicePanel()
    {
        ChoicePanel.SetActive(true);
        active = true;
        currline++;
    }

    public void DisableDialoguePanel()
    {
        Dialoguepanel.SetActive(false);
        active = false;
        textlines = new string[] { "  ", "  " };
    }

    public void DisableChoicePanel()
    {
        ChoicePanel.SetActive(false);
        active = false;
        textlines = new string[] { "  ", "  " };
    }

    public void Importtext(string x)
    {
        blank = Resources.Load(x) as TextAsset;
    }
}
