using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public float movementSpeed = 1;
        public float turningSpeed = 1;

        public Weapon currentWeapon;
        public GameObject sprite;
        public ProgressBar healthBar;
        public ProgressBar loadBar;
        public GameObject bars;
        public Animator animator;
        public SpriteRenderer playerRenderer;
        public SpriteRenderer arrowRenderer;
        public GameObject damageAnimation;
        public AudioSource footsteps;
        public AudioSource hit;
        public AudioSource gun;
        public AudioClip gunClip;
        public AudioClip shotgunClip;
        public AudioClip longRangeClip;

        // todo: remove and replace with map
        public KeyCode keyRight;
        public KeyCode keyLeft;
        [FormerlySerializedAs("keyAction1")] public KeyCode keyShoot;
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
            playerRenderer.color = _playerData.Color;
            Color arrowColor = new Color(_playerData.Color.r, _playerData.Color.g, _playerData.Color.b, ArrowAlpha);
            arrowRenderer.color = arrowColor;
        }

        private void PlayFootStep()
        {
            if (!footsteps.isPlaying)
            {
                footsteps.Play(0);
            }
        }

        // Updated 60 times per seconds
        private void FixedUpdate()
        {
            bool moving = true;
            if (Input.GetKey(keyRight) && !Input.GetKey(keyLeft))
            {
                // turn right
                Vector3 rot = Vector3.back * (turningSpeed * TurningSpeedAdjustment * Time.fixedDeltaTime);
                transform.Rotate(rot);
                sprite.transform.Rotate(-rot);
                bars.transform.Rotate(-rot);
                PlayFootStep();
            }

            if (!Input.GetKey(keyRight) && Input.GetKey(keyLeft))
            {
                // turn left
                Vector3 rot = Vector3.forward * (turningSpeed * TurningSpeedAdjustment * Time.fixedDeltaTime);
                transform.Rotate(rot);
                sprite.transform.Rotate(-rot);
                bars.transform.Rotate(-rot);
                PlayFootStep();
            }
            
            if (!(Input.GetKey(keyRight) && Input.GetKey(keyLeft)))
            {
                // go in forward facing direction
                _rb.velocity = transform.up * (movementSpeed * Globals.SpeedAdjFactor);
                PlayFootStep();
            }
            else
            {
                // stop if both keys are pressed
                footsteps.Stop();
                moving = false;
                _rb.velocity = Vector2.zero;
            }

            if (Input.GetKey(keyShoot))
            {
                // shoot. this only works after a cooldown
                AudioClip clip = gunClip;
                switch (_playerData.weaponType)
                {
                    case Weapon.WeaponType.Pistol:
                        clip = gunClip;
                        break;
                    case Weapon.WeaponType.Shotgun:
                        clip = shotgunClip;
                        break;
                    case Weapon.WeaponType.LongRange:
                        clip = longRangeClip;
                        break;
                }
                currentWeapon.Shoot(gun, clip);
            }

            if (_health <= 0)
            {
                // player died. anounce death
                _ctrl.AnnounceDeath(this);
            }

            animator.SetInteger("direction", ((int)(transform.rotation.eulerAngles.z + 22.5d) / 45 + 10) % 8);
            animator.SetBool("moving", moving);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var b = col.gameObject.GetComponent<Bullet>();
            if (b != null)
            {
                // soundo
                hit.pitch = Random.Range(0.8f, 1.2f);
                hit.Play(0);
                // decrease health
                _health -= b.GetDamage();
                healthBar.SetProgress(_health, _playerData.InitialHealth);
                
                // show blood animation
                var bloodAnimation = Instantiate(damageAnimation);
                bloodAnimation.transform.position = col.transform.position;
                
                // destroy hitting bullet
                Destroy(col.gameObject);
            }
        }

        private void SetColor(Color color)
        {
            playerRenderer.color = color;
            Color arrowColor = new Color(color.r, color.g, color.b, ArrowAlpha);
            arrowRenderer.color = arrowColor;
        }

        public void Initialize(PlayerData p)
        {
            // Color
            SetColor(p.Color);
            
            // Keys
            keyLeft = p.KeyMap[Globals.KeyFunctions.TurnLeft];
            keyRight = p.KeyMap[Globals.KeyFunctions.TurnRight];
            keyShoot = p.KeyMap[Globals.KeyFunctions.Shoot];
            keyAction2 = p.KeyMap[Globals.KeyFunctions.Ability];

            // Player properties
            _health = p.InitialHealth;
            movementSpeed = p.MovementSpeed;
            turningSpeed = p.TurningSpeed;

            currentWeapon.range = p.FireRange;
            currentWeapon.damage = p.FireDamage;
            currentWeapon.fireInterval = p.FireInterval;
            currentWeapon.bulletsPerShot = p.BulletsPerShot;

            _playerData = p;

            transform.Find(Globals.PlayerHeadSprite).GetComponent<SpriteRenderer>().color = p.Color;
            _playerData = p;
            healthBar.SetProgress(_health, _playerData.InitialHealth);
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
        public Sprite Image;
        public Weapon.WeaponType weaponType;

        public int Points;

        public float InitialHealth = Globals.InitPlayerVals.Health;
        public float MovementSpeed = Globals.InitPlayerVals.MovementSpeed;
        public float TurningSpeed = Globals.InitPlayerVals.TurningSpeed;
        
        public float FireInterval;
        public float FireRange;
        public float FireDamage;
        public readonly int BulletsPerShot;

        public PlayerData(Dictionary<string, KeyCode> keyMap, Color color, string name, Sprite image, Weapon.WeaponType type)
        {
            KeyMap = keyMap;
            Color = color;
            Name = name;
            Image = image;
            weaponType = weaponType;

            FireInterval = Globals.InitWeaponValues[type].FireInterval;
            FireRange = Globals.InitWeaponValues[type].Range;
            FireDamage = Globals.InitWeaponValues[type].Damage;
            BulletsPerShot = Globals.InitWeaponValues[type].BulletsPerShot;
        }
    }
}