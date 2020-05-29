using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    public GameObject textbox;
    public TextMeshProUGUI theText;
    public TextAsset text;
    public string[] textlines;
    public int currline;
    public int endline;
    public bool active;
    public TextAsset blank;
    public string story;
    public bool CR = false;
    public Text nameText;
    public ChoiceManager Choices;
    public CharController Chars;
    public string speaker;
    public Dictionary<string, bool> choicesdict = new Dictionary<string, bool>();
    public GameObject intro1;
    public GameObject intro2;
    public GameObject intro3;
    public GameObject bar;
    public GameObject charYou;
    public GameObject charOther;
    public GameObject speakerObject;
    public GameObject introMusic;
    public GameObject AnimationPanel;
    public Animator Animate;
    public AudioClip[] typeWriterSounds;
    private AudioSource audioSource;
    private AudioClip audioClip;
    private int counter;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        if (active)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        }
    }


    public void EnableTextBox()
    {
        currline = 0;
        if (text != null)
        {
            textlines = (text.text.Split('\n'));
        }

        if (endline == 0)
        {
            endline = textlines.Length;
        }
        if (textlines[currline].ToCharArray()[0] == '@')
        {
            nameText.text = textlines[currline].Substring(1);
            speaker = textlines[currline].Substring(1).ToString();
            currline++;
        }
        else if (textlines[currline].ToCharArray()[0] == '#')
        {
            Choices.EnableDialoguePanel(textlines[currline].Substring(1));
            DisableTextBox();
        }
        else if (textlines[currline].ToCharArray()[0] == '$')
        {
            Choices.EnableChoicePanel(textlines[currline].Substring(1));
            DisableTextBox();
        }
        else if (textlines[currline].ToCharArray()[0] == '%') //to change the dictionary
        {
            if (!choicesdict.ContainsKey(textlines[currline].Substring(1).Trim()))
            {
                choicesdict.Add(textlines[currline].Substring(1).Trim(), true);
                string ret = choicesdict[textlines[currline].Substring(1).Trim()].ToString();
                currline++;
                nameText.text = textlines[currline].Substring(1);
                speaker = textlines[currline].Substring(1).ToString();
                currline++;
            }
            else
            {
                currline++;
                nameText.text = textlines[currline].Substring(1);
                speaker = textlines[currline].Substring(1).ToString();
                currline++;
            }
        }
        textbox.SetActive(true);
        active = true;
        Chars.Chars();
        theText.text = textlines[currline];
        story = theText.text; 
        theText.text = "";
        StartCoroutine ("PlayText");
        currline++;
    }

    public void DisableTextBox()
    {
        textbox.SetActive(false);
        active = false;
        currline = 0;
        endline = 0;
        textlines = new string[]{"  ", "  "};
        theText.text = "";
    }
    
    public void Reload(TextAsset texts)
    {
        if (texts != null)
        {
            textlines = new string[1];
            textlines = (texts.text.Split('\n'));
        }
    }

    public void Continue()
    {
        if (active)
        {
            if (CR)
            {
                Skip();
                return;
            }
            if(currline < endline)
            {
                if(textlines[currline].ToCharArray()[0] == '@')
                {
                    nameText.text = textlines[currline].Substring(1);
                    speaker = textlines[currline].Substring(1);
                    currline++;
                }
                else if (textlines[currline].ToCharArray()[0] == '#')
                {
                    Choices.EnableDialoguePanel(textlines[currline].Substring(1));
                    DisableTextBox();
                }
                else if (textlines[currline].ToCharArray()[0] == '$')
                {
                    Choices.EnableChoicePanel(textlines[currline].Substring(1));
                    DisableTextBox();
                }
                else if (textlines[currline].ToCharArray()[0] == '%') //to change the dictionary
                {
                    if(!choicesdict.ContainsKey(textlines[currline].Substring(1).Trim()))
                    {
                        choicesdict.Add(textlines[currline].Substring(1).Trim(), true);
                        currline++;
                    }
                    else
                    {
                        currline++;
                        nameText.text = textlines[currline].Substring(1);
                        speaker = textlines[currline].Substring(1).ToString();
                        currline++;
                    }
                }
                else if (textlines[currline].ToCharArray()[0] == '^') //to look up something in the dictionary
                {
                    bool t = true;
                    if (!choicesdict.ContainsKey(textlines[currline].Substring(1))) //for extra dialogue
                    {
                    } else 
                    {
                        
                    }
                }
                else if (textlines[currline].ToCharArray()[0] == '&' && textlines[currline].ToCharArray()[1] == '1')
                {
                    intro2.SetActive(true);
                    intro1.SetActive(false);
                    currline++;
                }
                else if (textlines[currline].ToCharArray()[0] == '&' && textlines[currline].ToCharArray()[1] == '2')
                {
                    intro3.SetActive(true);
                    intro2.SetActive(false);
                    currline++;
                }
                else if (textlines[currline].ToCharArray()[0] == '&' && textlines[currline].ToCharArray()[1] == '3')
                {
                    AnimationPanel.SetActive(true);
                    Animate.SetBool("Fadeout", true);
                    StartCoroutine(ExampleCoroutine());
                    print("aaa");
                    return;
                }
                else if (textlines[currline].ToCharArray()[0] == '&' && textlines[currline].ToCharArray()[1] == '4')
                {
                    print("&4 seen");
                    charYou.SetActive(false);
                    charOther.SetActive(false);
                    speakerObject.SetActive(false);
                    currline++;
                }
                else if (textlines[currline].ToCharArray()[0] == '&' && textlines[currline].ToCharArray()[1] == '5')
                {
                    print("&5 seen");
                    charYou.SetActive(true);
                    charOther.SetActive(true);
                    speakerObject.SetActive(true);
                    currline++;
                }
                //% change
                //^ look up 
                Chars.Chars();
                theText.text = textlines[currline];
                story = theText.text; 
                theText.text = "";
                StartCoroutine ("PlayText");
                currline++;
            }
            else
            {
                DisableTextBox();
            }
           
        }
    }

    public void Skip()
    {
        if (active)
        {
            CR = false;
            StopCoroutine("PlayText");
            theText.text = "";
            story = story.Replace("*1","");
            story = story.Replace("*2", "");
            story = story.Replace("*3", "");
            story = story.Replace("*4", "");
            story = story.Replace("*5", "");
            story = story.Replace("*6", "");
            theText.text = story;
        }
    }

    IEnumerator PlayText()
    {
        counter = 0;
        CR = true;
        speed = .032f;
        for (int i = 0; i < story.Length; i++ )
        {
            string color = "";
            char c = story[i];
            if (c == '*')
            {
                i++;
                c = story[i];
                if (c == '1') //ultra slow
                {
                    speed = .12f;
                }
                if (c == '2') //slightly slow
                {
                    speed = .07f;
                }
                if (c == '3') //normal speed
                {
                    speed = .032f;
                }
                if (c == '4') // fast
                {
                    speed = .02f;
                }
                if (c == '5') //break without punctuation
                {
                    yield return new WaitForSeconds(.4f);
                }
                if (c == '6') //first line
                {
                    speed = .05f;
                }
            }
            else if (c == ',' || c == '.' || c == '?') //punctuation break
            {
                HelperPlay();
                theText.text += c;
                yield return new WaitForSeconds(.4f);
            }
            else if (c == '<' && story[i+1] == 'i') //italics
            {
                i += 2;
                c = story[i];
                while (c != '<')
                {
                    if (c == ',' || c == '.' || c == '?')
                    {
                        yield return new WaitForSeconds(.4f);
                    }
                    i++;
                    c = story[i];
                    if (c == '<')
                    {
                        break;
                    }
                    HelperPlay();
                    theText.text += "<i>" + c + "</i>";
                    yield return new WaitForSeconds(.07f);
                }
                i += 4;
                //HelperPlay();
                theText.text += story[i];
            }
            else if (c == '<')
            {
                for (int j = 0; j < 7; j++)
                {
                    color += story[i+j+7];
                }
                if (color == "#6EEFFF") // cyan - inner monologue
                {
                    i += 14;
                    c = story[i];
                    while (c != '<')
                    {
                        if (c == ',' || c == '.' || c == '?')
                        {
                            yield return new WaitForSeconds(.4f);
                        }

                        i++;
                        c = story[i];
                        if (c == '<')
                        {
                            theText.text += "<color=#6EEFFF>" + c + "</color>";
                        }
                        else
                        {
                            HelperPlay();
                            theText.text += "<color=#6EEFFF>" + c + "</color>";
                        }
                        yield return new WaitForSeconds(speed);
                    }
                    i += 13;
                }
                if (color == "#FDFF81") //light yellow - people
                {
                    i += 14;
                    c = story[i];
                    while (c != '<')
                    {
                        if (c == ',' || c == '.' || c == '?')
                        {
                            yield return new WaitForSeconds(.4f);
                        }
                        i++;
                        c = story[i];
                        if (c == '<')
                        {
                            theText.text += "<color=#FDFF81>" + c + "</color>";
                        }
                        else
                        {
                            HelperPlay();
                            theText.text += "<color=#FDFF81>" + c + "</color>";
                        }
                        yield return new WaitForSeconds(speed);
                    }
                    i += 13;
                }
                if (color == "#FF0000") // red - important details
                {
                    i += 14;
                    c = story[i];
                    while (c != '<')
                    {
                        if (c == ',' || c == '.' || c == '?')
                        {
                            yield return new WaitForSeconds(.4f);
                        }
                        i++;
                        c = story[i];
                        if (c == '<')
                        {
                            theText.text += "<color=#FF0000>" + c + "</color>";
                        }
                        else
                        {
                            HelperPlay();
                            theText.text += "<color=#FF0000>" + c + "</color>";
                        }
                        yield return new WaitForSeconds(speed);
                    }
                    i += 13;
                }

            }
            else if (!char.IsLetterOrDigit(c))
            {
                theText.text += c;
            }
            else
            {
                print("else");
                HelperPlay();
                theText.text += c;
            }
            yield return new WaitForSeconds(speed);
        }
        CR = false;
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(.75f);
        bar.SetActive(true);
        intro3.SetActive(false);
        introMusic.SetActive(false);
        currline++;
        charYou.SetActive(true);
        charOther.SetActive(true);
        speakerObject.SetActive(true);
        text = Resources.Load<TextAsset>(@"Dialogue\Bartender\BTfirstMeeting");
        DisableTextBox();
        StartCoroutine(pleasework());


    }
    IEnumerator pleasework()
    {
        yield return new WaitForSeconds(.55f);
        EnableTextBox();
        Animate.SetBool("Fadeout", false);
        AnimationPanel.SetActive(false);
    }

    public void HelperPlay()
    {
        if (speed == .12f || speed == .07f) //play every slow part
        {
            audioClip = typeWriterSounds[Random.Range(0, typeWriterSounds.Length)]; //this grabs the sound at that index in another dictionary
            audioSource = GetComponent<AudioSource>(); //initializes the audiosource
            audioSource.clip = audioClip; //sets the audiosource.clip to the audioclip that I grabbed earlier
            audioSource.Play();
        }
        /*else if (Random.Range(0,11) < 7) //play 60% of the time
        {
            audioClip = typeWriterSounds[Random.Range(0, typeWriterSounds.Length)]; //this grabs the sound at that index in another dictionary
            audioSource = GetComponent<AudioSource>(); //initializes the audiosource
            audioSource.clip = audioClip; //sets the audiosource.clip to the audioclip that I grabbed earlier
            audioSource.Play();
        }*/
        else if (counter % 2 == 0) //play every other note
        {
            audioClip = typeWriterSounds[Random.Range(0, typeWriterSounds.Length)]; //this grabs the sound at that index in another dictionary
            audioSource = GetComponent<AudioSource>(); //initializes the audiosource
            audioSource.clip = audioClip; //sets the audiosource.clip to the audioclip that I grabbed earlier
            audioSource.Play();
        }
        counter++;
        
    }
}
