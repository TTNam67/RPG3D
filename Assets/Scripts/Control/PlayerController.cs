using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

//Responsible for doing raycast
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        Fighter _fighter;
        Mover _mover;
        void Start()
        {
            _fighter = GetComponent<Fighter>();
            if (_fighter == null)
                Debug.LogWarning("PlayerController.cs: Fighter is not found");

            _mover = GetComponent<Mover>();
            if (_mover == null)
                Debug.LogWarning("PlayerController.cs: Mover is not found");
        }

        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;

            Debug.Log("Nothing to do");
        }

        private bool InteractWithCombat()
        {
            //hits: danh sách các RaycastHit được tạo ra từ việc 1 Ray (xuất phát từ
            // bàn phím) va chạm với các GameObject trên đường đi của nó
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                // Nếu con trỏ chuột hover vào 1 object có chứa
                // CombatTarget (có thể bị chọn làm mục tiêu tấn công)
                // và click chuột trái vào đó thì sẽ phát lệnh cho Player tấn công
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                
                // Nếu ta "Can't Attack" mà Raycast va phải thì ta bỏ qua và xét tới các object đằng sau nó
                if (!_fighter.CanAttack(target)) 
                {
                    continue;
                }

                // Is left mouse clicked
                if (Input.GetMouseButtonDown(0))
                {
                    // Attack if it is 
                    _fighter.Attack(target); //Get our sibling component
                    
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
                if (Input.GetMouseButton(0))
                    _mover.StartMoveAction(hit.point);

                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

