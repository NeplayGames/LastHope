
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AIHealthMovingBoard : MonoBehaviour
{
    [SerializeField] float health = Mathf.Infinity;
    bool isAnimated = false;
    float total = 0;
    [SerializeField] TextMeshProUGUI damag;
   
    SpriteRenderer img2;
    Camera cam;
    [SerializeField] float waitTime;
    float currentTime;
    Transform damageDisplay;
    private void Awake()
    {
       
        img2 = transform.Find("range").GetComponent<SpriteRenderer>();
        damageDisplay = transform.Find("damageDisplayPlaceHolder");

    }
    private void Start()
    {
        if (damag != null)
        {
            damag.gameObject.SetActive(false);
            cam = Camera.main;
        }

    }
    private void LateUpdate()
    {
        if (damag != null)
        {
            currentTime += Time.deltaTime;

            if (damag.gameObject.activeInHierarchy)
            {
                Vector3 pos = cam.WorldToScreenPoint(damageDisplay.position);
                damag.transform.position = pos + new Vector3(70f, 0, 0);
            }
            if (currentTime >= waitTime)
            {

                damag.gameObject.SetActive(false);
            }
            if (currentTime >= waitTime / 2)
            {
                img2.color = new Color32(255, 255, 255, 255);

             
            }
        }

    }
    public void HealthDamage(float damage, bool reqularHit)
    {

        health = Mathf.Max(health - damage, 0);
        UpdateDamage(damage, reqularHit);
        if (health == 0)
        {
            Die();
        }

    }
    private void UpdateDamage(float damage, bool reqularHit)
    {
     
        img2.color = new Color32(183, 0, 0, 255);
        if (currentTime >= waitTime) reqularHit = false;
        damag.gameObject.SetActive(true);
        if (reqularHit)
        {
            total += damage;
            damag.text = "-" + total.ToString();
            currentTime = 0;
        }
        else
        {
            damag.text = "-" + damage.ToString();
            currentTime = 0;
            total = 0;
        }


    }
    void Die()
    {

        if (isAnimated == true) return;
        // transform.Find("main").GetComponent<Animator>().SetTrigger("death");

        isAnimated = true;
    }


}
