using lastHope.movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StreamManager : MonoBehaviour
{

    Transform player;
   // Transform play;
    [SerializeField] float length;
    [SerializeField] float distance = 100f;
    public int iId;
    public int jId;
    [SerializeField] int noOfRows;
    [SerializeField] int noOfColumns;
    float tempDistance = Mathf.Infinity;
  //  bool checkNext = true;
    Vector2 tempY;
   // Transform forwardPlayer;
  //  List<AsyncOperation> async = new List<AsyncOperation>();
    List<bool> isLoad = new List<bool>();
    //    List<Terrain> terrains = new List<Terrain>();
    List<Terrain> terrains = new List<Terrain>();
    int totalTileToCheck;
    int tileToCheck;
    public static List<string> currentLoadedScenes = new List<string>();
   // Terrain[] orginal = new Terrain[9];
    [HideInInspector]
    public string scene;
    int tempInt;
  //  int tempIId = 0;
  
  //  float makeWait = 0f;
    bool ready = false;
    float tempDis;
  
    [SerializeField] Transform Terrains;
    [SerializeField] Transform TerrainsWithCollider;
    Vector2 x;
    Vector2 y;
 
    #region start Controller
   void Start()
    {
     
        totalTileToCheck = (int)(distance / length);
      
        tileToCheck = (int)(totalTileToCheck / 2);
      
      
        player = Camera.main.transform;

        iId = (int)(player.transform.position.z / length);
        jId = (int)(player.transform.position.x / length);
    
       for (int i = 0; i <= noOfRows; i++)
        {
            for (int j = 0; j <= noOfColumns; j++)
            {

               
               
                isLoad.Add(false);
               
            }
        }
        ready = true;

    }

  

   
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
       
            NeighBourCheck();

        }
    }
    #region Main Method
   
    Transform scene11;
    private void NeighBourCheck()
    {
    

        for (int i = -tileToCheck; i <= tileToCheck; i++)
        {
            for (int j = -tileToCheck; j <= tileToCheck; j++)
            {
                int id = iId + i;
                int idd = id * 100;
                int jd = jId + j;
                scene = idd.ToString() + jd;
               


                if (transform.Find(scene) != null)
                {
                    scene11 = transform.Find(scene);
                    
                    x = new Vector2(player.position.x, player.position.z);
                    y = new Vector2(scene11.position.x, scene11.position.z);

                    tempInt = id * 1 + noOfColumns * id + jd;
                    tempDis = Vector2.Distance(x, y);
                    tempDistance = Vector2.Distance(tempY, x);

                    if (tempDis < tempDistance)
                    {
                        tempY = y;
                        iId = id;
                        jId = jd;
                      
                    }
              

                    if (tempDis > distance - length/2f)


                    {
                        if (isLoad[tempInt])
                        {                         
                            {                            
                                {                                
                                    isLoad[tempInt] = false;
                                    if (Terrains.Find(scene))
                                    {
                                        SceneManager.UnloadSceneAsync(scene);
                                        TerrainsWithCollider.Find(scene).gameObject.SetActive(false);
                                        Terrains.Find(scene).gameObject.SetActive(true);
                                      
                                    }
                                }
                            }
                        }                    
                        }



                    else
                    {
                        if (!isLoad[tempInt])
                        {
                           
                            if (Terrains.Find(scene))
                            {
                               
                                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                                TerrainsWithCollider.Find(scene).gameObject.SetActive(true);
                               Terrains.Find(scene).gameObject.SetActive(false);
                              
                            }
                            isLoad[tempInt] = true;


                            return;
                        }
                    }

                }
            }
        }
    }

   
    
    

    #endregion

}
