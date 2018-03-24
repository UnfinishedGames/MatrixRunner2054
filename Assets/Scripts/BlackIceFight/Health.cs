using System.Collections;
using UnityEngine;

namespace BlackIceFight
{
    public class Health : MonoBehaviour
    {
        public float StartingHealth = 100;
        public ObjectType Type;
        public ParticleSystem Explosion;

        private float _currentHealth = 2;
        private string _name;
        private EncounterStatus _resultOfDeath;
        private Color _originalColor;

        public EncounterStatus ResultOfDeath
        {
            get { return _resultOfDeath; }
            set { _resultOfDeath = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Use this for initialization
        void Start()
        {
            _currentHealth = StartingHealth;
            _originalColor = gameObject.GetComponent<Renderer>().material.color;
        }

        // Update is called once per frame
        void Update()
        {
        }

        private IEnumerator Flash()
        {
            var material = gameObject.GetComponent<Renderer>().material;
            Color newColor = Color.Lerp(Color.red, _originalColor, _currentHealth / StartingHealth);
            material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            material.color = newColor;
        }

        private void UpdateGUI()
        {
            StartCoroutine(Flash());
            //        UpdateHealthBar();
        }

        private void Die()
        {
            PersistentEncounterStatus.Instance.status = ResultOfDeath;
            //Debug.Log(Name + " I am Dead!");
            var currentTransform = GetComponent<Transform>();
            var explosion = Instantiate(Explosion, currentTransform.position, currentTransform.rotation);
            explosion.transform.Rotate(Vector3.up, 180.0f);
            explosion.Play();
            Destroy(explosion.gameObject, explosion.duration);
            Destroy(gameObject);
        }

        public void TakeDamage(float amountOfDamage)
        {
            _currentHealth -= amountOfDamage;
            UpdateGUI();
            Debug.Log(Name + _currentHealth.ToString());
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }
}