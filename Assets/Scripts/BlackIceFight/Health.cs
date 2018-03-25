using System.Collections;
using System.Linq;
using UnityEngine;

namespace BlackIceFight
{
    public class Health : MonoBehaviour
    {
        public float StartingHealth = 100;
        public ObjectType Type;
        public ParticleSystem Explosion;
        
        /// As long as the childrens live, the object is invulnerable 
        private Health[] ChildrenList; 
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
            ChildrenList = GetComponentInChildrenExclusively();
        }

        private Health[] GetComponentInChildrenExclusively()
        {
            var childrenList = gameObject.GetComponentsInChildren<Health>();
            childrenList = childrenList.Where(val => val != this).ToArray();
            return childrenList;
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// Flashes the object. Be aware that this is also the damage indicator.
        /// We flash white first, to indicate a hit, then we show the damage color for a
        /// short while and switch back to tht original color.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Flash()
        {
            var material = gameObject.GetComponent<Renderer>().material;
            Color damageColor = Color.Lerp(Color.red, _originalColor, _currentHealth / StartingHealth);
            material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            material.color = damageColor;
            yield return new WaitForSeconds(0.1f);
            material.color = _originalColor;
        }

        private void UpdateGUI()
        {
            StartCoroutine(Flash());
            //        UpdateHealthBar();
        }

        private void ShowDyingExplosion()
        {
            var currentTransform = GetComponent<Transform>();
            var explosion = Instantiate(Explosion, currentTransform.position, currentTransform.rotation);
            explosion.transform.Rotate(Vector3.up, 180.0f);
            explosion.Play();
            Destroy(explosion.gameObject, explosion.duration);
        }

        private void InformParentOfDeath()
        {
            ICEMovement ice = GetComponentInParent<ICEMovement>();
            if (ice)
            {
                ice.OnChildDied();
            }
        }
        
        private void Die()
        {
            PersistentEncounterStatus.Instance.status = ResultOfDeath; // TODO: move to a monitoring instance
            //Debug.Log(Name + " I am Dead!");
            ShowDyingExplosion();
            InformParentOfDeath();
            Destroy(gameObject);
        }

        private bool IsInvulnerable()
        {
            var isInvulerable = false;
            if (ChildrenList != null)
            {
                foreach (var child in ChildrenList)
                {
                    if (null != child)
                    {
                        isInvulerable = true;
                    }
                }
            }

            return isInvulerable;
        }

        public void TakeDamage(float amountOfDamage)
        {
            if (!IsInvulnerable())
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
}