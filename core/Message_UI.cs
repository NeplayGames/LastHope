using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace lastHope.core
{

    public class Message_UI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI message;
        [SerializeField] Image image;
       
        private void Start()
        {
            message.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.6f, Screen.height * -0.2f, 1);
            image.GetComponent<RectTransform>().localScale = new Vector3(Screen.width , Screen.height/280f, 1);

            message.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 920f, Screen.height / 800f, 1);
            message.gameObject.SetActive(false);
            image.gameObject.SetActive(false);

        }
        public void Update_UI(string msg)
        {
            if (!message.gameObject.activeInHierarchy)
                message.gameObject.SetActive(true);
            message.text = msg;
            if (!image.gameObject.activeInHierarchy)
                image.gameObject.SetActive(true);
        }
        public void HideUI()
        {
            message.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
        }
    }

}
