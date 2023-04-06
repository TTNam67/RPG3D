using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        Transform _target;
        Mover _mover;
        [SerializeField] private float _weaponRange = 2.0f;

        private void Start() 
        {
            _mover = GetComponent<Mover>();
            if (_mover == null)    
                Debug.LogWarning("Fighter.cs: Mover is not found!");
        }
        void Update()
        {
            if (_target == null) return;

            if (_target != null && !GetIsInRange())
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.Stop();
            }


        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(_target.position, transform.position) <= _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _target = combatTarget.transform;
        }

        public void Cancel()
        {
            _target = null;
        }
    }
}
