using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoOnGameMisssion : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        HideMessage();
    }

    // Update is called once per frame
   public void UpdateMessage(string msg)
    {
        textMeshPro.gameObject.SetActive(true);
        textMeshPro.text = msg;
    }
    public void HideMessage()
    {
        textMeshPro.gameObject.SetActive(false);
    }
   
}
