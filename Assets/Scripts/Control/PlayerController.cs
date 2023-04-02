using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using RPG.Combat;
using UnityEngine;

//Responsible for doing raycast
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target); //Get our sibling component
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hit;

            // "out" giống với "ref" nhưng có khác đôi chút. Tra chatGPT để biết thêm
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); //Has hit or not?

            if (hasHit == true)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
