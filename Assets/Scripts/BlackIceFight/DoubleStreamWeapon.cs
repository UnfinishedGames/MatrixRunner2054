using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackIceFight
{
    public class DoubleStreamWeapon : PauseBehaviour, IWeapon
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
                var bullet1 = Instantiate(Bullet, currentTransform.position, currentTransform.rotation);
                var bullet2 = Instantiate(Bullet, currentTransform.position, currentTransform.rotation);
                var scene = SceneManager.GetSceneByName(PersistentEncounterStatus.Instance.currentFight);
                if (scene.IsValid())
                {
                    SceneManager.MoveGameObjectToScene(bullet1, scene);
                    SceneManager.MoveGameObjectToScene(bullet2, scene);
                }
                else
                {
                    // Debug.Log("Invalid Scene found " + PersistentEncounterStatus.Instance.currentFight);
                }

                var angle = GetNextAngle();
                var rotation1 = Quaternion.AngleAxis(angle, Vector3.forward);
                var rotation2 = Quaternion.AngleAxis(-angle, Vector3.forward);

                bullet1.GetComponent<BulletBehaviour>().Fire(Direction, gameObject, rotation1);
                bullet2.GetComponent<BulletBehaviour>().Fire(Direction, gameObject, rotation2);
                _bulletTimePassed = 0.0f;
            }
        }
    }
}