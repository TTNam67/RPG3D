using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Transform _target;
        Mover _mover;
        ActionScheduler _actionScheduler;
        Animator _animator;
        string a_Attack = "attack";
        [SerializeField] private float _weaponRange = 2.0f;
        [SerializeField] private float _timeBetweenAttacks = 1.0f;
        float _timeSinceLastAttack = 0.0f;
        float _punchDamage = 5f;

        private void Start() 
        {
            _mover = GetComponent<Mover>();
            if (_mover == null)    
                Debug.LogWarning("Fighter.cs: Mover is not found!");

            _animator = GetComponent<Animator>();
            if (_animator == null)
                Debug.LogWarning("Fighter.cs: Animator is not found!");

            _actionScheduler = GetComponent<ActionScheduler>();
            if (_actionScheduler == null)
                Debug.LogWarning("Fighter.cs: ActionScheduler is not found!");            
        }

        void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_target == null) return;
            if (!GetIsInRange())
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }


        }

        private void AttackBehaviour()
        {
            if (_timeSinceLastAttack > _timeBetweenAttacks)
            {
                // This will trigger the Hit() event
                _animator.SetTrigger(a_Attack);
                _timeSinceLastAttack = 0.0f;
                

            }

        }

        void Hit()
        {
            Health healthComponent = _target.GetComponent<Health>();
            if (healthComponent == null)
                Debug.LogError("Fighter.cs: Target's health component is not found");

            healthComponent.TakeDamage(_punchDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(_target.position, transform.position) <= _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget.transform;
        }

        public void Cancel()
        {
            _target = null;
        }

        // Animation event


    }

}
