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
        public float _shootTimer = 0.0f;
        private float _destroyTimer = 1.5f;
        private Vector3 _originalSpawn;
        public bool _canShoot = true;


        void Start()
        {
            _originalSpawn = ballSpawnPoint.position;
            _shootTimer = 0f;
        }

        void FixedUpdate()
        {

        }

        public void PermitShoot(Vector3 hitPoint, Vector3 barrelPosition)
        {
            if (_shootTimer > 0f || !_canShoot)
            {
                _shootTimer -= Time.deltaTime;
            }

            {
                    if (_shootTimer <= 0.0f || Mathf.Approximately(_shootTimer, 0f))
                    {
                        _shootTimer = 0f;
                        //Debug.Log("You Can Shoot!");
                        _canShoot = true;

                        // uncomment bellow for auto shooting. 
                        //TODO make this into a mode later
                        //Shoot(hitPoint, barrelPosition);
                    }
                }

            if (Input.GetButton("Fire1") && _canShoot)
                {
                    // if (_shootTimer <= 0.0f || Mathf.Approximately(_shootTimer, 0f))
                    // {
                        //Debug.Log("You Can Shoot!");
                        //_canShoot = true;
                        Shoot(hitPoint, barrelPosition);
                    //}
                }
        }

        public void Shoot(Vector3 hitPoint, Vector3 barrelPosition)
        {
            if (_canShoot)
            {
                GameObject ball = Instantiate(ballPrefab.gameObject, ballSpawnPoint.position, ballSpawnPoint.rotation, ballParent); 
                var clip = Random.Range(0, shootSounds.Length);
                AudioManager.Instance.Play(shootSounds[clip], transform);
                
                float thrust = Random.Range(thrustMin, thrustMax) * thrustAdjustment;
                
                ball.GetComponent<Rigidbody>().velocity = ballSpawnPoint.transform.up * thrustAdjustment;

                _canShoot = false;
                _shootTimer = shootCooldown;
            }

            _destroyTimer -= Time.deltaTime; // * timerModifier;
        }

        public float GetShootFraction()
        {
            return _shootTimer/shootCooldown;
        }
        
    }
}
