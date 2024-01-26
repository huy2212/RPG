using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Vector3 distanceOffset;

        void Start()
        {
            distanceOffset = transform.position - target.position;
        }

        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}

