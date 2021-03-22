using lastHope.core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace lastHope.movement {
    public class Movement : MonoBehaviour,ChangeAction
    {
        NavMeshAgent navMeshAgent;
        AIHealth health;
        bool isRun = false;
        Animator animator;
      

       
        private void Start()
        {
            health = GetComponent<AIHealth>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = transform.GetChild(0).GetComponent<Animator>();
           
        }
        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            AnimateCharacter();
        }

        private void AnimateCharacter()
        {
            if(Mathf.Abs(navMeshAgent.velocity.x) ==0f)
            {
               animator.SetFloat("speed", 0f);

            }
            else if(isRun)
            {
                animator.SetFloat("speed", 1f);

            }
            else
            {
                animator.SetFloat("speed", 0.5f);

            }
        }

        public void CancelAction()
        {
            navMeshAgent.isStopped = true;

        }
        public void MoveToDestination(Vector3 destination,float speed,bool isRunning)
        {
            navMeshAgent.speed =speed;
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
            isRun = isRunning;
        }
        
    }

}

