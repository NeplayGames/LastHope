using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using lastHope.movement;
public class InstanciateScene : MonoBehaviour
{
    public float progress = 0;
    public bool wwait = true;
    public bool check = false;
    public bool instanciateFirst = false;
    [SerializeField] int noOfRows;
    [SerializeField] int noOfColumns;
    [SerializeField] Canvas ca;
    public bool isDone = false;
    int tempTot;
    List<bool> isLoad = new List<bool>();
    AsyncOperation op ;
    AsyncOperation operation;
    string scene;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= noOfRows; i++)
        {
            for (int j = 0; j <= noOfColumns; j++)
            {


                isLoad.Add(false);

            }
        }
        tempTot = noOfRows * 1 + noOfRows * noOfColumns + noOfColumns;
       
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    public void Update()
    {
       

        if (!wwait)
        {
          // QualitySettings.vSyncCount = 0;
            if (!instanciateFirst)
            {
               // operation.allowSceneActivation = false;
               // operation.allowSceneActivation = false;
                        operation = SceneManager.LoadSceneAsync(1);

                instanciateFirst = true;
            }
           if(instanciateFirst && operation.progress ==1)
            for (int i = 0; i <= noOfRows; i++)
                {
                     
                    for (int j = 0; j <= noOfColumns; j++)
                    {
                       
                       
                        int tempInt = i * 1 + i * noOfColumns + j;

                    if (!isLoad[tempInt])
                        {
                        int idd = i * 100;

                        scene = "scene" + idd + j;
                        op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                            wwait = true;
                        check = true;
                            isLoad[tempInt] = true;
                   
                      
                        progress =(float) tempInt / tempTot;
                      
                       
                        return;
                        }
                        if (i == noOfRows && j == noOfColumns)
                        {
                            isDone = true;
                       // QualitySettings.vSyncCount = 1;
                      
                        DestroyObj();
                        }
                    }
                }
            

           

        }
        
        if ( check && op.progress == 1)
        {
          //  print(true);
            SceneManager.UnloadSceneAsync(scene);
               
                    wwait = false;
                    check = false;
                   
                
           
        }

    }
    public void DestroyObj()
    {
        Destroy(ca.gameObject);

        Destroy(this);
    }
}
