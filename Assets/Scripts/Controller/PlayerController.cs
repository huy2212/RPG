using System;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System.Runtime.CompilerServices;
using System.Security;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;
        private Mover mover;
        private Fighter fighter;
        private Camera mainCamera;
        private Ray ray;
        private Health health;

        void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        void Start()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            if (health.IsDead) return;
            if (HandleCombat()) return;
            if (HandleMouseMovement()) return;
        }

        private bool HandleCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay(), targetLayer);
            foreach (RaycastHit hit in hits)
            {
                Transform target = hit.transform;
                if (!fighter.CanAttack(target)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target);
                }
                return true;
            }
            return false;
        }

        private bool HandleMouseMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = GetMouseRay();
                RaycastHit hit;
                bool isHit = Physics.Raycast(ray, out hit);
                if (isHit)
                {
                    mover.StartMoveAction(hit.point);
                    return true;
                }
            }
            return false;
        }

        private Ray GetMouseRay()
        {
            return mainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }
}