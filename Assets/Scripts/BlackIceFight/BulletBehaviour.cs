using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public AudioClip shootSound;
    public AudioClip hitSound;
    public float ForwardForce = 100;

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
            if (player != null)
            {
                player.TakeDamage(damage);
//                AudioSource.PlayClipAtPoint(hitSound, transform.position, 10.0f);
                Destroy(gameObject);
            }
        }
        else
        {
            // If we destroy the object here, we need to check if we hit friendy objects like the firewall or ourselfs
        }
    }

    public void fire()
    {
//        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        Destroy(gameObject, TIME_TO_LIVE_SEC);
        Rigidbody body = GetComponent<Rigidbody>();
        body.AddForce(transform.up * -ForwardForce);
    }
}