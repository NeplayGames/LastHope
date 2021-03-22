using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    [SerializeField] Transform directionalLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        directionalLight.eulerAngles = new Vector3(0,    -Time.realtimeSinceStartup/10);
     
    }
}
