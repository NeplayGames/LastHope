using lastHope.combat;
using lastHope.movement;
using lastHope.core;
using UnityEngine;
using System.Collections.Generic;

public class AIMovement : MonoBehaviour
{
   
    [SerializeField] float distanceTollerence = 2f;
    [SerializeField] float distanceToPlayer = 5f;
    [SerializeField] float distanceToPlayerNear = 5f;
    [SerializeField] float timeSincePlayer = 7f;
    GameObject player;
    Movement movement;
    AIFighter fighter;
  
    AIHealth health;
    Vector3 guardPosition;
    int currentPointIndex = 0;
    [SerializeField] bool isFighter;
    [SerializeField] PatrolPath patrolPath;
    float timeSincePlayerObserved = Mathf.Infinity;
    float timeWaitAtWayPoint = Mathf.Infinity;
   
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        movement = GetComponent<Movement>();
        fighter = GetComponent<AIFighter>();
        health = GetComponent<AIHealth>();
        guardPosition = this.transform.position;
    }

    private void Update() {

        if (health.IsDead()) return;
        if (isFighter && InRange()  && fighter.CanAttack(player))
        {
            health.getHits = false;
            AttackBehaviour();
        }
        //else if (!isFighter && InRange() && !communication)
        //{
        //    CommunicateWithPC();
        //}
        else if (timeSincePlayerObserved < timeSincePlayer)
        {
            SuspiciousBehaviour();
        }
        else
        {
            PatrolBehaviour();
        }
    }

    //private void CommunicateWithPC()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        List<string> msg = gameData.LoadMessage("Mission0", 0);
    //        int objective = gameData.objectiveDone;
    //      //  print("This is msg length " + msg.Count);

    //        communcate.LoadMessage(msg, msg.Count,objective);
    //        communication = communcate.StartConversation(player);
    //    }
    //}
    Vector3 dirFromAtoB;
    float dotProd;
    bool InRange()
    {
         dirFromAtoB = (player.transform.position - transform.position).normalized;
        dotProd = Vector3.Dot(dirFromAtoB, transform.forward);
       
      
        return ((Vector3.Distance(player.transform.position, transform.position)  < distanceToPlayer && dotProd > 0.9)|| Vector3.Distance(player.transform.position, transform.position) < distanceToPlayerNear || health.getHits);
        
    }
    
    void AttackBehaviour()
    {
        timeSincePlayerObserved = 0;

        fighter.Attack(player);
    }
    private void PatrolBehaviour()
    {
        Vector3 nextPoint = guardPosition;
        if (patrolPath != null)
        {
            if (AtWayPoint())
            {
                timeWaitAtWayPoint += Time.deltaTime;
                if (timeWaitAtWayPoint < 2f)
                    return;
                CycleWayPoint();
                timeWaitAtWayPoint = 0f;
            }
            nextPoint = GetNextPoint();
        }
        movement.MoveToDestination(nextPoint,2f,false);
    }
    private Vector3 GetNextPoint()
    {
        return patrolPath.GetPosition(currentPointIndex);
    }

    private void CycleWayPoint()
    {
        currentPointIndex = patrolPath.GetNextPoint(currentPointIndex);
    }

    private bool AtWayPoint()
    {
        float distanceFromTheWayPoint = Vector3.Distance(transform.position, GetNextPoint());
        return distanceFromTheWayPoint < distanceTollerence;
    }

    private void SuspiciousBehaviour()
    {
        timeSincePlayerObserved += Time.deltaTime;
        GetComponent<Scheduler>().CancelAction();
    }
}
