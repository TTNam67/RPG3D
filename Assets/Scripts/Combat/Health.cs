using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _healthPoint = 50f;
        Animator _animator;
        string a_die = "die";
        bool _isDead = false;
        void Start()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
                Debug.LogWarning("Health.cs: Animator is not found!");
        }

        public void TakeDamage(float damage)
        {
            _healthPoint = Mathf.Max(_healthPoint - damage, 0);
            if (_healthPoint == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_isDead) return;
            _isDead = true;

            _animator.SetTrigger(a_die);
        }
    }
}
