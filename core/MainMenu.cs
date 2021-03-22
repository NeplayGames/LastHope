
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace lastHope.core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] GameObject gameMode; 
       
        [SerializeField] GameObject inventory;
        [SerializeField] GameObject environment;
        [SerializeField] Image image;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] TextMeshProUGUI playBackText;
        [SerializeField] Loader loader;
        [SerializeField] Button yes;
        [SerializeField] Button no;
      
   
        bool showGameMode = false;
        [SerializeField] AudioController audioController;
        private void Start()
        {
          // DontDestroyOnLoad(this.gameObject);

         //  DontDestroyOnLoad(orbitController.gameObject);
        }
        public void ShowGameMode()
        {
            showGameMode = !showGameMode;
            audioController.ButtonClick();
            if (showGameMode)
            {
                gameMode.SetActive(true);
              //  player.SetActive(false);
                playBackText.text = "BACK";
            }
            else
            {
                
                    gameMode.SetActive(false);
                //    player.SetActive(true);
                    playBackText.text = "PLAY";
                
            }
           
        }
      public  Transform selectedPlayer;
        public void PlayGame()
        {
            audioController.ButtonClick();
         //  selectedPlayer =  FindObjectOfType<PlayerManager>().selectedPlayer;
            selectedPlayer.parent = null;
            DontDestroyOnLoad(selectedPlayer);
            loader.OnLoadLevelClick(2);

        }
        public void PlayStoryMode()
        {
            audioController.ButtonClick();
           // selectedPlayer = FindObjectOfType<PlayerManager>().selectedPlayer;
            selectedPlayer.parent = null;
            DontDestroyOnLoad(selectedPlayer);
            loader.OnLoadLevelClick(1);
            //   i = 0;
        }
        public void PlayNalaPani()
        {
            audioController.ButtonClick();
             selectedPlayer.parent = null;
            DontDestroyOnLoad(selectedPlayer);
            loader.OnLoadLevelClick(4);
            //   i = 0;
        }
        public void CheckLoadOut()
        {
            //text.gameObject.SetActive(true);
            //yes.gameObject.SetActive(true);
            //no.gameObject.SetActive(true);
            //text.text = "Check LoadOuts?";

            //i = 2;
        }
        bool inventoryShow = false;
        public void ShowInventory()
        {
            inventoryShow = !inventoryShow;
            audioController.ButtonClick();

            if (inventoryShow)
            {
                inventory.SetActive(true);
             environment.SetActive(false);
              //  FindObjectOfType<PlayerComponents>().transform.position = new Vector3(0, 0, 0);

            }
            else
            {
                  // FindObjectOfType<PlayerComponents>().transform.position = new Vector3(0, -1, 0);

                inventory.SetActive(false);
              environment.SetActive(true);
            }

        }
       
        public void Quit()
        {
            Application.Quit();
        }
    }

}
