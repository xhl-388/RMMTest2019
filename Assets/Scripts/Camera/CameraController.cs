using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CP.Camera
{
    using Camera = UnityEngine.Camera;
    public class CameraController : MonoBehaviour
    {
        private Camera m_camera;

        // Camera Property Begin
        private float minRotX = 0;
        private float maxRotX = 80;

        private float rotX;
        private float rotY;

        private float speed = 2;
        private float radius = 5;
        private float height = 0.5f;
        // Camera Property End

        [SerializeField]
        private Transform target;

        private bool lookAt = false;

        private void Awake()
        {
            m_camera = GetComponent<Camera>();
        }

        private void Start()
        {
            Debug.Assert(target != null);
        }

        private void LateUpdate()
        {
            if (lookAt)
            {
                LookAtLateUpdate();
            }else
            {
                UnLookAtLateUpdate();
            }
        }

        private void LookAtLateUpdate()
        {
            rotX -= Input.GetAxis("Mouse Y");
            rotX = Mathf.Clamp(rotX, minRotX, maxRotX);
            rotY += Input.GetAxis("Mouse X");
            if (rotY > 360)
                rotY -= 360;
            if (rotY < -360)
                rotY += 360;

            Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
            Vector3 tp = target.position;
            Vector3 pos = tp + Vector3.up * height + rot * new Vector3(0, 0, -radius);
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * speed);
        }
        
        private void UnLookAtLateUpdate()
        {
            // TODO
            LookAtLateUpdate();

        }

        public void SetLookAt(bool lookat)
        {
            lookAt = lookat;
        }
    }
}
