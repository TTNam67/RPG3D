using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform _target;

        float _rotateSpeed = 50f;

        // Player completes the move, then camera follows back 
        void LateUpdate()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.RotateAround(_target.transform.position, Vector3.up, -_rotateSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
                transform.RotateAround(_target.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime);
           
        }
    }
}
