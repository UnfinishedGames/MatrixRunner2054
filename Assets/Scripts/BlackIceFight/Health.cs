using System.Collections;
using UnityEngine;

namespace BlackIceFight
{
    public class Health : MonoBehaviour
    {
        public float StartingHealth = 100;
        public ObjectType Type;

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
            Color newColor = Color.Lerp(Color.red, _originalColor, _currentHealth / 100.0f);
            material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            material.color = Color.grey;
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

        public void TakeDamage(float amountOfDamage)
        {
            _currentHealth -= amountOfDamage;
            UpdateGUI();
            Debug.Log(Name + _currentHealth.ToString());
            if (_currentHealth <= 0)
            {
                PersistentEncounterStatus.Instance.status = ResultOfDeath;
                Debug.Log(Name + " I am Dead!");
                Destroy(gameObject);
//            counter.IAmDead(myName);
            }
        }
    }
}