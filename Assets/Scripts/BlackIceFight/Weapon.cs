using UnityEngine;

namespace BlackIceFight
{
    public class Weapon : MonoBehaviour
    {
        public GameObject Bullet;
        public float FireSpeedModifier = 1.0f;
        public BulletDirection Direction = BulletDirection.Down;
        public const float _bulletFireTime = 1.0f;
        public bool AutoFire = true;

        private float _bulletTimePassed = 0.0f;

        private void Start()
        {
            _bulletTimePassed = _bulletFireTime; // So we can shoot immediately
        }

        // Update is called once per frame
        void Update()
        {
            if (AutoFire)
            {
                FireBullet();
            }
        }

        public void FireBullet()
        {
            _bulletTimePassed += Time.deltaTime;
            if (_bulletTimePassed * FireSpeedModifier > _bulletFireTime)
            {
                Transform currentTransform = GetComponent<Transform>();
                GameObject bullet = Instantiate(Bullet, currentTransform.position, currentTransform.rotation);
                bullet.GetComponent<BulletBehaviour>().Fire(Direction, gameObject);
                _bulletTimePassed = 0.0f;
            }
        }
    }
}