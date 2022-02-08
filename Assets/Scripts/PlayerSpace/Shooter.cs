using System.Collections;
using System.Collections.Generic;
using Demo.Effects;
using UnityEngine;

namespace Demo.PlayerSpace
{
    public class Shooter : MonoBehaviour
    {
        [Header("Shooting Coniguration")]
        
        [SerializeField] GameObject ballPrefab = null;
        [SerializeField] Transform ballSpawnPoint = null;
        [SerializeField] Transform ballParent = null;
        [SerializeField] float thrustMin = 5.0f;
        [SerializeField] float thrustMax = 10.0f;
        [SerializeField] float thrustAdjustment = 10.0f;

        [SerializeField] AudioClip[] shootSounds = null;

        [Header("Timer Configuration")]
        //[SerializeField] float timerModifier = 0.2f;
        [SerializeField] float shootCooldown = 1.0f;
        public float shootTimer = 0.0f;
        private float destroyTimer = 1.5f;
        private Vector3 originalSpawn;
        public bool canShoot = true;


        void Start()
        {
            originalSpawn = ballSpawnPoint.position;
            shootTimer = 0f;
        }

        void FixedUpdate()
        {
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            if (shootTimer > 0f)
            {
                shootTimer -= Time.deltaTime;
            }

            if (shootTimer <= 0.0f)
            {
                canShoot = true;
            }


            if (Mathf.Approximately(shootTimer, 0f))
            {
                shootTimer = 0;
                canShoot = true;
            }
        }

        public void Shoot(Vector3 hitPoint, Vector3 barrelPosition)
        {
            if (canShoot)
            {
                GameObject ball = Instantiate(ballPrefab.gameObject, ballSpawnPoint.position, ballSpawnPoint.rotation, ballParent); 
                var clip = Random.Range(0, shootSounds.Length);
                AudioManager.Instance.Play(shootSounds[clip], transform);
                
                float thrust = Random.Range(thrustMin, thrustMax) * thrustAdjustment;
                
                ball.GetComponent<Rigidbody>().velocity = ballSpawnPoint.transform.up * thrustAdjustment;

                canShoot = false;
                shootTimer = shootCooldown;
                UpdateTimers();
            }

            destroyTimer -= Time.deltaTime; // * timerModifier;
        }

        public float GetShootTimer()
        {
            return shootTimer;
        }
        
    }
}
