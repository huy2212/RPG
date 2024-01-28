using System;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private Mover mover;
        private Fighter fighter;
        private Camera mainCamera;
        private Ray ray;

        void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        void Start()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            if (HandleCombat()) return;
            if (HandleMouseMovement()) return;
            Debug.Log("Nothing to do!");
        }

        private bool HandleCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

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