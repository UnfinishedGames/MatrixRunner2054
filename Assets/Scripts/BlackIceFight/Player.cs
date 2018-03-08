using UnityEngine;

//using UnityEngine.UI;

namespace BlackIceFight
{
    public class Player : MonoBehaviour
    {
        public float MovementModifier = 0.01f;

        private bool _isMoving = false;
        private Weapon _weapon;

        private void Start()
        {
            GetComponent<Health>().Name = this.ToString();
            GetComponent<Health>().ResultOfDeath = EncounterStatus.PlayerLost;
            _weapon = GetComponent<Weapon>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _weapon.FireBullet();
            }

        }

        private void FixedUpdate()
        {
            CheckMovement();
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