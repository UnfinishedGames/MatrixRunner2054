using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlackIceFight
{
    public class ICEMovement : PauseBehaviour
    {
        /// The absolute range the object can move within
        public float RangeX = 3.0f;

        /// The speed of the basic speed of the object - only to be set by the GUI
        public float Speed = 1.0f;

        /// Indicates if the object is currently moving - if not a new destination is calulated
        private bool _isMoving = false;

        /// The point the object currently moves to
        private Vector3 _destination;

        /// Modifies the current speed of the object - can be changed if some event occurs
        private float _speedModifier = 1.0f;

        /// Use this for initialization
        void Start()
        {
            Random.InitState(Time.frameCount);
            GetComponent<Health>().Name = this.ToString();
            GetComponent<Health>().ResultOfDeath = EncounterStatus.PlayerWins;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_pause)
            {
                Move();
            }
        }

        /// <summary>
        /// The logic that moves the ICE. If it is already moving the positions get updated.
        /// The position is updated with translate, that means we get closer to the destination by every frame.
        /// If it has arrived at the destionation, a new destination is calculated.
        /// The movement speed can be influenced by _speedModifier.
        /// </summary>
        private void Move()
        {
            if (!_isMoving)
            {
                CalculateNewDestination();
                _isMoving = true;
            }
            else
            {
                transform.Translate(new Vector3(
                                        Math.Sign(_destination.x), 0, 0) * Time.deltaTime * Speed *_speedModifier
                                    );
                if (OnDestination(_destination))
                {
                    _isMoving = false;
                }
            }
        }

        /// <summary>
        /// Calculates the new random destination. The movement direction is changed every time the function is called.
        /// </summary>
        private void CalculateNewDestination()
        {
            var formerDirection = Math.Sign(_destination.x);
            _destination = new Vector3(Random.Range(-1 * RangeX, RangeX), 0, 0);
            if (Math.Sign(_destination.x) == formerDirection)
            {
                _destination *= -1;
            }
        }

        /// <summary>
        /// Checks if the object has arrived at the destination or at least withing a range arround the destination.
        /// The exact destination might never be hit!
        /// </summary>
        /// <param name="destination">The target destination</param>
        /// <returns>True if at the destination</returns>
        private bool OnDestination(Vector3 destination)
        {
            const float
                deltaValue = 0.01f; // used to avoid deadlocks when the object cannot be transformed that small value
            var onDestination = false;
            if (Math.Sign(_destination.x) == -1)
            {
                if (destination.x > (transform.position.x + deltaValue))
                {
                    onDestination = true;
                }
            }
            else
            {
                if (destination.x < (transform.position.x - deltaValue))
                {
                    onDestination = true;
                }
            }

            return onDestination;
        }

        /// <summary>
        /// Is called by a dying child. 
        /// </summary>
        public void OnChildDied()
        {
            //switchWeapon()
            _speedModifier *= 1.5f;
        }
    }
}