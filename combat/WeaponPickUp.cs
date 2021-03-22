using lastHope.controller;
using System;
using System.Collections;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField]private WeaponHolder holder;
    private WeaponAttackController weaponAttackController;
    Quaternion rotation;
  
    float pos;
    GameInfoProvider gameInfo;
    private void Start()
    {
        gameInfo = FindObjectOfType<GameInfoProvider>();
        rotation = transform.rotation;
        pos = transform.position.y;
        weaponAttackController = GetComponent<WeaponAttackController>();
        holder = FindObjectOfType<WeaponHolder>();
    }

    public void LateUpdate()
    {

        // print(Vector3.Distance(transform.position, holder.transform.position));
        if (weaponAttackController.pickedUp) return;
        if (Vector3.Distance(transform.position, holder.transform.position) < 1.7f && (holder.transform.position != transform.position))
        {
           // gameInfo.showInfo++;
            gameInfo.info = "Pick Up";
            gameInfo.button = "E";
            gameInfo.showInfo++;
            holder.pickItem = transform.gameObject;
            

        }

        
      
        
    }
    
   

    public void DropWeapon()
    {
        transform.gameObject.SetActive(true);

        print(rotation);
        transform.parent = null;
        transform.position = new Vector3(transform.position.x,pos,transform.position.z);

        transform.rotation = rotation;
        weaponAttackController.WeaponIsActive(false,4);
        StartCoroutine(Pick());
    }
    IEnumerator Pick()
    {
       yield return new WaitForEndOfFrame();
       weaponAttackController.pickedUp = false;

    }
   
}
