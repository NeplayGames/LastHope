using lastHope.controller;
using lastHope.core;

using UnityEngine;

public class PointWeapon : WeaponAttackController
{
    public LineRenderer lineRenderer;
    public GameObject blood;
    bool regularHit = false;
    int i = 0;
   public override void Shoot()
    {
        
        base.Shoot();
        RaycastHit hit = GetHitInfo();
        GameObject bulletTrailEffect = Instantiate(lineRenderer.gameObject, this.transform.GetChild(2).position, Quaternion.identity);
        LineRenderer line = bulletTrailEffect.GetComponent<LineRenderer>();
        line.SetPosition(0, this.transform.GetChild(0).position);
        if (hit.point == Vector3.zero) line.SetPosition(1, this.transform.GetChild(2).forward * 500);else line.SetPosition(1, hit.point);

        Destroy(bulletTrailEffect, 1f);
        if(hit.collider!=null)
        if (hit.collider.CompareTag("Enemy"))
        {
            i++;
            regularHit = i > 1;
                Instantiate(blood, hit.point, transform.rotation);

                hit.collider.GetComponent<AIHealth>().HealthDamage(damage,regularHit);
        }
      
        else if(hit.collider.CompareTag("head"))
    
        {
            i++;
            regularHit = i > 1;
            hit.collider.transform.parent.GetComponent<AIHealth>().HealthDamage(damage*2f,regularHit); Instantiate(blood, hit.point, transform.rotation);
            }
        else if ( hit.collider.CompareTag("AIEnemy"))
        {
            i++;
            regularHit = i > 1;
                Instantiate(blood, hit.point, transform.rotation);
                hit.collider.GetComponent<AIHealth>().HealthDamage(damage, regularHit);

        }
        else if ( hit.collider.CompareTag("AIEnemyMovingBoard"))
        {
            i++;
            regularHit = i > 1;
                Instantiate(blood, hit.point, transform.rotation);
                hit.collider.GetComponent<AIHealthMovingBoard>().HealthDamage(damage, regularHit);

        }
        else if (hit.collider.CompareTag("AIHead"))
        {
            hit.collider.transform.parent.GetComponent<AIHealth>().HealthDamage(damage * 2f, regularHit); Instantiate(blood, hit.point, transform.rotation);
            }

        else
        {
            i = 0;

        }
    }
}
