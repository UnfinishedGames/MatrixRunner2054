using UnityEngine;

namespace BlackIceFight
{
    public class Weapon : MonoBehaviour
    {
        public GameObject Bullet;
        public float FireSpeedModifier = 1.0f;
        public BulletDirection Direction = BulletDirection.Down;
        public const float _bulletFireTime = 1.0f;

        private float _bulletTimePassed = 0.0f;

        // Update is called once per frame
        void Update()
        {
            FireBullet();
        }

        private void FireBullet()
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