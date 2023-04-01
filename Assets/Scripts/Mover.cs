using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform _target;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();     
        }
        UpdateAnimator();

    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // "out" giống với "ref" nhưng có khác đôi chút. Tra chatGPT để biết thêm
        bool hasHit = Physics.Raycast(ray, out hit); //Has hit or not?

        if (hasHit == true)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);  
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
