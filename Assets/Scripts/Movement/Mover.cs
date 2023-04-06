using RPG.Combat;
using UnityEngine;
using UnityEngine.AI;

// Responsible for controlling the nav mesh agent
namespace RPG.Movement
{
    public class Mover : MonoBehaviour
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
        public void Stop()
        {
            _navMeshAgent.isStopped = true;
        }
        
        public void StartMoveAction(Vector3 _destination)
        {
            GetComponent<Fighter>().Cancel();
            MoveTo(_destination);
        }

        public void MoveTo(Vector3 _destination)
        {
            _navMeshAgent.destination = _destination;
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



