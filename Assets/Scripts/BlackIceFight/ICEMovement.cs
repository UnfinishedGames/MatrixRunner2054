using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ICEMovement : MonoBehaviour
{
    public float RangeX = 3.0f;
    public float Speed = 1.0f;
    public GameObject GenericBullet;
    public GameObject SweeperBullet;
    public float FireSpeedModifier = 1.0f;

    private bool _isMoving = false;
    private Vector3 _destination;
    private float _speedModifier;
    private float _bulletTimePassedGeneric = 0.0f;
    private float _bulletTimePassedSweeper = 0.0f;
    private const float _bulletFireTimeGeneric = 1.0f;
    private const float _bulletFireTimeSweeper = 1.0f;
    private float currentHealth = 2;

    // Use this for initialization
    void Start()
    {
        Random.InitState(Time.frameCount);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FireGenericBullet();
        FireSweeperBullet();
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

    private void FireGenericBullet()
    {
        _bulletTimePassedGeneric += Time.deltaTime;
        if (_bulletTimePassedGeneric * FireSpeedModifier > _bulletFireTimeGeneric)
        {
            FireBullet(GenericBullet);
            _bulletTimePassedGeneric = 0.0f;
        }
    }

    private void FireSweeperBullet()
    {
        _bulletTimePassedSweeper += Time.deltaTime;
        if (_bulletTimePassedSweeper * FireSpeedModifier > _bulletFireTimeSweeper)
        {
            FireBullet(SweeperBullet);
            _bulletTimePassedSweeper = 0.0f;
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

    private void FireBullet(GameObject bulletType)
    {
        Transform transform = GetComponent<Transform>();
        GameObject bullet = Instantiate(bulletType, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<BulletBehaviour>().Fire(BulletDirection.Down, gameObject);
    }

    public void TakeDamage(float amountOfDamage)
    {
        currentHealth -= amountOfDamage;
//        UpdateHealthBar();
        Debug.Log("ICE: " + currentHealth.ToString());
        if (currentHealth <= 0)
        {
            PersistentEncounterStatus.FetchPersistentStatus().status = EncounterStatus.PlayerWins;
            Debug.Log("ICE: I am Dead!");
//            counter.IAmDead(myName);
        }
    }
}