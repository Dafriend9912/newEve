using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceScreen : MonoBehaviour
{
    public RawImage img;
    public GameObject prefabs;
    public Transform buttonContainer;
    // Start is called before the first frame update
    void Start()
    {
        Texture[] textures = Resources.LoadAll<Texture>("Silouhete");
        foreach(Texture T in textures){
            GameObject go =Instantiate(prefabs) as GameObject;
            go.transform.SetParent(buttonContainer);
            go.GetComponent<RawImage>().texture = T;
        }
    }

    // Update is called once per frame
}
