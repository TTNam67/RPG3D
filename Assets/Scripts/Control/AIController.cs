using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float _chaseDistance = 5f;
        Fighter _fighter;
        GameObject _player;
        Health _health;
        Mover _mover;
        ActionScheduler _actionScheduler;
        Vector3 _guardPosition;

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
                _fighter.Attack(_player);
            }
            else 
            {
                _mover.StartMoveAction(_guardPosition);
            }
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

