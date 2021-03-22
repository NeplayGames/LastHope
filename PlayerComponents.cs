using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    [SerializeField] public Transform hand;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
