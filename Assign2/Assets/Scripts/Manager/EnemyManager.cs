using UnityEngine;
using UnityEngine.UI;
public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject chicken;
    public GameObject sheep;
    public GameObject pig;// The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public int level;
    public int currentEC;
    public int startingCount;
    public GameObject[] curEnemyCount;
    [SerializeField] private Text levelText;
    [SerializeField] private Text enemyText;
    void Start()
    {
        level = 1;
        startingCount =3;
        spawnEnemies(startingCount);  
    }

    void Update()
    {
        levelText.text = "Level: " + level;
        curEnemyCount = GameObject.FindGameObjectsWithTag("Enemy");
        currentEC = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyText.text = "Enemies Left: " + currentEC;
        if (currentEC == 0)
        {
            level++;
            spawnEnemies( (startingCount * level) * 2);
        }    
    }
    void spawnEnemies(int enemies)
    {

        for (int i = 0; i < enemies; i++)
        {
            Spawn();
        }


    }

    void Spawn()
    {

        // If the player has no health left...
        if (playerHealth.currentHealth <= 0f)
        {
            // ... exit the function.
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        const int probabilityChicken = 0;
        const int probabilitySheep = 1;
        int randomChance = Random.Range(0, 4);
        if (randomChance == probabilityChicken)
        {
            Instantiate(chicken, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        else if (randomChance == probabilitySheep)
        {
            Instantiate(sheep, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        else 
        {
            Instantiate(pig, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        
    }
}

