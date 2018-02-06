using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public AudioClip shootSound;
    public AudioClip hitSound;
    private float damage = 1.0f;

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
            }
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    public void fire()
    {
//        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        Destroy(gameObject, 5.0f);
        Rigidbody body = GetComponent<Rigidbody>();
        body.AddForce(transform.up * -1000);
    }
}