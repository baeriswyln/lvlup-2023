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

        public GameObject bullet;
        public ProgressBar bar;

        private Vector3 _offset;

        private int counter;
        private int countToFire;
        private float intervalSec = 0.1f;

        public void Start()
        {
            counter = 0;
            countToFire = (int) (fireInterval / intervalSec);
            InvokeRepeating("IntervalUpdate", intervalSec, intervalSec);
        }

        void IntervalUpdate()
        {
            counter++;
            bar.SetProgress(counter, countToFire);
            if (counter >= countToFire)
            {
                Shoot();
                counter = 0;
            }
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
