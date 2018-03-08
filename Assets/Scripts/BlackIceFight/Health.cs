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
        }

        // Update is called once per frame
        void Update()
        {
        }
    
        public void TakeDamage(float amountOfDamage)
        {
            _currentHealth -= amountOfDamage;
//        UpdateHealthBar();
            Debug.Log(Name + _currentHealth.ToString());
            if (_currentHealth <= 0)
            {
                PersistentEncounterStatus.Instance.status = ResultOfDeath;
                Debug.Log(Name + " I am Dead!");
//            counter.IAmDead(myName);
            }
        }
    }
}
