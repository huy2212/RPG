using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Move
{
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private Vector3 localVelocity;
        private float forwardSpeed;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateWalkingAnimation();
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
        }

        private void UpdateWalkingAnimation()
        {
            localVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
            forwardSpeed = localVelocity.z;
            animator.SetFloat("forwardSpeed", forwardSpeed);
        }
    }
}
