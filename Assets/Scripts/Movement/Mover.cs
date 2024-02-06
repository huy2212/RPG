using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private Vector3 localVelocity;
        private float forwardSpeed;
        private Health health;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead)
            {
                navMeshAgent.enabled = false;
            }
            UpdateWalkingAnimation();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateWalkingAnimation()
        {
            localVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
            forwardSpeed = localVelocity.z;
            animator.SetFloat("forwardSpeed", forwardSpeed);
        }

        public void Cancel()
        {
            Stop();
        }
    }
}
