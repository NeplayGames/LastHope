using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class InputText : MonoBehaviour
{

    public InputField input;

    // Use this for initialization
    void Start()
    {
        
        input.keyboardType = TouchScreenKeyboardType.Search;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
