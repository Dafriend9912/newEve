using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject currPanel;
    public GameObject locations;
    public GameObject textManager;

    public void StartGame()
    {
        locations.SetActive(true);
        textManager.SetActive(true);
        currPanel.SetActive(false);
    }
}
