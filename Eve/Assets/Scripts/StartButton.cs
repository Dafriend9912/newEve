using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject currPanel;
    public GameObject locations;
    public TextManager textManager;
    public ChoiceManager choiceManager;
    public GameObject charYou;
    public GameObject charOther;
    public GameObject introMusic;

    private void Start()
    {
        choiceManager.DisableDialoguePanel();
        choiceManager.DisableChoicePanel();
        textManager.DisableTextBox();
        charYou.SetActive(false);
        charOther.SetActive(false);
        introMusic.SetActive(false);
    }

    public void StartGame()
    {
        locations.SetActive(true);
        textManager.EnableTextBox();
        currPanel.SetActive(false);
        charYou.SetActive(false);
        charOther.SetActive(false);
        introMusic.SetActive(true);

    }
}
