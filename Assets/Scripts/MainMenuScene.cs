using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuScene : MonoBehaviour
{
    public TMP_Text welcomeText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test");
        welcomeText.text = "hallo";
 
    }

    // Update is called once per frame
    void Update()

    {
        welcomeText.text = "hallo";
        Debug.Log("test");


    }
}
