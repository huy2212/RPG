using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private NavMeshAgent navMeshAgent;

    void Awake()
    {

    }
}
