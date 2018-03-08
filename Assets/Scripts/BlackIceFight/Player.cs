using UnityEngine;

//using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float FireSpeedModifier = 1.0f;
    public float MovementModifier = 0.01f;

    public GameObject CurrentBullet;

    private float _bulletTimePassed = 0.0f;
    private const float _bulletFireTime = 1.0f;
    private bool _isMoving = false;

    private void Start()
    {
        _bulletTimePassed = _bulletFireTime;
        GetComponent<Health>().Name = this.ToString();
        GetComponent<Health>().ResultOfDeath = EncounterStatus.PlayerLost;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            RequestFireBullet();
        }

    }

    private void FixedUpdate()
    {
        CheckMovement();
    }

    private void FireBullet(GameObject bulletObject)
    {
        Transform transform = GetComponent<Transform>();
        GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<BulletBehaviour>().Fire(BulletDirection.Up, gameObject);
    }

    private void RequestFireBullet()
    {
        _bulletTimePassed += Time.deltaTime;
        if (_bulletTimePassed * FireSpeedModifier > _bulletFireTime)
        {
            FireBullet(CurrentBullet);
            _bulletTimePassed = 0.0f;
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