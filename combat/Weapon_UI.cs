
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace lastHope.core
{
    public class Weapon_UI : MonoBehaviour
    {
        public TextMeshProUGUI[] gunAmmoText = new TextMeshProUGUI[3];
       // public Image[] image = new Image[3];

        public Image crossHair;
        public Image sniperCrossHair;
        [SerializeField] float widthDeduction = 1000f;
        [SerializeField] float heightDeduction = 400f ;
      
        readonly float[] gunPosX = new float[3] { 0.351f, 0.52f, 0.69f };
        readonly float gunPosY = 0.004f;
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                gunAmmoText[i].GetComponent<RectTransform>().localScale = new Vector3(Screen.width / widthDeduction, Screen.height / heightDeduction, 1);
                gunAmmoText[i].transform.position = new Vector3(Screen.width * gunPosX[i], Screen.height * gunPosY, 1);


            }
            HideAllAmmo();
            HideUI();
        }

       
        //public void Update_UI(string msg)
        //{
        //    if (!ammoText.gameObject.activeInHierarchy)
        //        ammoText.gameObject.SetActive(true);
        //    ammoText.text = msg;
        //}
        // Update is called once per frame
        public void Update_UI(int ammo, int maxAmo, int i)
        {

                  switch (i)
            {
                case 0:
                    {
                        if (!gunAmmoText[0].gameObject.activeInHierarchy)
                            gunAmmoText[0].gameObject.SetActive(true);
                        gunAmmoText[0].text = ammo.ToString() + " / " + maxAmo.ToString();
                     
                    }
                    break;
                case 1:
                    {
                        if (!gunAmmoText[1].gameObject.activeInHierarchy)
                            gunAmmoText[1].gameObject.SetActive(true);
                        gunAmmoText[1].text = ammo.ToString() + " / " + maxAmo.ToString();
                       // image[1].color = new Color32(0, 0, 0, 180);

                    }
                    break;
                case 2:
                    {
                        if (!gunAmmoText[2].gameObject.activeInHierarchy)
                            gunAmmoText[2].gameObject.SetActive(true);
                        gunAmmoText[2].text = ammo.ToString() + " / " + maxAmo.ToString();
                       // image[2].color = new Color32(0, 0, 0, 180);

                    }
                    break;
                default:
                    break;
            }
          
        }
        public void SetCrossHair(Sprite _sprite)
        {
            if (!crossHair.gameObject.activeInHierarchy)
                crossHair.gameObject.SetActive(true);
            crossHair.sprite = _sprite;

        }
        public void ChangeCrossHairColor()
        {
            crossHair.color = new Color(0.6f, 0, 0, 0.6f);
        }
        public void ChangeCrossHairColorNormal ()
        {
            crossHair.color = new Color(1f, 1f, 1f, 1f);
        }
        public void HideAmmoText(int num)
        {
            gunAmmoText[num].gameObject.SetActive(false);
          //  image[num].color = Color.white;
        }
        public void HideAllAmmo()
        {
            gunAmmoText[0].gameObject.SetActive(false);
            gunAmmoText[1].gameObject.SetActive(false);
            gunAmmoText[2].gameObject.SetActive(false);
        }
        public void HideUI()
        {
          
            crossHair.gameObject.SetActive(false);
            sniperCrossHair.gameObject.SetActive(false);
        }
        public void HideSniperCross()
        {
            sniperCrossHair.gameObject.SetActive(false);
            crossHair.gameObject.SetActive(true);

        }
        public void SetSniperCrossHair(Sprite snipCrossHair)
        {
            if (!sniperCrossHair.gameObject.activeInHierarchy)
            {
                sniperCrossHair.gameObject.SetActive(true);
                sniperCrossHair.sprite = snipCrossHair;
                crossHair.gameObject.SetActive(false);
            }

        }
    }

}
