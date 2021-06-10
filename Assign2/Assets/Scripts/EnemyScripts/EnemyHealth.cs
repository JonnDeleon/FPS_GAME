using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public AudioClip deathClip;

    public GameObject HealthPack;
    public GameObject ArmorPack;
    public GameObject Immunity;
    public GameObject Nuke;
    // public int deathCount = 0;

    AudioSource enemyAudio;
    Animator anim;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyAudio = GetComponent<AudioSource>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount)
    {
        
        if (isDead)
            return;

        currentHealth -= amount;

        //hitParticles.transform.position = hitPoint;
       // hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        isSinking = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        Destroy(gameObject,.5f);
        DropItem();
        



    }

    void DropItem()
    {
        const int probabilityHealth = 10;
        const int probabilityArmor = 5;
        const int probabilityImmunity = 3;
        const int probabilityNuke = 2;
        int randomChance = Random.Range(0, 100);
        if (randomChance <= probabilityNuke)
        {
            Object.Instantiate(HealthPack, transform.position, Quaternion.identity);
        }
        else if(randomChance < probabilityImmunity)
        {
            Object.Instantiate(Immunity, transform.position, Quaternion.identity);
        }
        else if(randomChance < probabilityArmor)
        {
            Object.Instantiate(ArmorPack, transform.position, Quaternion.identity);
        }
        else if(randomChance < probabilityHealth)
        {
            Object.Instantiate(HealthPack, transform.position, Quaternion.identity);
        }
        Debug.Log(randomChance);
        
    }
}
