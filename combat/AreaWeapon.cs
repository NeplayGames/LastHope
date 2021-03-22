using lastHope.controller;
using lastHope.core;
using UnityEngine;

public class AreaWeapon : WeaponAttackController
{
    bool regularHit = false;
    int i = 0;
    public float blastRadius;
    public GameObject blood;
    Vector3 point = Vector3.zero;
    public override void Shoot()
    {
        base.Shoot();
        RaycastHit hit = GetHitInfo();

        if (transform.CompareTag("Player"))
        {
            point = transform.position;
        }
        else
        {
            point = hit.point;
        }

        Collider[] col = Physics.OverlapSphere(point, blastRadius);

        for(int j =0; j< col.Length; j++)
        {
            if (col[j] != null && col[j].CompareTag("Enemy"))
            {
                i++;
                regularHit = i > 1;

                Instantiate(blood, hit.point, transform.rotation);
                col[j].GetComponent<AIHealth>().HealthDamage(damage, regularHit);
            }

            else if (col[j] != null && col[j].CompareTag("head"))

            {
                i++;
                regularHit = i > 1;
                col[j].transform.parent.GetComponent<AIHealth>().HealthDamage(damage, regularHit);
                Instantiate(blood, hit.point, transform.rotation);
            }
             if (col[j] != null && col[j].CompareTag("AIEnemyMovingBoard"))

            {
                i++;
                regularHit = i > 1;
                col[j].GetComponent<AIHealthMovingBoard>().HealthDamage(damage, regularHit);
                Instantiate(blood, hit.point, transform.rotation);
            }
             if (col[j] != null && col[j].CompareTag("AIHead"))
            {
                i++;
                regularHit = i > 1;
        
                col[j].transform.parent.GetComponent<AIHealth>().HealthDamage(damage, regularHit);
                Instantiate(blood, hit.point, transform.rotation);
            }
            else if (col[j] != null && col[j].CompareTag("AIEnemy"))
            {
                col[j].GetComponent<AIHealth>().HealthDamage(damage, regularHit); Instantiate(blood, hit.point, transform.rotation);
            }

            else
            {
                i = 0;

            }
        }
        
    }
}
