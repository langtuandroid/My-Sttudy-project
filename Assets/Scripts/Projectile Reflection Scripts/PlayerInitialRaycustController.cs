using System;
using Interfaces;
using UnityEngine;

namespace Projectile_Reflection_Scripts
{
    public class PlayerInitialRaycustController : MonoBehaviour
    {
        public event Action<RaycastHit> OnRayHit; 
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RayThrower();
            }
        }
        
        private void RayThrower()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<IReflectable>() != null) OnRayHit?.Invoke(hit);
            }
        }
        
    }
}