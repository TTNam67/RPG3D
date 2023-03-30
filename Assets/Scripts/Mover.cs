using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform _target;
    Ray _lastRay;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        Debug.DrawRay(_lastRay.origin, _lastRay.direction * 100, new Color(0.3f, 0.4f, 0.6f, 1f));
        GetComponent<NavMeshAgent>().destination = _target.position;
    }
}

