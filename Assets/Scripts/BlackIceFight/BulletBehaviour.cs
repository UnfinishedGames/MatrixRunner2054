using BlackIceFight;
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

    private bool IsTargetType(GameObject other, GameObject self)
    {
        var otherHealth = other.GetComponent<Health>();
        var selfHealth = self.GetComponent<Health>();
        var result = false;
        if (otherHealth && selfHealth)
        {
            if (otherHealth.Type != selfHealth.Type
                && otherHealth.Type != ObjectType.Bystander
                && selfHealth.Type != ObjectType.Bystander)
            {
                result = true;
            }
        }

        return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();
        if (targetRigidbody != null
            && _origin != null
            && IsTargetType(other.gameObject, _origin))
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



    public void Fire(BulletDirection bulletDirection, GameObject origin, Quaternion rotation)
    {
//        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        _origin = origin;
        Destroy(gameObject, TIME_TO_LIVE_SEC);
        Rigidbody body = GetComponent<Rigidbody>();

        var myVector = rotation * Vector3.up;
        body.transform.rotation = rotation;
        body.AddForce(myVector * (int) bulletDirection * ForwardForce);
    }
}