using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public int movementSpeed = 1;
        public int turningSpeed = 1;

        public Weapon currentWeapon;
        public GameObject sprite;
        public Animator animator;
        public SpriteRenderer playerRenderer;
        public SpriteRenderer arrowRenderer;
        public Color playerColor;
        
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
        private const float ArrowAlpha = 0.3f;

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _ctrl = GameObject.Find(Globals.Controller).GetComponent<Controller>();
            playerRenderer.color = playerColor;
            Color arrowColor = new Color(playerColor.r, playerColor.g, playerColor.b, ArrowAlpha);
            arrowRenderer.color = arrowColor;
        }

        // Updated 60 times per seconds
        private void FixedUpdate()
        {
            if (Input.GetKey(keyRight) && !Input.GetKey(keyLeft))
            {
                // turn right
                Vector3 rot = Vector3.back * (turningSpeed * TurningSpeedAdjustment * Time.fixedDeltaTime);
                transform.Rotate(rot);
                sprite.transform.Rotate(-rot);
            }

            if (!Input.GetKey(keyRight) && Input.GetKey(keyLeft))
            {
                // turn left
                Vector3 rot = Vector3.forward * (turningSpeed * TurningSpeedAdjustment * Time.fixedDeltaTime);
                transform.Rotate(rot);
                sprite.transform.Rotate(-rot);
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
            animator.SetInteger("direction", ((int)(transform.rotation.eulerAngles.z + 22.5d) / 45 + 10) % 8);
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
            _playerData = p;

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
        
        public float InitialHealth = 2;

        public PlayerData(Dictionary<string, KeyCode> keyMap, Color color, string name)
        {
            KeyMap = keyMap;
            Color = color;
            Name = name;
        }
    }
}