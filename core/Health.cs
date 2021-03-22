using lastHope.movement;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace lastHope.core
{
    public class Health : MonoBehaviour
    {
       // [SerializeField] AudioClip hurtAudio;
       // [SerializeField] PlayerMoment audioSource;
        [SerializeField] HealthBarScript healthBarScript;
       // [SerializeField] Canvas damageImage;
     
        public float health = 100f;
        bool isAnimated = false;
        bool isDead = false;
     
        [SerializeField] float waitTime;

       
        public void HealthDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
                healthBarScript.HealthBar();
            
            if (health == 0)
            {
                Die();
            }
        }

       
        void Die()
        {
            isDead = true;
            if (isAnimated == true) return;
           transform.GetChild(0).GetComponent<Animator>().SetTrigger("death");
           
            isAnimated = true;
        }
        public bool IsDead()
        {
                
            return isDead;
        }
    }

}