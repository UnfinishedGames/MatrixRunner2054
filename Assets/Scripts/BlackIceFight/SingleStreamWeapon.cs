using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackIceFight
{
    public class SingleStreamWeapon : PauseBehaviour, IWeapon
    {
        public GameObject Bullet;
        public float FireSpeedModifier = 1.0f;
        public BulletDirection Direction = BulletDirection.Down;
        public const float _bulletFireTime = 1.0f;
        public bool AutoFire = true;
        public bool Sweeps = false;

        private float _bulletTimePassed = 0.0f;
        private float lastAngle = 0.0f;
        private int lastDirection = 1;

        private void Start()
        {
            _bulletTimePassed = _bulletFireTime; // So we can shoot immediately
        }

        // Update is called once per frame
        void Update()
        {
            if (AutoFire && !_pause)
            {
                FireBullet();
            }
        }

        private float GetNextAngle()
        {
            lastAngle += lastDirection * 10.0f;
            if (Math.Abs(lastAngle) >= 70)
            {
                lastDirection *= -1;
            }

            return lastAngle;
        }

        public void FireBullet()
        {
            _bulletTimePassed += Time.deltaTime;
            if (_bulletTimePassed * FireSpeedModifier > _bulletFireTime)
            {
                var currentTransform = GetComponent<Transform>();
                var bullet = Instantiate(Bullet, currentTransform.position, currentTransform.rotation);
                var scene = SceneManager.GetSceneByName(PersistentEncounterStatus.Instance.currentFight);
                if (scene.IsValid())
                {
                    SceneManager.MoveGameObjectToScene(bullet, scene);
                }
                else
                {
                    // Debug.Log("Invalid Scene found " + PersistentEncounterStatus.Instance.currentFight);
                }
                var rotation = Quaternion.Euler(Vector3.forward);
                if (Sweeps)
                {
                    rotation = Quaternion.AngleAxis(GetNextAngle(), Vector3.forward);
                }

                bullet.GetComponent<BulletBehaviour>().Fire(Direction, gameObject, rotation);
                _bulletTimePassed = 0.0f;
            }
        }
    }
}