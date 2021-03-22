using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;
    //[SerializeField] TextMeshProUGUI pause;
    //[SerializeField] Transform image2;
    
    //[SerializeField] Button map;
    //[SerializeField] Button setting;
    //[SerializeField] Button option;
    //[SerializeField] Button stat;
    //[SerializeField] Button quit;

    //[SerializeField] Canvas maps;
    //[SerializeField] Canvas settings;
    //[SerializeField] Canvas options;
    //[SerializeField] Canvas stats;
    //[SerializeField] Canvas quits;
  

    // Start is called before the first frame update
    void Awake()
    {
        // image.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.6f, Screen.height * 0.5f, 1);

        //image2.GetComponent<RectTransform>().localScale = new Vector3(Screen.width, Screen.height, 1);
        //pause.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1700f, Screen.height / 550f, 1);
        //map.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 900f, Screen.height / 400f, 1);

        //setting.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 900f, Screen.height / 400f, 1);
        //option.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 900f, Screen.height / 400f, 1);
        //stat.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 900f, Screen.height / 400f, 1);
        //quit.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 900f, Screen.height / 400f, 1);
        //pause.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.14f, Screen.height * 0.92f, 1);
        //map.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.14f, Screen.height * 0.85f, 1);
        //setting.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.32f, Screen.height * 0.85f, 1);
        //option.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.50f, Screen.height * 0.85f, 1);
        //stat.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.68f, Screen.height * 0.85f, 1);
        //quit.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.86f, Screen.height * 0.85f, 1);
     //   HideAllFirst();


        //  HidePauseMenu();
    }
    public void ShowSettingMenu()
    {
        isPaused = !isPaused;
        if (isPaused == true) ShowPauseMenu(); else HidePauseMenu();
    }
    // Update is called once per frame
    void Update()
    {
       
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused == true) ShowPauseMenu();else HidePauseMenu();
        }
        Time.timeScale = isPaused ? 0 : 1;
       
    }
   //public void HideAllFirst()
   // {
      
   //     stats.gameObject.SetActive(false);
   //     settings.gameObject.SetActive(false);

   // }
   // public void ShowStat()
   // {
   //     stats.gameObject.SetActive(true);
   // }
   // public void ShowMap()
   // {
   //     maps.gameObject.SetActive(true);
   // }
   // public void ShowSetting()
   // {
   //     settings.gameObject.SetActive(true);
   // }
   // public void ShowOptions()
   // {
   //     options.gameObject.SetActive(true);
   // }
    //public void ShowQuits()
    //{
    //    quits.gameObject.SetActive(true);
    //}
    
    //public void HideStat()
    //{
    //    stat.gameObject.SetActive(false);
    //}
    public void MainMenuCall()
    {
        SceneManager.LoadScene(0);
    }
    void HidePauseMenu()
    {
        transform.GetChild(0).gameObject.SetActive(false);
       // transform.GetChild(1).gameObject.SetActive(false);
    }
    bool isPause = false;
    void ShowPauseMenu()
    {
        isPause = !isPause;

       transform.GetChild(0).gameObject.SetActive(isPause = true ? true:false);
       // transform.GetChild(1).gameObject.SetActive(true);

    }
    public void Quit()
    {
        Application.Quit();
    }
}
