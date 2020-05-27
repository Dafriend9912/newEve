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
    public GameObject AnimationPanel;
    public Animator Animate;

    public void OpenBartenderPanel()
    {
        AnimationPanel.SetActive(true);
        Animate.SetBool("Fadeout", true);
        StartCoroutine(Bartender());
        
    }
    public void OpenOwnerPanel()
    {
        OwnerPanel.SetActive(true);
        currentPanel.SetActive(false);
        Manager.text = Resources.Load<TextAsset>(@"Dialogue\Owner\ONfirstMeeting");
        Manager.EnableTextBox();
        Other = Resources.Load<Image>(@"Character\IxD bartender V2");

    }
    public void OpenDancerPanel()
    {
        AnimationPanel.SetActive(true);
        Animate.SetBool("Fadeout", true);
        StartCoroutine(Dancer());
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
        Other.sprite = Resources.Load<Sprite>(@"Characters\IxD boxer V2");
    }

    IEnumerator Bartender()
    {
        yield return new WaitForSeconds(.75f);
        BartenderPanel.SetActive(true);
        print(BartenderPanel.activeSelf ? "Bartender Panel should be Active" : "Inactive");
        Other.sprite = Resources.Load<Sprite>(@"Characters\IxD bartender silhouette");
        StartCoroutine(Bartender2());
    }

    IEnumerator Bartender2()
    {
        yield return new WaitForSeconds(.65f);
        currentPanel.SetActive(false);
        Manager.EnableTextBox();
        Animate.SetBool("Fadeout", false);
        AnimationPanel.SetActive(false);
    }
    IEnumerator Dancer()
    {
        yield return new WaitForSeconds(.75f);
        DancerPanel.SetActive(true);
        Manager.text = Resources.Load<TextAsset>(@"Dialogue\Dancer\DNfirstMeeting");
        Other.sprite = Resources.Load<Sprite>(@"Characters\IxD dancer V2");
        StartCoroutine(Dancer2());
    }

    IEnumerator Dancer2()
    {
        yield return new WaitForSeconds(.65f);
        currentPanel.SetActive(false);
        AnimationPanel.SetActive(false);
        Manager.EnableTextBox();
        Animate.SetBool("Fadeout", false);
        
    }
}
