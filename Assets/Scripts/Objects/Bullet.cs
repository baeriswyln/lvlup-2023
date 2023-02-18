using System;
using DefaultNamespace;
using UnityEngine;

namespace Objects
{
    public class Bullet : MonoBehaviour
    {
        private Weapon _weapon;

        private Vector3 _lastPosition;
        private Vector2 _lastVelocity;
        
        private float _distanceTraveled;
        private Rigidbody2D _rb;

        public void SetProperties(Weapon weapon)
        {
            _weapon = weapon;
        }

        public float GetDamage()
        {
            return _weapon.damage;
        }

        private void Start()
        {
            _lastPosition = transform.position;
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = transform.up * (_weapon.bulletSpeed * Globals.SpeedAdjFactor);
            _lastVelocity = _rb.velocity;
        }

        private void FixedUpdate()
        {
            if (_distanceTraveled > _weapon.range)
            {
                Destroy(gameObject);
            }

            _distanceTraveled += Vector3.Distance(_lastPosition, transform.position);
            _lastPosition = transform.position;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _rb.velocity = Vector2.Reflect(_lastVelocity, col.contacts[0].normal);
            _lastVelocity = _rb.velocity;
        }
    }
}