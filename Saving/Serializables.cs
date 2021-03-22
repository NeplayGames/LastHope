using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Serializables
    {

    int mission;
       
        
        

       
    public Serializables(int missionNo)
    {
        mission = missionNo;
    }
  
    public int Mission()
    {
        return mission;
    }
   
  
}

