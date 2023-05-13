using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _healthPoint = 50f;
        Animator _animator;
        ActionScheduler _actionScheduler;
        string a_die = "die";
        bool _isDead = false;

        //Getters
        public bool IsDead()
        {
            return _isDead;
        }

        void Start()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
                Debug.LogWarning("Health.cs: Animator is not found!");

            _actionScheduler = GetComponent<ActionScheduler>();
            if (_actionScheduler == null)
                Debug.LogWarning("Health.cs: ActionScheduler is not found");
        }

        public void TakeDamage(float damage)
        {
            _healthPoint = Mathf.Max(_healthPoint - damage, 0);
            if (_healthPoint ==  0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_isDead) return;
            _isDead = true;
            _animator.SetTrigger(a_die);

            // When you die, any running action is eliminated 
            _actionScheduler.CancelCurrentAction();
        }
    }
}
