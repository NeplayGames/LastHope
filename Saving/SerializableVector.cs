using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableVector
{
    float x, y, z;
    public SerializableVector(Vector3 position){
        x = position.x;
        y = position.y;
        z = position.z;
        }
    public Vector3 ToVector()
    {
        return new Vector3(x, y, z);
    }
}
