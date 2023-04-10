using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Health _target;
        Mover _mover;
        ActionScheduler _actionScheduler;
        Animator _animator;
        string a_attack = "attack", a_stopAttack = "stopAttack";
        [SerializeField] private float _weaponRange = 2.0f;
        [SerializeField] private float _timeBetweenAttacks = 1.0f;
        [SerializeField] float _punchDamage = 5f;

        float _timeSinceLastAttack = 0.0f;

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
            if (_target.IsDead()) return;

            if (!GetIsInRange())
            {
                _mover.MoveTo(_target.transform.position);
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
                _animator.SetTrigger(a_attack);
                _timeSinceLastAttack = 0.0f;
                

            }

        }

        void Hit()
        {
            _target.TakeDamage(_punchDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(_target.transform.position, transform.position) <= _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            _animator.SetTrigger(a_stopAttack);
            _target = null;
        }

        // Animation event


    }

}
