using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

// Responsible for controlling the nav mesh agent
namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        

        Transform _target;
        NavMeshAgent _navMeshAgent;
        ActionScheduler _actionScheduler; 
        Animator _animator;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (_navMeshAgent == null)
                Debug.LogWarning("Mover.cs: NavMeshAgent is not found!");

            _actionScheduler = GetComponent<ActionScheduler>();
            if (_actionScheduler == null)
                Debug.LogWarning("Mover.cs: ActionScheduler is not found!");

            _animator = GetComponent<Animator>();
            if (_animator == null)
                Debug.LogWarning("Mover.cs: Animator is not found!");
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
            _actionScheduler.StartAction(this);
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
            _animator.SetFloat("forwardSpeed", speed);
        }
    }


}



