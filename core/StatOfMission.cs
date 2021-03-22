using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lastHope.core;
public class StatOfMission
{

    float healthStat;
    float timeStat;
    float totalRespamStat;
    public float CalculateStat(float health,float time,float totalRespam)
    {
        healthStat = health;
        timeStat = time;
        totalRespamStat = totalRespam;

        return health * 0.1F;
    }
   
}
