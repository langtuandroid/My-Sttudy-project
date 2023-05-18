using System;
using Interfaces;
using UnityEngine;

namespace Projectile_Reflection_Scripts
{
    public class RaycustController : MonoBehaviour
    {
        public event Action<Vector3> OnRayHit; 
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 rayHitPoint = RayThrower();
                if(rayHitPoint != Vector3.zero) OnRayHit?.Invoke(rayHitPoint);
            }
        }

        private Vector3 RayThrower()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<IReflectable>() != null) return hit.point;
                else return Vector3.zero;
            }
            
            return Vector3.zero;
        }
    }
}