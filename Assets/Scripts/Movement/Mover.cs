using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

// Responsible for controlling the nav mesh agent
namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        
        
        [SerializeField] Transform _target;
        [SerializeField] float _maxSpeed = 6f;
        NavMeshAgent _navMeshAgent;
        ActionScheduler _actionScheduler; 
        Animator _animator;
        Health _health;


        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (_navMeshAgent == null)
                Debug.LogWarning(transform.name + " Mover.cs: NavMeshAgent is not found!");

            _actionScheduler = GetComponent<ActionScheduler>();
            if (_actionScheduler == null)
                Debug.LogWarning(transform.name + " Mover.cs: ActionScheduler is not found!");

            _animator = GetComponent<Animator>();
            if (_animator == null)
                Debug.LogWarning(transform.name + " Mover.cs: Animator is not found!");

            _health = GetComponent<Health>();
            if (_health == null)
                Debug.LogWarning(transform.name + " Mover.cs: Health is not found!");
        }

        void Update()
        {
            _navMeshAgent.enabled = !_health.IsDead();
            UpdateAnimator();
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
        
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            _actionScheduler.StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction) 
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.speed =  _maxSpeed * Mathf.Clamp01(speedFraction);
            _navMeshAgent.isStopped = false;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);  //Transforms a direction from world space to local space
            float speed = localVelocity.z;
            _animator.SetFloat("forwardSpeed", speed);
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3) state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}


