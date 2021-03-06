using System;
using System.Collections;
using System.Collections.Generic;
using Demo.Effects;
using UnityEngine;

namespace Demo.Control
{
    public class PlayerController : MonoBehaviour, ILevelCompleter
    {
        
        [SerializeField] LayerMask hitLayer = 3;
        [SerializeField] GameObject[] validTargets = null;
        [SerializeField] GameObject barrel = null;
        private Vector3 barrelPosition;
        private Vector3 barrelForward;
        private Camera _camera = null;
        private static Vector3 point;
        private Aimer _mover;
        private Shooter _shooter;
        private bool _hasControl;
        void Start()
        {
            Cursor.visible = false;
            _camera = Camera.main;
            _mover = GetComponent<Aimer>();
            _shooter = GetComponent<Shooter>();
            _hasControl = true;
        }

        private void FixedUpdate()
        {
            if (!_hasControl && Cursor.visible == true) return;
            if (!_hasControl && Cursor.visible == false)
            {
                Cursor.visible = true;
            }

            GetHitPoint();
            if (_hasControl)
            {
                _mover.AimConnon(GetHitPoint());            
                
                _shooter.PermitShoot(GetHitPoint(), barrel.transform.position);
                
            }
            
        }


        //returns the Vector3 over which the mouse is hovering
        public Vector3 GetHitPoint()
        {
            //RaycastHit hit;
            Ray ray = new Ray(GetMouseRay().origin, GetMouseRay().direction);
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.blue);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, hitLayer);

            Vector3 lastValid = new Vector3(0,0,0);
            for (int i = 0; i < hits.Length; i++)
            {
                for (var x = 0; x < validTargets.Length; x++)
                {
                    if (validTargets[x] == hits[i].transform.gameObject )
                    {
                        lastValid = hits[i].point;
                        return lastValid;
                    }
                }
            }
            
            return lastValid;
        }

        public void SetControl(bool controlValue)
        {
            
            _hasControl = controlValue;
            if (!_hasControl)
            {
                Cursor.visible = true;
            }
            Debug.Log("Setting control to " + _hasControl);
        }

        public bool GetHasControl()
        {
            return _hasControl;
        }
        public void CompleteLevel()
        {
            Debug.Log("Congratulations, Load next level after this");
            _hasControl = false;
            Cursor.visible = true;
        }

        public void StageLevel()
        {
            Debug.Log("anything that needs to rearrange does now");
        }
        private static Ray GetMouseRay ()
        {
            return Camera.main.ScreenPointToRay (Input.mousePosition);
        }

        public void HalfWayBonus()
        {
            
        }
    }
}
