using lastHope.core;
using lastHope.movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionHelper : MonoBehaviour
{
    [SerializeField] GameObject directionalArrow;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform parent;
    [SerializeField] Text text;
    [SerializeField] Text gameInfo;
    [SerializeField]public string missionInfo;
    [SerializeField] public string missionComplete;
      [SerializeField] Text missionDetails;
    Transform child;
 Transform playerForwardPositon;
    float distance;
    Transform player;
    int totalChild;
    void Start()
    {
        missionDetails = FindObjectOfType<MIssionInfo>().GetComponent<Text>();
        missionDetails.text = missionInfo;
        player = FindObjectOfType<PlayerMoment>().transform;
        child = parent.GetChild(0);
       directionalArrow.transform.position = Camera.main.WorldToScreenPoint(child.position);
        distance = Vector2.Distance(new Vector2(child.position.x, child.position.z), new Vector2(player.position.x, player.position.z));
        playerForwardPositon = FindObjectOfType<OrbitController>().transform.GetChild(0);
        tempDis = (int)distance;
        text.text = tempDis + "";
        totalChild = parent.childCount;
        
       
       
        // directionalArrow.transform.position = child.position;
    }
    int i=0;
    public void ShowAreaMarker()
    {

        directionalArrow.SetActive(true);
     
       

        

    }
    bool finished = false;
    int tempDis;
    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            directionalArrow.transform.position = Camera.main.WorldToScreenPoint(child.position);
        if (Vector3.Distance(child.position, playerForwardPositon.position) > Vector3.Distance(child.position, player.position))
            arrow.SetActive(true);
        else
            arrow.SetActive(false);

        distance = Vector2.Distance(new Vector2(child.position.x, child.position.z), new Vector2(player.position.x, player.position.z));
        if(tempDis != (int)distance)
        {
            tempDis = (int)distance;
            text.text = tempDis + "";

        }

            if (distance < 3f)
            {
                gameInfo.gameObject.SetActive(true);

                gameInfo.text = child.GetComponent<TextHolder>().info;
                TextInfo();
                //  Destroy(child.gameObject);
                if (i == totalChild - 2)
                {
                    directionalArrow.SetActive(false);
                  //  print(distance);
                    FindObjectOfType<MissionHelper>().MissionComplete();
                    finished = true;
                    return;

                }


                else
                    i++;
              
             
                //if(i == parent.childCount)
                //{

                //    directionalArrow.SetActive(false);
                //    this.gameObject.SetActive(false);
                //    return;
                //}
                child = parent.GetChild(i);
                distance = Vector2.Distance(new Vector2(child.position.x, child.position.z), new Vector2(player.position.x, player.position.z));
                tempDis = (int)distance;

                directionalArrow.transform.position = child.position;
            }
        }
        }
    public void TextInfo()
    {

        StartCoroutine(DeactivateText());
    }

    IEnumerator DeactivateText()
    {
        yield return new WaitForSeconds(5f);
        gameInfo.gameObject.SetActive(false);
    }
}
       
