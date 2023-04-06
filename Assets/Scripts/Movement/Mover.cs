using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

// Responsible for controlling the nav mesh agent
namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        

        [SerializeField] Transform _target;
        NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (_navMeshAgent == null)
                Debug.LogWarning("Mover.cs: NavMeshAgent is not found!");
        }
        void Update()
        {

            UpdateAnimator();

        }
        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
        
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            // GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);  //Transforms a direction from world space to local space
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }


}



