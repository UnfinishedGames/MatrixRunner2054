using UnityEngine;

public enum BulletDirection
{
    Up = 1,
    Down = -1
}

public class BulletBehaviour : MonoBehaviour
{
    public AudioClip shootSound;
    public AudioClip hitSound;
    public float ForwardForce = 100;

    private GameObject _origin;
    private float damage = 1.0f;
    protected const float TIME_TO_LIVE_SEC = 10.0f;

    void Start()
    {
    }

    void Awake()
    {
    }

    public void Update()
    {
        if (PersistentEncounterStatus.Instance.status == EncounterStatus.Unavailable &&
            gameObject.name.Contains("clone"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
        if (targetRigidbody != null
            && targetRigidbody != _origin.GetComponent<Rigidbody>())
        {
            Health health = targetRigidbody.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
//                AudioSource.PlayClipAtPoint(hitSound, transform.position, 10.0f);
                Destroy(gameObject);
            }
        }
    }
    
    public void Fire(BulletDirection bulletDirection, GameObject origin)
    {
//        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        _origin = origin;
        Destroy(gameObject, TIME_TO_LIVE_SEC);
        Rigidbody body = GetComponent<Rigidbody>();
        body.AddForce(transform.up * (int)bulletDirection * ForwardForce);
    }
}