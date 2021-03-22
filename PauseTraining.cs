using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTraining : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    bool isPaused = false;
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
            }
            Time.timeScale = isPaused ? 0 : 1;
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            ShowPauseMenu();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            HidePauseMenu();
         

        }
        void HidePauseMenu()
        {
            transform.GetChild(0).gameObject.SetActive(false);
          
        }
        void ShowPauseMenu()
        {
            transform.GetChild(0).gameObject.SetActive(true);
       

        }
    }
}
