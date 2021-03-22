using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lastHope.movement;
public class AreaMarker : MonoBehaviour
{
    public Vector3 targetPostion;
    Transform player;
    [SerializeField] Transform playerForwardPositon;
    [SerializeField] GameObject point;
    // Vector3 temp;
    float s = 0;
    [SerializeField] float temp;
    [SerializeField] float temp1;
    [SerializeField] float temp2;
    Camera cam;
    // Start is called before the first frame update
    private void Start()
    {
      //  player = FindObjectOfType<PlayerMoment>().transform;
        transform.GetComponent<RectTransform>().localScale = new Vector3(Screen.width / 1800f, Screen.width / 1800f);
        cam = Camera.main;
        player = cam.transform;

    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
    // Update is called once per frame
    void Update()
    {

        Vector2 x = new Vector2(player.position.x, player.position.z);
        Vector2 y = new Vector2(targetPostion.x, targetPostion.z);
        Vector2 b = new Vector2(playerForwardPositon.position.x, playerForwardPositon.position.z);

        point.transform.position = Camera.main.WorldToScreenPoint(targetPostion);

        float tempf = AngleBetweenVector2(x, y);
        float tempb = AngleBetweenVector2(x, b);

        //print(tempb - tempf); 
        point.SetActive(true);
        if (temp < tempb - tempf && tempb - tempf < temp1)
        {
            s = 3;
            // transform.gameObject.SetActive(false);
        }
        else if (temp1 < tempb - tempf && tempb - tempf < temp2)
        {

            transform.rotation = Quaternion.Euler(0, 0, 0);

            s = 0.97f;


        }
        else if (temp > tempb - tempf || 315 < tempb - tempf)
        {


            transform.rotation = Quaternion.Euler(0, 0, 180);

            s = 0.03f;
        }

        transform.position = new Vector3(Screen.width * s, cam.WorldToScreenPoint(new Vector3(0, targetPostion.y, 0)).y, 0);
      if (Vector3.Distance(targetPostion, playerForwardPositon.position) > Vector3.Distance(targetPostion, player.position))
         point.SetActive(false);
}
    //  else
    //  {
    //  transform.position = new Vector3(   Screen.width * 0.5f, Screen.height *0.9f, 0);
    //transform.position = new Vector3(   cam.WorldToScreenPoint(targetPostion).x, Screen.height *0.9f, 0);
    // transform.rotation = Quaternion.Euler(0, 0, 90);
    //  }



    //else if (DotResult > 0)
    //{
    //    transform.rotation = Quaternion.Euler(0,0,180);

    //    s = 0.1f;


    //}
    //else if (DotResult < 0)
    //{
    //    transform.rotation = Quaternion.Euler(0, 0, 0);

    //    s = 0.9f;
    //    DotResult = -DotResult;

    //}

    // transform.position = new Vector3(Screen.width * s, Screen.height * 0.8f, 1);

}
//else
//{

//    //transform.position = new Vector3(Screen.width * temp.x, Screen.height * 0.6f, 0);
//    transform.position = new Vector3(Screen.width * DotResult * -0.5f+Screen.width/2, Screen.height *0.9f, 0);
//    transform.rotation = Quaternion.Euler(0, 0, 90);

//}




//}
