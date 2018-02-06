using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ICEMovement : MonoBehaviour
{
    public float RangeX = 3.0f;
    public float Speed = 1.0f;
    public GameObject _currentBullet;

    private bool _isMoving = false;
    private Vector3 _destination;
    private float _speedModifier;
    private float _bulletTimePassed = 0.0f;
    private const float _bulletFireTime = 2.0f;

    // Use this for initialization
    void Start()
    {
        Random.InitState(Time.frameCount);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FireBullet();
    }

    private void Move()
    {
        if (!_isMoving)
        {
            _speedModifier = Random.Range(0.8f, 1.2f);
            var formerDirection = Math.Sign(_destination.x);
            _destination = new Vector3(Random.Range(-1 * RangeX, RangeX), 0, 0);
            if (Math.Sign(_destination.x) == formerDirection)
            {
                _destination *= -1;
            }

            _isMoving = true;
        }
        else
        {
            transform.Translate(new Vector3(Math.Sign(_destination.x), 0, 0) * Time.deltaTime * Speed *
            _speedModifier);
            if (OnDestination(_destination))
            {
                _isMoving = false;
            }
        }
    }

    private void FireBullet()
    {
        _bulletTimePassed += Time.deltaTime;
        if (_bulletTimePassed > _bulletFireTime)
        {
            FireWeapon();
            _bulletTimePassed = 0.0f;
        }
    }

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

    private void FireWeapon()
    {
        Transform transform = GetComponent<Transform>();
        GameObject bullet = Instantiate(_currentBullet, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<BulletBehaviour>().fire();
    }
}