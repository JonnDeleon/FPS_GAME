using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemyManager enemyManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HealthPack")
        {
            Debug.Log("HealthPack Hit");
            if (playerHealth.currentHealth < 10)
            {
                playerHealth.currentHealth++;
                playerHealth.healthSlider.value = playerHealth.currentHealth;
                Destroy(other.gameObject);
            }
        }
        else if(other.gameObject.tag == "ArmorUp")
        {
            Debug.Log("ArmorUPHit");
            if (playerHealth.currentArmor < 3)
            {
                playerHealth.currentArmor++;
                playerHealth.armorSlider.value = playerHealth.currentArmor;
                Destroy(other.gameObject);
            }
        }
        else if(other.gameObject.tag == "Immunity")
        {
            Debug.Log("Immune Hit");
            playerHealth.isImmune = true;
            Invoke("SetBoolBack", 5);
            Destroy(other.gameObject);


        }
        else if (other.gameObject.tag == "Nuke")
        {
            Debug.Log("Nuke HIT");
            for (int i = 0; i < enemyManager.currentEC; i++)
            {
                Destroy(enemyManager.curEnemyCount[i]);
            }
            Destroy(other.gameObject);
        }

    }
    private void SetBoolBack()
    {
        playerHealth.isImmune = false;
    }
}
