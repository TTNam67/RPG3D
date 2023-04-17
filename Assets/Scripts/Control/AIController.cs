using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float _chaseDistance = 5f;
        [SerializeField] private float _suspicionTime = 5f;
        [SerializeField] PatrolPath _patrolPath;
        [SerializeField] float _waypointTolerance = 1f;
        [SerializeField] float _waypointDwellTime = 1.5f;
        [Range(0, 1)]
        [SerializeField] float _patrolSpeedFraction = 0.2f; //20% of maxSpeed
        Fighter _fighter;
        GameObject _player;
        Health _health;
        Mover _mover;
        ActionScheduler _actionScheduler;
        Vector3 _guardPosition;
        NavMeshAgent _navMeshAgent;
        float _timeSinceLastSawPlayer = Mathf.Infinity;
        float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int _currentWaypointIndex = 0;

        private void Start() 
        {
            _fighter = GetComponent<Fighter>();
            if (_fighter == null)
                Debug.LogWarning("AIController.cs: Fighter is not found");

            _player = GameObject.FindWithTag("Player");
            if (_player == null)
                Debug.LogWarning("AIController.cs: Player is not found");

            _health = GetComponent<Health>();
            if (_health == null)
                Debug.LogWarning("AIController.cs: Health is not found");

            _mover = GetComponent<Mover>();
            if (_mover == null)
                Debug.LogWarning("AIController.cs: Mover is not found");

            _actionScheduler = GetComponent<ActionScheduler>();
            if (_actionScheduler == null)
                Debug.LogWarning("AIController.cs: ActionScheduler is not found");

            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (_navMeshAgent == null)
                Debug.LogWarning("AIController.cs: NavMeshAgent is not found");

            _guardPosition = transform.position;
        }

        private void Update()
        {
            if (_health.IsDead() == true)
            {
                return;
            }

            if (InAttackRangeOfPlayer() && _fighter.CanAttack(_player))
            {
                
                AttackBehaviour();
            }
            else if (!InAttackRangeOfPlayer() && _timeSinceLastSawPlayer < _suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            
            Vector3 nextPosition = _guardPosition;
            if (_patrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (_timeSinceArrivedAtWaypoint > _waypointDwellTime)
            {
                _mover.StartMoveAction(nextPosition, _patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < _waypointTolerance;
        }

        private void CycleWaypoint()
        {
            _currentWaypointIndex = _patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return _patrolPath.GetWaypoint(_currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            _actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            return distanceToPlayer < _chaseDistance;
        }

        //Called by Unity
        //Only draw when the object is selected
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        } 
    }
}

