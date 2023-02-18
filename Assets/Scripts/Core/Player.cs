using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Objects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public int movementSpeed = 1;
        public int turningSpeed = 1;

        public Weapon currentWeapon;
        
        // todo: remove and replace with map
        public KeyCode keyRight;
        public KeyCode keyLeft;
        public KeyCode keyAction1;
        public KeyCode keyAction2;

        private float _health = 10;

        private PlayerData _playerData;
        private Rigidbody2D _rb;
        private Controller _ctrl;

        private const int TurningSpeedAdjustment = 200;

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _ctrl = GameObject.Find(Globals.Controller).GetComponent<Controller>();
        }

        // Updated 60 times per seconds
        private void FixedUpdate()
        {
            if (Input.GetKey(keyRight) && !Input.GetKey(keyLeft))
            {
                // turn right
                transform.Rotate(Vector3.back * (turningSpeed * TurningSpeedAdjustment * Time.fixedDeltaTime));
            }

            if (!Input.GetKey(keyRight) && Input.GetKey(keyLeft))
            {
                // turn left
                transform.Rotate(Vector3.forward * (turningSpeed * TurningSpeedAdjustment * Time.fixedDeltaTime));
            }
            
            if (!(Input.GetKey(keyRight) && Input.GetKey(keyLeft)))
            {
                // go in forward facing direction
                _rb.velocity = transform.up * (movementSpeed * Globals.SpeedAdjFactor);
            }
            else
            {
                _rb.velocity = Vector2.zero;
            }

            if (_health <= 0)
            {
                _ctrl.AnnounceDeath(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var b = col.gameObject.GetComponent<Bullet>();
            if (b != null)
            {
                _health -= b.GetDamage();
                Destroy(col.gameObject);
            }
        }

        public void Initialize(PlayerData p)
        {
            keyLeft = p.KeyMap[Globals.KeyFunctions.TurnLeft];
            keyRight = p.KeyMap[Globals.KeyFunctions.TurnRight];
            keyAction1 = p.KeyMap[Globals.KeyFunctions.Action1];
            keyAction2 = p.KeyMap[Globals.KeyFunctions.Action2];

            _health = p.InitialHealth;

            transform.Find(Globals.PlayerHeadSprite).GetComponent<SpriteRenderer>().color = p.Color;
        }

        public PlayerData GetData()
        {
            return _playerData;
        }
    }

    public class PlayerData
    {
        public readonly Dictionary<string, KeyCode> KeyMap;
        public string Name;
        public Color Color;
        
        public float InitialHealth = 10;

        public PlayerData(Dictionary<string, KeyCode> keyMap, Color color)
        {
            KeyMap = keyMap;
            Color = color;
        }
    }
}