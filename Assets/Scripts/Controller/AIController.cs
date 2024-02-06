using System;
using System.Security;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float suspicionTime;
        [SerializeField] private float chaseDistance;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointDwellTime = 2f;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private Transform player;
        private Fighter fighter;
        private Health health;
        private Mover mover;
        private Vector3 guardPosition;
        private ActionScheduler actionScheduler;
        private int currentWaypointIndex = 0;
        private float wayPointTolerance = 1f;

        void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
        }

        void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
            guardPosition = transform.position;
        }

        void Update()
        {
            if (health.IsDead) return;
            if (IsInChasingRange() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0f;
                fighter.Attack(player);
            }
            else if (timeSinceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                if (patrolPath != null)
                {
                    PatrolBehaviour();
                }
                fighter.Cancel();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void SuspicionBehavior()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
            timeSinceArrivedAtWaypoint += Time.deltaTime;
            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition);
            }
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextWayPointIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());

            return distanceToWaypoint < wayPointTolerance;
        }

        private bool IsInChasingRange()
        {
            return Vector3.Distance(transform.position, player.position) <= chaseDistance;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}