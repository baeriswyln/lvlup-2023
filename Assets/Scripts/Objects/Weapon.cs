using System;
using System.Collections.Generic;
using System.Timers;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Objects
{
    public class Weapon : MonoBehaviour
    {
        public enum WeaponType
        {
            Shotgun,
            LongRange,
            Pistol
        }

        public float fireInterval;
        public float bulletSpeed;
        public float range;
        public float damage;
        public int bulletsPerShot = 1;

        public GameObject bullet;
        public ProgressBar bar;

        private Vector3 _offset;

        private int counter;
        private int countToFire;
        private float intervalSec = 0.1f;

        public void Start()
        {
            counter = 0;
            countToFire = (int)(fireInterval / intervalSec);
            InvokeRepeating("IntervalUpdate", intervalSec, intervalSec);
        }

        void IntervalUpdate()
        {
            counter++;
            bar.SetProgress(counter, countToFire);
        }

        public void Shoot(AudioSource source, AudioClip clip)
        {
            if (counter < countToFire) return;

            counter = 0;
            source.pitch = Random.Range(0.8f, 1.2f);
            source.PlayOneShot(clip);
            
            // show particles with initial speed
            GameObject bulletInstance;

            // shoot bullets adding a spray
            for (var i = 0; i < bulletsPerShot; i++)
            {
                var eulerAnglesZ = transform.rotation.eulerAngles.z;
                // remove half of the total angle between outer most shots (centers the shots)
                eulerAnglesZ -= (bulletsPerShot - 1) * Globals.ShootingAngleDelta / 2f;
                // add offset for current shot
                eulerAnglesZ += Globals.ShootingAngleDelta * i;
                var rotation = Quaternion.Euler(0, 0, eulerAnglesZ);

                // now, the same things must be done for the initial position. otherwise, the bullets collide and spread
                // around uncontrolled.
                var bulletWidth = bullet.transform.localScale.x * 2f;
                var offset = transform.up;
                offset -= ((bulletsPerShot - 1) * bulletWidth) * -transform.right;
                offset += (bulletWidth * i) * -transform.right;

                bulletInstance = Instantiate(bullet, transform.position + offset, rotation);
                bulletInstance.AddComponent<Bullet>().SetProperties(this);
            }
        }

        public class InitValues
        {
            public readonly float Range;
            public readonly float Damage;
            public readonly int BulletsPerShot;
            public readonly float FireInterval;

            public InitValues(float range, float damage, int bulletsPerShot, float fireInterval)
            {
                Range = range;
                Damage = damage;
                BulletsPerShot = bulletsPerShot;
                FireInterval = fireInterval;
            }
        }
    }
}