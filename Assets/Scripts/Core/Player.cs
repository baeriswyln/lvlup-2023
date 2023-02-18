using System;
using DefaultNamespace;
using Objects;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public int MovementSpeed = 1;
        public int TurningSpeed = 1;

        public Weapon CurrentWeapon;
        public float Health;

        public KeyCode KeyRight;
        public KeyCode KeyLeft;
        public KeyCode KeyEvent1;
        public KeyCode KeyEvent2;

        private Rigidbody2D _rb;

        private const int MovementSpeedAdjustment = 200;
        private const int TurningSpeedAdjustment = 200;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Updated 60 times per seconds
        private void FixedUpdate()
        {
            if (Input.GetKey(KeyRight) && !Input.GetKey(KeyLeft))
            {
                // turn right
                transform.Rotate(Vector3.back * (TurningSpeed * TurningSpeedAdjustment * Time.fixedDeltaTime));
            }

            if (!Input.GetKey(KeyRight) && Input.GetKey(KeyLeft))
            {
                // turn left
                transform.Rotate(Vector3.forward * (TurningSpeed * TurningSpeedAdjustment * Time.fixedDeltaTime));
            }
            
            if (!(Input.GetKey(KeyRight) && Input.GetKey(KeyLeft)))
            {
                // go in forward facing direction
                _rb.velocity = transform.up * (MovementSpeed * Globals.SpeedAdjFactor);
            }
            else
            {
                _rb.velocity = Vector2.zero;
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var b = col.gameObject.GetComponent<Bullet>();
            if (b != null)
            {
                Health -= b.GetDamage();
                Destroy(col.gameObject);
            }
        }
    }
}