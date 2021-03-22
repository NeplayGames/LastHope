using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer line;
    [SerializeField] Transform zipline1;
    [SerializeField] Transform zipline2;
    void Start()
    {
       line = transform.GetComponent<LineRenderer>();
        line.SetPosition(0, zipline1.position+Vector3.up * 6.4f+ Vector3.forward);
        line.SetPosition(1, zipline2.position+ Vector3.up * 6.4f+Vector3.forward);
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
    }

  
}
