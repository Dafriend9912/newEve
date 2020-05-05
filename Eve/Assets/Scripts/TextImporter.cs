using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImporter : MonoBehaviour
{

    public TextAsset text;
    public string[] textlines;

    // Start is called before the first frame update
    void Start()
    {
        if (text != null)
        {
            textlines = (text.text.Split('\n'));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}