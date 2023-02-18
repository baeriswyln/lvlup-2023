using System;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

namespace Objects
{
    public class Weapon : MonoBehaviour
    {
        public float FireInterval;
        public float BulletSpeed;
        public float Range;
        public float Damage;

        public GameObject bullet;

        private Vector3 _offset;
        
        public void Start()
        {
            InvokeRepeating("Shoot", FireInterval, FireInterval);
        }

        void Shoot()
        {
            // show particles with initial speed
            GameObject bulletInstance;
            Vector3 offset = transform.up;


            bulletInstance = Instantiate(bullet, transform.position + offset, transform.rotation);
            // bulletInstance.GetComponent<SpriteRenderer>().enabled = true;
            // bulletInstance.GetComponent<CircleCollider2D>().enabled = true;
            
            Bullet b = bulletInstance.AddComponent<Bullet>();
            b.SetProperties(this);
        }
    }
}
