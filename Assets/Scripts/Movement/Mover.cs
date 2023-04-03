using UnityEngine;
using UnityEngine.AI;

// Responsible for controlling the nav mesh agent
namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform _target;
        void Update()
        {

            UpdateAnimator();

        }

        public void MoveTo(Vector3 _destination)
        {
            GetComponent<NavMeshAgent>().destination = _destination;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);  //Transforms a direction from world space to local space
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }

    
}

