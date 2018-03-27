using UnityEngine;

//using UnityEngine.UI;

namespace BlackIceFight
{
    public class Player : PauseBehaviour
    {
        public float MovementModifier = 0.01f;
        public GameObject Weapon;

        private bool _isMoving = false;

        private void Start()
        {
            GetComponent<Health>().Name = this.ToString();
            GetComponent<Health>().ResultOfDeath = EncounterStatus.PlayerLost;
        }

        private void Update()
        {
            if (!_pause)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Weapon.GetComponent<IWeapon>().FireBullet();
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_pause)
            {
                CheckMovement();
            }
        }

        private void CheckMovement()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Rigidbody body = GetComponent<Rigidbody>();
                body.MovePosition(body.position + Vector3.left * MovementModifier);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                Rigidbody body = GetComponent<Rigidbody>();
                body.MovePosition(body.position + Vector3.right * MovementModifier);
            }
        }
    }
}