    using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuCanvasSetup : MonoBehaviour
{
    [SerializeField] Button storyMode;
    [SerializeField] Button trainingGround;
    [SerializeField] Button loadOut;
    [SerializeField] Button Quit;
    [SerializeField] Button Yes;
    [SerializeField] Button No;
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        storyMode.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1250f, Screen.height / 300f, 1);
        trainingGround.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1250f, Screen.height / 300f, 1);
        loadOut.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1250f, Screen.height / 300f, 1);
        Quit.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1250f, Screen.height / 300f, 1);
        storyMode.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.875f, Screen.height * 0.2f, 1);
        trainingGround.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.875f, Screen.height * 0.15f, 1);
        loadOut.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.875f, Screen.height * 0.1f, 1);
        Quit.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.875f, Screen.height * 0.05f, 1);
        Yes.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1250f, Screen.height / 300f, 1);
        Yes.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.6f, Screen.height * 0.1f, 1); 
         No.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1250f, Screen.height / 300f, 1);
        No.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.7f, Screen.height * 0.08f, 1);
        text.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1250f, Screen.height / 300f, 1);
        text.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.675f, Screen.height * 0.3f, 1);
    }
}
