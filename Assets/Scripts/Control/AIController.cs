using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float _chaseDistance = 5f;

        private void Update()
        {
            GameObject player = GameObject.FindWithTag("Player");
            
            if (DistanceTo(player) < _chaseDistance)
                Debug.LogWarning(transform.name + "Should chase");
        }

        private float DistanceTo(GameObject player)
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }

}