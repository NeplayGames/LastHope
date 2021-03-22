using lastHope.core;
using lastHope.movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerMale;
    [SerializeField] Transform playerFemale;
    private Vector2 initialPosition;
    PlayerMoment playerMoment;
    Vector2 direction;
    float tempValue;
    public bool isMale = true;
    [SerializeField] MainMenu mainMenu;
    public void MaleCharacters()
    {
        isMale = true;
        ChangeItemMale(0);
    }
    public void FemaleCharacters()
    {
        isMale = false;
        ChangeItemFemale(0);
    }
    // Start is called before the first frame update
    void Start()
    {
       // playerMoment = FindObjectOfType<PlayerMoment>();
       // player = playerMoment.gameObject;
        selectedPlayer = FindObjectOfType<PlayerComponents>().transform;
        mainMenu.selectedPlayer = selectedPlayer;
        this.gameObject.SetActive(false);

        //FindObjectOfType<PlayerComponents>().transform.localPosition = new Vector3(0, -1, 0);
    }
    Touch touch;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.touches[0];


            //if (!CheckUIObjectsInPosition(Input.GetTouch(0).position))
            //{

            //}
            //else
            if (touch.phase == TouchPhase.Began)
            {
                initialPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {

                direction = touch.position - initialPosition;

            }

        }
        else if (direction.x != tempValue)
        {

            if(isMale)
            ChangeItemMale(-(int)Mathf.Sign(direction.x));
            else
                ChangeItemFemale(-(int)Mathf.Sign(direction.x));
        
                tempValue = direction.x;
        }
    }
    int currentItem = 0;
    public Transform selectedPlayer;

    public void ChangeItemMale(int changeValue)
    {






        currentItem += changeValue;


        if (currentItem > playerMale.childCount - 1) currentItem = 0;
        else if (currentItem < 0) currentItem = playerMale.childCount - 1;


        if (selectedPlayer)
        {



           selectedPlayer.gameObject.SetActive(false);
        }


        if (playerMale.GetChild(currentItem))
        {

            selectedPlayer = playerMale.GetChild(currentItem);
            mainMenu.selectedPlayer = selectedPlayer;
            selectedPlayer.position = new Vector3(97f, 9.7f, 213.2f);
            selectedPlayer.gameObject.SetActive(true);
            
        }

    }
    public void ChangeItemFemale(int changeValue)
    {

       


        currentItem += changeValue;


        if (currentItem > playerFemale.childCount - 1) currentItem = 0;
        else if (currentItem < 0) currentItem = playerFemale.childCount - 1;


        if (selectedPlayer)
        {



            selectedPlayer.gameObject.SetActive(false);
        }


        if (playerFemale.GetChild(currentItem))
        {

            selectedPlayer = playerFemale.GetChild(currentItem); 
            selectedPlayer.position = new Vector3(97f, 9.7f, 213.2f);
            selectedPlayer.gameObject.SetActive(true);

        }

    }
}
