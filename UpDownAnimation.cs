using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float period = 2f;
    [SerializeField] float movementFactor = 3f;

    const float tau = Mathf.PI * 2f;
    float cycle;
    float rawCycle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cycle = Time.realtimeSinceStartup / period;
        rawCycle = Mathf.Sin(cycle * tau);
        transform.position = transform.position + transform.up * movementFactor * rawCycle;
    }
}
