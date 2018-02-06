using UnityEngine;

//using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public string myName;
    public float startingHealth = 100;
    //    public Slider healthBar;
    private float currentHealth;
    //    public CountWins counter;
    //    public GameObject playerSpawn;

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
        if (currentHealth <= 0)
        {
            Debug.Log("I am Dead!");
//            counter.IAmDead(myName);
        }
    }

    //    private void UpdateHealthBar()
    //    {
    //        healthBar.value = currentHealth;
    //    }
}