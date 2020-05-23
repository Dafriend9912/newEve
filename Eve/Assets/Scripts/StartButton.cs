using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject currPanel;
    public GameObject locations;
    public TextManager textManager;
    public ChoiceManager choiceManager;

    private void Start()
    {
        choiceManager.DisableDialoguePanel();
        choiceManager.DisableChoicePanel();
        textManager.DisableTextBox();
    }

    public void StartGame()
    {
        locations.SetActive(true);
        textManager.EnableTextBox();
        currPanel.SetActive(false);
        

    }
}
