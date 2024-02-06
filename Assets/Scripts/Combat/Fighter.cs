using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 10f;
        private float timeSinceLastAttack;
        private Health target;
        public event Action OnAttackCancel;

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead) return;
            MoveToTarget();
        }

        public void MoveToTarget()
        {
            bool isInRange = Vector3.Distance(transform.position, target.transform.position) < weaponRange;
            if (!isInRange)
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
                // Start attacking when in range
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                timeSinceLastAttack = 0;
                // This will trigger hit event
                TriggerAttack();
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation event
        void DealDamage()
        {
            target?.TakeDamage(weaponDamage);
        }

        public void Attack(Transform combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            // GetComponent<Mover>().Cancel();
            target = null;
            OnAttackCancel?.Invoke();
        }

        public bool CanAttack(Transform combatTarget)
        {
            if (combatTarget == null) return false;
            Health combatTargetHealth = combatTarget.GetComponent<Health>();
            return combatTargetHealth != null && !combatTargetHealth.IsDead;
        }
    }
}