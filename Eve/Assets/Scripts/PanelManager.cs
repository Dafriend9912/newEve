using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public TextManager Manager;
    public Image Other;

    public void OpenBartenderPanel()
    {
        BartenderPanel.SetActive(true);
        print(BartenderPanel.activeSelf ? "Bartender Panel should be Active" : "Inactive");
        currentPanel.SetActive(false);
        Manager.EnableTextBox();
        Other.sprite = Resources.Load<Sprite>(@"Characters\IxD bartender silhouette");
    }
    public void OpenOwnerPanel()
    {
        OwnerPanel.SetActive(true);
        currentPanel.SetActive(false);
        Manager.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONfirstMeeting");
        Manager.EnableTextBox();
        Other = Resources.Load<Image>(@"Character\IxD bartender silhouette");

    }
    public void OpenDancerPanel()
    {
        DancerPanel.SetActive(true);
        currentPanel.SetActive(false);
        Manager.text = Resources.Load<TextAsset>(@"Dialogue\Dancer\DNfirstMeeting");
        Manager.EnableTextBox();
        Other.sprite = Resources.Load<Sprite>(@"Characters\IxD main dancer");
    }
    public void OpenMusicianPanel()
    {
        MusicianPanel.SetActive(true);
        currentPanel.SetActive(false);
        Manager.EnableTextBox();
        Other = Resources.Load<Image>(@"Characters\IxD musucian silhouette");
    }
    public void OpenBoxerPanel()
    {
        BoxerPanel.SetActive(true);
        print(BoxerPanel.activeSelf ? "Boxer Panel should be Active" : "Inactive");
        currentPanel.SetActive(false);
        Manager.EnableTextBox();
        Other.sprite = Resources.Load<Sprite>(@"Characters\IxD boxer silhouette");
    }
}
