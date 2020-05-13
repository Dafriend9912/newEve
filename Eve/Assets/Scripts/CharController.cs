using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Player;
    public Image Other;
    public TextManager manage;
    // Update is called once per frame
    public void Chars() {
        print(manage.speaker);
        print(manage.speaker.GetType());
        if (manage.speaker.Trim().Equals("You"))
        {
            print("working");
            var color1 = Other.color;
            color1.a = .75f;
            Other.color = color1;
            var color2 = Player.color;
            color2.a = 1f;
            Player.color = color2;
        }
        else
        {
            var color1 = Player.color;
            color1.a = .75f;
            Player.color = color1;
            var color2 = Other.color;
            color2.a = 1f;
            Other.color = color2;
        }
    }
}
