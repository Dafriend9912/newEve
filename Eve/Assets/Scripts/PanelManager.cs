using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject locations;
    public GameObject currentPanel;
    public GameObject BartenderPanel;
    public GameObject DancerPanel;
    public GameObject OwnerPanel;
    public GameObject MusicianPanel;
    public GameObject BoxerPanel;

    public void OpenBartenderPanel()
    {
        BartenderPanel.SetActive(true);
        print(BartenderPanel.activeSelf ? "Bartender Panel should be Active" : "Inactive");
        currentPanel.SetActive(false);
    }
    public void OpenOwnerPanel()
    {
        OwnerPanel.SetActive(true);
        currentPanel.SetActive(false);
    }
    public void OpenDancerPanel()
    {
        DancerPanel.SetActive(true);
        currentPanel.SetActive(false);
    }
    public void OpenMusicianPanel()
    {
        MusicianPanel.SetActive(true);
        currentPanel.SetActive(false);
    }
    public void OpenBoxerPanel()
    {
        BoxerPanel.SetActive(true);
        print(BoxerPanel.activeSelf ? "Boxer Panel should be Active" : "Inactive");
        currentPanel.SetActive(false);
    }
}
