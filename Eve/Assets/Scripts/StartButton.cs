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
    public GameObject AnimationPanel;
    public Animator Animate;

    private void Start()
    {
        choiceManager.DisableDialoguePanel();
        choiceManager.DisableChoicePanel();
        textManager.DisableTextBox();
        charYou.SetActive(false);
        charOther.SetActive(false);
        AnimationPanel.SetActive(false);
        locations.SetActive(false);

        introMusic.SetActive(false);
    }

    public void StartGame()
    {
        AnimationPanel.SetActive(true);
        Animate.SetBool("Fadeout", true);
        StartCoroutine(ExampleCoroutine());
        AudioSource music = currPanel.GetComponent<AudioSource>();
        StartCoroutine(StartFade(music,.75f,0f));

    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(.75f);
        print("Thos work?");
        locations.SetActive(true);
        StartCoroutine(pleasework());
        
        
    }
    IEnumerator pleasework()
    {
        yield return new WaitForSeconds(.75f);
        currPanel.SetActive(false);
        print("Thos work?");
        Animate.SetBool("Fadeout", false);
        AnimationPanel.SetActive(false);
        textManager.EnableTextBox();
        charYou.SetActive(false);
        charOther.SetActive(false);
        introMusic.SetActive(true);

    }
    IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
