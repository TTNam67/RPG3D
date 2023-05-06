using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

//Responsible for doing raycast
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        public float maxFallDistance = 2f; // Maximum height for falling
        public float bounceForce = 50f; // Force of the bounce
        public float bounceDelay = 0.03f; // Delay before bouncing starts
        private bool isBouncing = false;
        Vector3 _defaultPosition = new Vector3(81f, 5f, 78f);
        Fighter _fighter;
        Mover _mover;
        Health _health;
        Rigidbody _rigidbody;

        [SerializeField]Camera _camera;
        void Start()
        {
            this.transform.position = _defaultPosition;

            _fighter = GetComponent<Fighter>();
            if (_fighter == null)
                Debug.LogWarning(transform.name + " PlayerController.cs: Fighter is not found");

            _mover = GetComponent<Mover>();
            if (_mover == null)
                Debug.LogWarning(transform.name + " PlayerController.cs: Mover is not found");

            _health = GetComponent<Health>();
            if (_health == null)
                Debug.LogWarning(transform.name + " PlayerController.cs: Health is not found");

            _rigidbody = GetComponent<Rigidbody>();
            if (_rigidbody == null)
                Debug.LogWarning(transform.name + " PlayerController.cs: Rigidbody is not found");
        }

        void Update()
        {
            if (_health.IsDead() == true) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            //hits: danh sách các RaycastHit được tạo ra từ việc 1 Ray (xuất phát từ
            // camera) va chạm với các GameObject trên đường đi của nó
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                // Gizmos.color = Color.blue;
                // Gizmos.DrawLine(_camera.transform.position, hit.point);
                // Nếu con trỏ chuột hover vào 1 object không chứa CombatTarget (click vào bản thân, NPC, terrain,...)
                // thì xét tới các object tiếp theo
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                // Nếu ta "Can't Attack" mà Raycast va phải thì ta bỏ qua và xét tới các object đằng sau nó
                if (!_fighter.CanAttack(target.gameObject)) 
                {
                    continue;
                }

                // Is left mouse clicked
                if (Input.GetMouseButtonDown(1))
                {
                    // Attack if it is 
                    _fighter.Attack(target.gameObject); //Get our sibling component
                }

                // If there is at least 1 CombatTarget on the way, return true
                return true;
            }
            // If there is no CombatTarget or GameObject on the way of the Ray
            // return false
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;

            // "out" giống với "ref" nhưng có khác đôi chút. Tra chatGPT để biết thêm
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); //Has hit or not?

            if (hasHit == true)
            {
                if (Input.GetMouseButton(1))
                    _mover.StartMoveAction(hit.point, 1f);

                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void OnCollisionEnter(Collision collision)
        {
            print("Force:" + collision.impulse.magnitude);
            if (collision.impulse.magnitude > 4f && !isBouncing) // Only bounce if the impact is strong enough
            {
                ContactPoint cP = collision.contacts[0];
                float fallDistance = transform.position.y - cP.point.y;
                print("Fall: " + fallDistance);
                print(cP.thisCollider.name + " hit " + cP.otherCollider.name);

                if (fallDistance > maxFallDistance)
                {
                    Invoke("StartBouncing", bounceDelay);
                }
            }
        }


        private void StartBouncing()
        {
            isBouncing = true;
            _rigidbody.AddForce(new Vector3(0f, 12f, 0f) * bounceForce, ForceMode.Impulse);
        }

        

    }
}

