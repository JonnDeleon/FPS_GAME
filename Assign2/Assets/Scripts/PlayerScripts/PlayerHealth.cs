
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 10;
    public int currentHealth;
    public Slider healthSlider;
    public int startingArmor = 3;
    public int currentArmor;
    public Slider armorSlider;
    bool isDead;
    bool damaged;
    public bool isImmune = false;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);



    void Awake()
    {
        currentHealth = startingHealth;
        currentArmor = startingArmor;
    }


    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;
        if (isImmune == true)
        {
            amount = 0;
        }
        if (currentArmor != 0)
        {
            currentArmor -= amount;
            armorSlider.value = currentArmor;
        }
        else
        {
            currentHealth -= amount;
            healthSlider.value = currentHealth;
        }

        

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        SceneManager.LoadScene("Over");
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}