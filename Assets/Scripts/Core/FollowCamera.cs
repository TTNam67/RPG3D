using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] private Camera _camera;

        private Vector3 _previousPosition;
        float _rotateSpeed = 90f;

        // Player completes the move, then camera follows back 
        void LateUpdate()
        {
            RotateCamera();
        }

        private void RotateCamera()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _previousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = _previousPosition - _camera.ScreenToViewportPoint(Input.mousePosition);

                transform.position = _target.position;

                transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
                transform.Translate(new Vector3(0, 0, -7));

                _previousPosition = _camera.ScreenToViewportPoint(Input.mousePosition); 
            }

            if (Input.GetKey(KeyCode.RightArrow))
                transform.RotateAround(_target.transform.position, Vector3.up, -_rotateSpeed * Time.deltaTime);
            else if (Input.GetKey(KeyCode.LeftArrow))
                transform.RotateAround(_target.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime);
        }

     
    }
}

