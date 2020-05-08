﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Other;

    // Update is called once per frame
    void Update()
    {
        if ( TextManager.nameText.ToString() == "You")
        {
            Other.SetActive(false);
            Player.SetActive(true);
        }
        else
        {
            Player.SetActive(false);
            Other.SetActive(true);
        }
    }
}