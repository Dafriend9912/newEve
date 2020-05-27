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
    public Dictionary<char, int> typewriter = new Dictionary<char, int>();
    public AudioClip[] TypewriterSounds;
    public char[] Abc;
    private char k;
    private int result;
    private int i;
    private AudioSource audioSource;
    private AudioClip audioClip;
    public Dictionary<string, bool> choicesdict = new Dictionary<string, bool>();
    public GameObject intro1;
    public GameObject intro2;
    public GameObject intro3;
    public GameObject bar;
    public GameObject charYou;
    public GameObject charOther;
    public GameObject speakerObject;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource s = GetComponent<AudioSource>();
        if (Abc.Length == 0)
        {
            print("Oh");
        }
        dictionaryfiller();
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
                    bar.SetActive(true);
                    intro3.SetActive(false);
                    currline++;
                    charYou.SetActive(true);
                    charOther.SetActive(true);
                    speakerObject.SetActive(true);
                    text = Resources.Load<TextAsset>(@"Dialogue\Bartender\BTfirstMeeting");
                    DisableTextBox();
                    EnableTextBox();
                    return;
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
        CR = true;
        float speed = .032f;
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
            else if (c == ',' || c == '.' || c == '?')
            {
                if (!c.Equals('!') && !c.Equals('.') && !c.Equals(' ') && !c.Equals('?'))
                {   
                    k = c; //takes the letter puts it in k
                    print(k);
                    k = char.ToLower(k); //makes sure that letter is lowercase
                    i = typewriter[k]; //indexes into the dictionary of letters to indexes to grab the index for that number
                    audioClip = TypewriterSounds[i]; //this grabs the sound at that index in another dictionary
                    audioSource = GetComponent<AudioSource>(); //initializes the audiosource
                    audioSource.clip = audioClip; //sets the audiosource.clip to the audioclip that I grabbed earlier
                    audioSource.Play(); //plays the audioclip
                }
                theText.text += c;
                yield return new WaitForSeconds(.4f);
            }
            else if (c == '<' && story[i+1] == 'i')
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
                    if (!c.Equals('!') && !c.Equals('.') && !c.Equals(' ') && !c.Equals('?'))
                    {   
                        k = c; //takes the letter puts it in k
                        print(k);
                        k = char.ToLower(k); //makes sure that letter is lowercase
                        i = typewriter[k]; //indexes into the dictionary of letters to indexes to grab the index for that number
                        audioClip = TypewriterSounds[i]; //this grabs the sound at that index in another dictionary
                        audioSource = GetComponent<AudioSource>(); //initializes the audiosource
                        audioSource.clip = audioClip; //sets the audiosource.clip to the audioclip that I grabbed earlier
                        audioSource.Play(); //plays the audioclip
                    }
                    theText.text += "<i>" + c + "</i>";
                    yield return new WaitForSeconds(.07f);
                }
                i += 4;
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
                        if (!c.Equals('!') && !c.Equals('.') && !c.Equals(' ') && !c.Equals('?'))
                        {   
                            k = c; //takes the letter puts it in k
                            print(k);
                            k = char.ToLower(k); //makes sure that letter is lowercase
                            i = typewriter[k]; //indexes into the dictionary of letters to indexes to grab the index for that number
                            audioClip = TypewriterSounds[i]; //this grabs the sound at that index in another dictionary
                            audioSource = GetComponent<AudioSource>(); //initializes the audiosource
                            audioSource.clip = audioClip; //sets the audiosource.clip to the audioclip that I grabbed earlier
                            audioSource.Play(); //plays the audioclip
                        }
                        theText.text += "<color=#6EEFFF>" + c + "</color>";
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
                        if (!c.Equals('!') && !c.Equals('.') && !c.Equals(' ') && !c.Equals('?'))
                        {   
                            k = c; //takes the letter puts it in k
                            print(k);
                            k = char.ToLower(k); //makes sure that letter is lowercase
                            i = typewriter[k]; //indexes into the dictionary of letters to indexes to grab the index for that number
                            audioClip = TypewriterSounds[i]; //this grabs the sound at that index in another dictionary
                            audioSource = GetComponent<AudioSource>(); //initializes the audiosource
                            audioSource.clip = audioClip; //sets the audiosource.clip to the audioclip that I grabbed earlier
                            audioSource.Play(); //plays the audioclip
                        }
                        theText.text += "<color=#FDFF81>" + c + "</color>";
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
                        if (!c.Equals('!') && !c.Equals('.') && !c.Equals(' ') && !c.Equals('?'))
                        {   
                            k = c; //takes the letter puts it in k
                            print(k);
                            k = char.ToLower(k); //makes sure that letter is lowercase
                            i = typewriter[k]; //indexes into the dictionary of letters to indexes to grab the index for that number
                            audioClip = TypewriterSounds[i]; //this grabs the sound at that index in another dictionary
                            audioSource = GetComponent<AudioSource>(); //initializes the audiosource
                            audioSource.clip = audioClip; //sets the audiosource.clip to the audioclip that I grabbed earlier
                            audioSource.Play(); //plays the audioclip
                        }
                        theText.text += "<color=#FF0000>" + c + "</color>";
                        yield return new WaitForSeconds(speed);
                    }
                    i += 13;
                }

            }
            else
            {
            if (!c.Equals('!') && !c.Equals('.') && !c.Equals(' ') && !c.Equals('?'))
                {   
                    k = c; //takes the letter puts it in k
                    print(k);
                    k = char.ToLower(k); //makes sure that letter is lowercase
                    i = typewriter[k]; //indexes into the dictionary of letters to indexes to grab the index for that number
                    audioClip = TypewriterSounds[i]; //this grabs the sound at that index in another dictionary
                    audioSource = GetComponent<AudioSource>(); //initializes the audiosource
                    audioSource.clip = audioClip; //sets the audiosource.clip to the audioclip that I grabbed earlier
                    audioSource.Play(); //plays the audioclip
                }
                theText.text += c;
            }
            yield return new WaitForSeconds(speed);
        }
        CR = false;
    }
    public void dictionaryfiller()
    {
        i = 0;
        foreach (char s in Abc)
        {
            typewriter.Add(s,i);
            i++;
        }
    }

}


