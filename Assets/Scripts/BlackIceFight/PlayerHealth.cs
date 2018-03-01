using UnityEngine;

//using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float FireSpeedModifier = 1.0f;

    public GameObject CurrentBullet;

    //    public Slider healthBar;
    private float currentHealth;

    //    public CountWins counter;
    //    public GameObject playerSpawn;
    private float _bulletTimePassed = 0.0f;
    private const float _bulletFireTime = 1.0f;
    private bool isMoving = false;

    private void Start()
    {
        _bulletTimePassed = _bulletFireTime;
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

    private void OnEnable()
    {
        currentHealth = startingHealth;
//        UpdateHealthBar();
//        transform.position = playerSpawn.transform.position;
    }

    public void TakeDamage(float amountOfDamage)
    {
        currentHealth -= amountOfDamage;
//        UpdateHealthBar();
        Debug.Log(currentHealth.ToString());
        if (currentHealth <= 0)
        {
            PersistentEncounterStatus.FetchPersistentStatus().status = EncounterStatus.PlayerLost;
            Debug.Log("I am Dead!");
//            counter.IAmDead(myName);
        }
    }

    private void CheckMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rigidbody body = GetComponent<Rigidbody>();
            body.MovePosition(body.position + Vector3.left * 0.01f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Rigidbody body = GetComponent<Rigidbody>();
            body.MovePosition(body.position + Vector3.right * 0.01f);
        }
    }

    //    private void UpdateHealthBar()
    //    {
    //        healthBar.value = currentHealth;
    //    }
}