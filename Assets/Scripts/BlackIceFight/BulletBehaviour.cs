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

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
        if (targetRigidbody != null)
        {
            PlayerHealth player = targetRigidbody.GetComponent<PlayerHealth>();
            ICEMovement ice = targetRigidbody.GetComponent<ICEMovement>();
            if (player != null && _origin.GetComponent<PlayerHealth>() != player)
            {
                player.TakeDamage(damage);
//                AudioSource.PlayClipAtPoint(hitSound, transform.position, 10.0f);
                Destroy(gameObject);
            }
            else if(ice != null && _origin.GetComponent<ICEMovement>() != ice)
            {
                ice.TakeDamage(damage);
//                AudioSource.PlayClipAtPoint(hitSound, transform.position, 10.0f);
                Destroy(gameObject);
            }
        }
        else
        {
            // If we destroy the object here, we need to check if we hit friendy objects like the firewall or ourselfs
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