using lastHope.controller;
using lastHope.core;
using lastHope.movement;
using System;
using System.Collections;
using UnityEngine;

namespace lastHope.combat
{
    public class AIFighter : MonoBehaviour,ChangeAction
    {
        [SerializeField] int amo;
        [SerializeField] int amoCanHold;
        [SerializeField] int totalAmo;
        [SerializeField] GameObject blood;
        Vector3 rotate = new Vector3(-77f, 70f, 21f);
        Vector3 position = new Vector3(1.69f, 0.31f, 0.14f);
        AIHealth health;
        Health targeEnemy;
        public float weaponRange = 1f;
        public float weaponDamage = 5f;
        //reload time of the weapon
        public float[] timeBetweenAttacks = { 2.17f, 1.5f, 3.2f, 0.7f, 2.2f };
        float timeBetweenAttack = 1.5f;
        Movement movement;
        Animator animator;
       float  distortion = 0f;
        float timeSinceLastAttack = 0f;
        [SerializeField] Transform weapon;
        [SerializeField] AudioClip aud;
        AudioSource audioSource;
        [SerializeField] LayerMask player;
        [SerializeField] LayerMask enemy;
        [SerializeField] float y;
        [SerializeField] AudioClip reloadClip;
        [SerializeField] AudioClip noAmo;
        [SerializeField] float reloadTime;
     
       
        bool reloading;
        private void Start()
        {
            if (weapon != null)
            {
                weapon.transform.localPosition = new Vector3(0.58f, 0.39f, 0.99f);

                weapon.transform.localRotation = Quaternion.Euler(rotate);
                weapon.gameObject.SetActive(false);

            }
            audioSource = GetComponent<AudioSource>();
            movement = GetComponent<Movement>();
            animator = transform.Find("main").GetComponent<Animator>();
            health = GetComponent<AIHealth>();
            RaycastHit hit;

            if (Physics.Raycast(transform.position + Vector3.up * 5, -200 * Vector3.up, out hit, 200) && !hit.collider.CompareTag("water")) transform.position = hit.point;
            else if (Physics.Raycast(transform.position + Vector3.up * 5, +200 * Vector3.up, out hit, 200) && !hit.collider.CompareTag("water")) transform.position = hit.point;
            else if (Physics.Raycast(transform.position + Vector3.forward, +200 * Vector3.forward, out hit, 200) && !hit.collider.CompareTag("water")) transform.position = hit.point;
            else if (Physics.Raycast(transform.position + Vector3.forward, -200 * Vector3.forward, out hit, 200) && !hit.collider.CompareTag("water")) transform.position = hit.point;
            else Destroy(transform.gameObject);
        }
        public void CancelAction()
        {
            targeEnemy = null;
        }
        public void Attack(GameObject target)
        {
            targeEnemy = target.GetComponent<Health>();
            GetComponent<Scheduler>().StartAction(this);
            
        }
        
        private void TriggerAttack()
        {
          
           
            audioSource.clip = aud;
            audioSource.Play();
        }
        private void AttackBehaviour()
        {
            if (reloading) return;
            weapon.gameObject.SetActive(true);
          
           // transform.LookAt(targeEnemy.transform);
            weapon.LookAt(targeEnemy.transform);
            timeBetweenAttack = timeBetweenAttacks[UnityEngine.Random.Range(0, 4)];
            if (timeSinceLastAttack < timeBetweenAttack) return;
            TriggerAttack();
           

          
            if (Physics.Raycast(weapon.position, weapon.forward * 100 + weapon.right * UnityEngine.Random.Range(0, 7), out RaycastHit hit, 100))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //weapon.LookAt(targeEnemy.transform.position + transform.up);
                    Shoot();
                    Instantiate(blood, hit.point, transform.rotation);

                    // muzzleSprite.SetActive(true);

                    //  DeactivateMuzzleSprite();
                }
            }
           
          
            timeSinceLastAttack = 0f;
        }

        //private void DeactivateMuzzleSprite()
        //{
        //    Invoke("Deactivate", 0.04f);
        //}

        //void Deactivate()
        //{
        //    muzzleSprite.SetActive(false);

        //}
        IEnumerator Reload()
        {
            reloading = true;
            audioSource.clip = reloadClip;
            audioSource.Play();
        
            yield return new WaitForSeconds(reloadTime);

            if (amoCanHold <= totalAmo)
            {
                totalAmo += amo;
                amo = 0;
                totalAmo -= amoCanHold;
                amo += amoCanHold;
              

            }
            else
            {
                amo += totalAmo;
             

            }
            reloading = false;
        }
        private void Shoot()
        {
            if(amo == 0)
            {
                StartCoroutine(Reload());
            }
            else
            {
             
                targeEnemy.HealthDamage(weaponDamage);

                amo -= 1;
           
            }
          
        }
        Quaternion rotationAngle;
        bool IsInAttackRange()
        {

            weapon.transform.localPosition = position;

            weapon.transform.localRotation = Quaternion.Euler(rotate);
            //transform.LookAt(targeEnemy.transform);
             rotationAngle = Quaternion.LookRotation(targeEnemy.transform.position - transform.position); // we get the angle has to be rotated
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * 5);
            weapon.LookAt(targeEnemy.transform);
          //  Debug.DrawRay(weapon.position, weapon.forward * 200, Color.red, 2f);
                
            if (Vector3.Distance(targeEnemy.transform.position, transform.position) < weaponRange )
            {
            
                if (Physics.Raycast(weapon.position, weapon.forward * 200 , out RaycastHit hit, 200, enemy))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        animator.SetTrigger("locomotionToAttack");
                        return Vector3.Distance(targeEnemy.transform.position, transform.position) < weaponRange;

                    }

                   

                        movement.MoveToDestination(hit.collider.transform.position, 3.5f, true);
                        return false;
                    }
                    else
                        return false;
              
            }

               
            else
                return false;
        }
        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;
            Health targetToTest = target.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        private void Update()
        {
            if (targeEnemy == null) return;

            if (targeEnemy.IsDead()) return;
            if (health.IsDead()) return;
            if (IsInAttackRange())
            {
                AttackBehaviour();
                animator.SetFloat("aimingFloat", 0f);
               
                movement.CancelAction();
                timeSinceLastAttack += Time.deltaTime;

            }
            else
            {
                animator.SetTrigger("movement");    
                weapon.gameObject.SetActive(false);
                //animationPoint = 1.5f;
                movement.MoveToDestination(targeEnemy.transform.position,3.5f,true);

            }
        }
    }

}
