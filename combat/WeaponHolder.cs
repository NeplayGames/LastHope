using lastHope.controller;
using lastHope.core;
using lastHope.movement;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
  
    public Transform hand;
    public WeaponAttackController weaponActive = null;
     GameObject[] Weapon = new GameObject[3];
    GameObject temp;
    public int i = 0;
        private int k = 0;
    bool aiming=false;
    [SerializeField]AimScheduler aimScheduler;
    [SerializeField] Image[] gun = new Image[3];
    public GameObject pickItem;
    PlayerComponents playerComponents;
    PlayerMoment playerMoment;
    private void Start()
    {
        playerMoment = FindObjectOfType<PlayerMoment>();
        aimScheduler = transform.GetComponent<AimScheduler>();
        playerComponents = FindObjectOfType<PlayerComponents>();
        hand = playerComponents.hand;
    }
    public bool pickUp = false;
    public void Shoot()
    {
        print("Shoot");
        if (weaponActive != null)
        {
            weaponActive.ShootGun();
           
        }
       
    }
    public void ChooseGun(int j)
    {
       
        if (Weapon[j] == null)
            {
                return;
            }
            weaponActive = Weapon[j].GetComponent<WeaponAttackController>();

            if (j == k)
            {
                if (Weapon[j].gameObject.activeInHierarchy)
                {
                if (weaponActive.isKhukuri)
                {

                    playerMoment.animator.SetTrigger("movement");

                   
                }
                Weapon[j].gameObject.SetActive(false);
                    gun[j].color = new Color32(0, 0, 0, 50);
                    weaponActive.WeaponIsActive(false, j);
                    aiming = false;
                
            }
                else
                {

                    aiming = true;
                    Weapon[j].gameObject.SetActive(true);
                    gun[j].color = new Color32(0, 0, 0, 180);
                    weaponActive.WeaponIsActive(true, j);
                    weaponActive.HandleUI();


                }


            }

            else if (Weapon[k] != null && Weapon[k].gameObject.activeInHierarchy)
            {
                Weapon[k].SetActive(false);
                gun[k].color = new Color32(0, 0, 0, 50);
                weaponActive.WeaponIsActive(true, j);
                weaponActive.HandleUI();

                Weapon[j].gameObject.SetActive(true);
                gun[j].color = new Color32(0, 0, 0, 180);
                aiming = true;
            }
            else
            {
                weaponActive.WeaponIsActive(true, j);
                weaponActive.HandleUI();


                Weapon[j].gameObject.SetActive(true);
                gun[j].color = new Color32(0, 0, 0, 180);

                aiming = true;
            }
        playerMoment.isKhukuri = weaponActive.isKhukuri;
        if (weaponActive.isKhukuri)
        {
            print(weaponActive.isKhukuri);
            playerMoment.animator.SetTrigger("khukuri");

            playerMoment.animator.ResetTrigger("aim");
        }

        else
        {
            playerMoment.animator.SetTrigger("aim");
            playerMoment.animator.ResetTrigger("khukuri");
        }
            k = j;
        
    }
    private void Update()
    {
       
        GunSpriteHandle();
      if(Weapon[k]!= null && Weapon[k].gameObject.activeInHierarchy && !weaponActive.isKhukuri)
        {
            Weapon[k].transform.LookAt(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 100)));
        }

        aimScheduler.Aiming(aiming);
        if (transform.GetComponent<Health>().IsDead())
        {
            return;
        }
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    Weapon[k].GetComponent<WeaponAttackController>().HideAmmoUI(k);
        //    gun[k].color= new Color32(0, 0, 0, 50);
        //    temp = Weapon[k];
        //    Weapon[k] = null;

        //    DropWeapon();
        //    aiming = false;
        //    i = k;
        //}
     
        if (aiming)
        {
            Weapon[k].GetComponent<WeaponAttackController>().ChangeScopeColor();
        }
      
    }
    public void HideInfo()
    {
        for (int i = 0; i < 3; i++)
            gun[i].gameObject.SetActive(false);
    }
    public void ShowInfo()
    {
        for (int i = 0; i < 3; i++)
            gun[i].gameObject.SetActive(true);
    }
    public void GunSpriteHandle()
    {
       

        for (int i = 0; i < 3; i++)
        { 

            if (Weapon[i] != null)
            {

               
                gun[i].sprite = Weapon[i].GetComponent<WeaponAttackController>().gunSprite;
            }
        }
    }
    public void SHowMessage() { print("TOuch"); }

    public void PickUpWeapon()
    {
    
        aiming = true;
    
        if (Weapon[k] != null)
        {
            Weapon[k].SetActive(false);
            gun[k].color = new Color32(0, 0, 0, 50);

        }
        if (hand.childCount == 4)
        {
            temp = Weapon[k];
            DropWeapon();
            gun[k].color = new Color32(0, 0, 0, 50);

            Weapon[k] = pickItem;
            weaponActive = Weapon[k].GetComponent<WeaponAttackController>();
            
        }

        else
        {
            Weapon[i] = pickItem;
            gun[i].color = new Color32(0, 0, 0, 180);
            k = i;
            weaponActive = Weapon[i].GetComponent<WeaponAttackController>();



        }
        weaponActive.WeaponIsActive(true,k);
        weaponActive.HandleUI();
        weaponActive.pickedUp = true;
        playerMoment.isKhukuri = weaponActive.isKhukuri;
        pickItem.SetActive(true);

        pickItem.transform.SetParent(hand);
       // weapon.transform.rotation = hand.rotation;
        pickItem.transform.position = hand.position;
        if (weaponActive.isKhukuri)
        {
            playerMoment.animator.ResetTrigger("aim");

            playerMoment.animator.SetTrigger("khukuri");    
            pickItem.transform.localPosition = new Vector3(0.051f, 0.008f, 0.012f);
            pickItem.transform.localRotation = Quaternion.Euler(new Vector3(17.83f, -74, -13.55f));
        }
        else
        {
            pickItem.transform.localPosition = new Vector3(0.06f, -0.02f, 0.02f);
            playerMoment.animator.SetTrigger("aim");
            playerMoment.animator.ResetTrigger("khukuri");
        }

        //  weapon.transform.localRotation = Quaternion.Euler(rotate);



        for (int r = 0; r < 3; r++)
            if (Weapon[r] == null)
            {
                i = r;
                break;

            }

    }
    public void DropWeapon()
    {
        if (temp != null)
        {
            temp.GetComponent<WeaponPickUp>().DropWeapon();          
       }
    }
}
