using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Control
{
    public class Aimer : MonoBehaviour
    {
        [SerializeField] GameObject barrel = null;
        [SerializeField] float rotationSpeed = 5.0f;
        public Quaternion rotation;
        private Shooter _shooter;

        private void Start() 
        {
            _shooter = GetComponent<Shooter>();
        }

        public void AimConnon(Vector3 hitPoint)
        {
            Quaternion lookRotation = Quaternion.LookRotation(hitPoint - transform.position); //Calculates the barrel Rotation to look at the hitPoint

            barrel.transform.rotation = Quaternion.Slerp(barrel.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            
            rotation = barrel.transform.rotation;
        }
    }
}
