using lastHope.core;
using lastHope.movement;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
     Slider slider;
    Transform player;
    [SerializeField] Gradient gradient;
    [SerializeField] Image healthBar;
    Health health;
    float hth;
    private void Start()
    {
        player = FindObjectOfType<PlayerMoment>().transform ;
        health = player.GetComponent<Health>();
        slider = transform.GetChild(0).GetComponent<Slider>();
        hth = health.health;
        slider.value = health.health;
        healthBar.color = gradient.Evaluate(1f);
       
    }
    public void HealthBar()
    {
        slider.value = health.health;
        healthBar.color = gradient.Evaluate(health.health / hth);
    }
    public void HideBar()
    {
        if(slider.gameObject.activeInHierarchy)
        slider.gameObject.SetActive(false);
    }
    public void ShowBar()
    {
        if(!slider.gameObject.activeInHierarchy)
        slider.gameObject.SetActive(true);

    }
}
