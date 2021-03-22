
using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class AIHealth : MonoBehaviour
{

    [SerializeField] float health = Mathf.Infinity;
    bool isAnimated = false;
    public bool getHits = false;
    float total = 0;
    [SerializeField] TextMeshProUGUI damageText;
    Camera cam;
    [SerializeField] float waitTime;
    float currentTime;
    Transform damageDisplay;
    bool isDead = false;
    private void Start()
    {
        if (damageText != null)
        {
            damageText.gameObject.SetActive(false);
            cam = Camera.main;
            damageDisplay = transform.Find("damageDisplayPlaceHolder");
        }
      
    }
    public bool IsDead()
    {

        return isDead;
    }
    private void LateUpdate()
    {
        if (damageText != null&& !isDead)
        {
            currentTime += Time.deltaTime;
           
            if (damageText.gameObject.activeInHierarchy)
            {
                Vector3 pos = cam.WorldToScreenPoint(damageDisplay.position);
                damageText.transform.position = pos + new Vector3(70f, 0, 0);
            }
                if (currentTime >= waitTime)
            {
               
                damageText.gameObject.SetActive(false);
            }
          
            }

    }
    public void HealthDamage(float damage,bool reqularHit)
    {
        getHits = true;

        health = Mathf.Max(health - damage, 0);
        UpdateDamage(damage, reqularHit);
        if (health == 0)
        {
            isDead = true;
            Die();
        }

    }
    private void UpdateDamage(float damage, bool reqularHit)
    {
       
        if (currentTime >= waitTime) reqularHit = false;
        damageText.gameObject.SetActive(true);
        if (reqularHit)
        {
            total += damage;
            damageText.text = "-" + total.ToString();
            currentTime = 0;
        }
        else
        {
            damageText.text = "-" + damage.ToString();
            currentTime = 0;
            total = 0;
        }


    }
    void Die()
    {
      
        if (isAnimated == true) return;
        if(transform.CompareTag("Enemy"))
        transform.Find("main").GetComponent<Animator>().SetTrigger("death");
        StartCoroutine(ProvideMaterial());
        isAnimated = true;
    }
    [SerializeField] GameObject Gun;
    IEnumerator ProvideMaterial()
    {
        yield return new WaitForSeconds(4f);
        Instantiate(Gun, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
