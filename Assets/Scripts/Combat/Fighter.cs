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
        private Transform target;


        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            MoveToTarget();
        }

        public void MoveToTarget()
        {
            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;
            if (!isInRange)
            {
                GetComponent<Mover>().MoveTo(target.position);
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
                GetComponent<Animator>().SetTrigger("attack");
            }
        }

        // Animation event
        void Hit()
        {
            Health targetHealth = target.GetComponent<Health>();
            targetHealth.TakeDamage(weaponDamage);
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}