using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _health = 50f;
        void Start()
        {

        }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Max(_health - damage, 0);
            print("health: " + _health);
        }
    }
}
