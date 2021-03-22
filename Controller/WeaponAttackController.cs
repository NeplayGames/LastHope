
using System.Collections;
using UnityEngine;
using lastHope.core;
using System;
using lastHope.movement;

namespace lastHope.controller
{
    [RequireComponent(typeof(WeaponPickUp))]
    public class WeaponAttackController : MonoBehaviour
    {
        [SerializeField] float weaponRange = 12f;
        public float addFOV = 15f;
        public float timeBetweenAttack = 1f;
        public Sprite customCrossHair;
        [SerializeField] Sprite sniperSprite;
        public Sprite gunSprite;  
        [SerializeField] int amo;
        [SerializeField] int amoCanHold;
        [SerializeField] int totalAmo;
        [SerializeField] AudioClip audioClip;
        [SerializeField] AudioClip reloadClip;
        [SerializeField] AudioClip noAmo;
       
        public float recoil=0.1f;
       // public float recoily = 0.05f;
        public bool reload = false;
        float recoils;
        int gunNo;
        AudioSource audioSource;
        public float damage;
        bool isActive;
        AimScheduler aimScheduler;
        private Weapon_UI weapon_UI;
        float time = 0f;
        public bool sniper = false;
        public bool playerDefaultWeapon = false;
        GameObject muzzleSprite;
        RaycastHit hit;
        public bool pickedUp = false;
        [SerializeField]public bool isKhukuri = false;

        
        public void ShootGun()
        {
            

            if (time <= 0 && !reload)
                {
                if (isKhukuri)
                {
                    playerMoment.animator.SetTrigger("KhukuriAttack");
                }
                if (amo != 0)
                    {
                        muzzleSprite.SetActive(true);
                        Shoot();
                        recoil += recoils;
                    }
                    else if (amo == 0 && totalAmo == 0)
                    {
                        audioSource.clip = noAmo;
                        audioSource.Play();
                    }
                    else
                    {
                        StartCoroutine(Reload());
                        recoil = 0;
                    }
                   // time = 0;
                    Invoke("Deactivate", 0.04f);


                }
                else
                    recoil = 0;
            if (time <= 0)
                time = timeBetweenAttack;
            else
                time -= Time.deltaTime;

        }
        private void Update()
        {
            if (!isActive) return;
            if (isKhukuri)
            {
                recoil = 0;
                if (time >0)
                  
                    time -= Time.deltaTime;
            }
           


            if (sniperSprite != null)
            {
                sniper = true;
                if (Input.GetMouseButton(1))
                {
                    weapon_UI.SetSniperCrossHair(sniperSprite);

                }
                else
                {
                    weapon_UI.HideSniperCross();
                }
            }
               
            if (Input.GetKey(KeyCode.R))
            {
                StartCoroutine(Reload());
            }
        }

        public void ChangeScopeColor()
        {
          hit =  GetHitInfo();
       //     print(hit.collider.name);

            if (hit.collider != null) { 
                if (hit.collider.CompareTag("Enemy"))
                {
                    weapon_UI.ChangeCrossHairColor();
                }

                else if (hit.collider.CompareTag("head"))

                {

                    weapon_UI.ChangeCrossHairColor();

                }
                else if (hit.collider.CompareTag("AIEnemy"))
                {

                    weapon_UI.ChangeCrossHairColor();

                }
                else if (hit.collider.CompareTag("AIEnemyMovingBoard"))
                {
                    weapon_UI.ChangeCrossHairColor();


                }
                else if (hit.collider.CompareTag("AIHead"))
                {
                    weapon_UI.ChangeCrossHairColor();


                }
                else
                    weapon_UI.ChangeCrossHairColorNormal();
            }
            else
                weapon_UI.ChangeCrossHairColorNormal();


        }

        void Deactivate()
        {
            muzzleSprite.SetActive(false);

        }
        PlayerMoment playerMoment;
        private void Start()
        {
            recoils = recoil;
            recoil = 0;
            weapon_UI = FindObjectOfType<Weapon_UI>();
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            aimScheduler = FindObjectOfType<AimScheduler>();
            playerMoment = FindObjectOfType<PlayerMoment>();

            if (!playerDefaultWeapon)
            {
                 muzzleSprite = transform.Find("muzzleSprite").gameObject;
                     muzzleSprite.SetActive(false);
            }
            
        }




        public void WeaponIsActive(bool value,int gunNum)
        {
            isActive = value;
            gunNo = gunNum;
 
            if (isActive)
            {
                weapon_UI.SetCrossHair(customCrossHair);
            }
            else
            {
                weapon_UI.HideUI();
            }
        }
        public void HideAmmoUI(int i)
        {
            weapon_UI.HideAmmoText(i);
        }
        public void HandleUI()
        {
            weapon_UI.Update_UI(amo, totalAmo,gunNo);
        }
        
        IEnumerator Reload()
        {
            reload = true;
            aimScheduler.Reloading(reload);
            audioSource.clip = reloadClip;
            audioSource.Play();
           
            yield return new WaitForSeconds(3.34f);
            
             if (amoCanHold <= (totalAmo+amo))
            {
                totalAmo += amo;
                amo = 0;
                totalAmo -= amoCanHold;
                amo += amoCanHold;
                reload = false;

            }
            else
            {
                amo += totalAmo;
                totalAmo = 0;
                reload = false;

            }
            aimScheduler.Reloading(reload);
            HandleUI();
        }

      
        public virtual void  Shoot()
        {
            if (!isKhukuri)
            {
                amo -= 1;
                HandleUI();

            }
            audioSource.clip = audioClip;
                audioSource.Play();
          
        }
        public RaycastHit GetHitInfo()
        {
            Vector3 placement= new Vector3(0.5f, 0.5f, 0f);
            Ray ray = Camera.main.ViewportPointToRay(placement);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, weaponRange))
            {
                return hit;
            }
            return hit;
        }
        }
    }
   
    


