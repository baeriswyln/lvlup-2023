using System;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Objects
{
    public class Weapon : MonoBehaviour
    {
        [FormerlySerializedAs("FireInterval")] public float fireInterval;
        [FormerlySerializedAs("BulletSpeed")] public float bulletSpeed;
        [FormerlySerializedAs("Range")] public float range;
        [FormerlySerializedAs("Damage")] public float damage;
        public int bulletsPerShot = 1;

        public GameObject bullet;

        private Vector3 _offset;
        
        public void Start()
        {
            InvokeRepeating("Shoot", fireInterval, fireInterval);
        }

        void Shoot()
        {
            // show particles with initial speed
            GameObject bulletInstance;
            Vector3 offset = transform.up;


            bulletInstance = Instantiate(bullet, transform.position + offset, transform.rotation);
            
            Bullet b = bulletInstance.AddComponent<Bullet>();
            b.SetProperties(this);
        }
    }
}
