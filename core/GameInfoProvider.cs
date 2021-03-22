using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoProvider : MonoBehaviour
{

    [SerializeField]public Button infoImage;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI text;
    WeaponHolder holder;
    public string info; public string button;
    public int showInfo = 0;
   // GameObject pla;
    private void Awake()
    {
        holder = FindObjectOfType<WeaponHolder>();
      //  pla = holder.gameObject;
       // holder.transform.SetParent(transform);
        text = text.gameObject.GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        holder = FindObjectOfType<WeaponHolder>();
        HideInfo();
    }
    public void PickWeapon()
    {
        holder.PickUpWeapon();
    }
    // Update is called once per frame
    void LateUpdate()
    {
      
        if (text != null)
        {
            text.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1400f, Screen.height / 900f, 1);
            text.GetComponent<RectTransform>().localPosition = new Vector3(Screen.width * 0.14f, Screen.height * 0.14f, 1);

            text2.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1400f, Screen.height / 900f, 1);
            text2.GetComponent<RectTransform>().localPosition = new Vector3(Screen.width * 0.1f, Screen.height * 0.14f, 1);

        }

        infoImage.GetComponent<RectTransform>().localPosition = new Vector3(Screen.width * 0.01f, Screen.height * 0.15f, 1);

        infoImage.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 2000f, Screen.height / 1400f, 1);
        if (showInfo != 0)
        {
            ShowInfo(info,button);
            
        }
        else
            HideInfo();

        showInfo = 0;
    }

    public void HideInfo()
    {
        infoImage.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
    }
    public void ShowInfo(string info,string button)
    {
        text.text = info;
        text2.text = button;
        text.gameObject.SetActive(true);
        text2.gameObject.SetActive(true);

        infoImage.gameObject.SetActive(true);
    }
 
    
}
