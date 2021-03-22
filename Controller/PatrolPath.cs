using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PatrolPath : MonoBehaviour
{
    private void OnEnable()
    {
        CheckPlacement();
    }
    private void Start()
    {
        CheckPlacement();
    }
    [SerializeField] Transform player;
    private void OnDrawGizmos()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(GetPosition(i), .2f);
            Gizmos.DrawLine(GetPosition(i), GetPosition(GetNextPoint(i)));
            Gizmos.DrawLine(GetPosition(i) + Vector3.up * 5, GetPosition(i) + -2000 * Vector3.up);
        }
    }
    RaycastHit hit;
     void CheckPlacement()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Physics.Raycast(GetPosition(i) + Vector3.up * 5, -2000 * Vector3.up, out hit) && !hit.collider.CompareTag("water")) transform.GetChild(i).position = hit.point;
            else if (Physics.Raycast(GetPosition(i) + Vector3.up * 5, +2000 * Vector3.up, out hit) && !hit.collider.CompareTag("water")) transform.GetChild(i).position = hit.point;

        }
        player.transform.position = GetPosition(0) + transform.up * 0.5f;
        player.GetComponent<NavMeshAgent>().Warp(player.transform.position);
        }
    public int GetNextPoint(int i)
    {
        return (i + 1) % transform.childCount;
    }

    public Vector3 GetPosition(int i)
    {
        return transform.GetChild(i).position;
    }
}
