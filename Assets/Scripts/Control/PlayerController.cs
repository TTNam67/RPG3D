using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
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
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // "out" giống với "ref" nhưng có khác đôi chút. Tra chatGPT để biết thêm
            bool hasHit = Physics.Raycast(ray, out hit); //Has hit or not?

            if (hasHit == true)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }
    }
}
