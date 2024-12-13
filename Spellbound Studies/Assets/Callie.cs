using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Callie : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    public UnityEvent<GameObject> OnHitWithReference;
    public KnockBackFeedback knockBackFeedback;
    public GameObject[] spawnerList;
    public GameObject[] knifeList;
    public Animator animator;

    private int attackTimer;
    private int currentTime = 0;
    private bool callieAlive = true;

    [HideInInspector] public bool talking = true;
    private bool continueAttack = false;

    float maxHealth = 50f;
    public Enemy lHealth;
    bool bigAttack = false;
    float health;

    [ContextMenu("testUp")]
    public void testUp()
    {
        updateSpawnersUp();
    }
    [ContextMenu("testDown")]
    public void testDown()
    {
        updateSpawnersDown();
    }
    [ContextMenu("testRight")]
    public void testRight()
    {
        updateSpawnersRight();
    }
    [ContextMenu("testLeft")]
    public void testLeft()
    {
        updateSpawnersLeft();
    }



    //blocking mechanism
    //public float blockDuration = 2f; // How long blocking lasts
    //public float blockCooldown = 5f; // Cooldown before blocking again
    //public float damageReductionFactor = 0.5f; // Reduces damage by 50%
    //private bool isBlocking = false;
    //private bool canBlock = true;
    //private float blockTimer = 0f;
    //private float cooldownTimer = 0f;

    // AI logic parameters
    //public float blockTriggerHealthThreshold = 0.3f; // Block if health < 30% of maxHealth
    //public float blockProbability = 0.5f; // 50% chance to block when triggered
    //private bool playerIsAttacking = false; // Simulate player attack detection


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        updateSpawnersNothing();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (talking == false & continueAttack == false)
        {
            StartCoroutine(attackWaiter());
            continueAttack = true;
        }

        if (target)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }

        AudioMgr.Instance.PlaySFX("Lilith Attack");


        // Handle blocking timers
        //if (isBlocking)
        //{
        //    blockTimer -= Time.deltaTime;
        //    if (blockTimer <= 0f)
        //    {
        //        isBlocking = false;
        //        cooldownTimer = blockCooldown; // Start cooldown
        //    }
        //}
        //else if (!canBlock)
        //{
        //    cooldownTimer -= Time.deltaTime;
        //    if (cooldownTimer <= 0f)
        //    {
        //        canBlock = true; // Reset block availability
        //    }
        //}
        //if (canBlock && ShouldBlock())
        //{
        //    StartBlocking();
        //}

}
    private void FixedUpdate()
    {
        //if (target && !isBlocking) // Disable movement while blocking
        //{
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        //}
        //else
        //{
            //rb.velocity = Vector2.zero; // Stop movement while blocking
        //}
    }
    //public void TakeDamage(float damage)
    //{
    //    Debug.Log($"Callie took {damage} damage!");

    //    //if (isBlocking)
    //    //{
    //    //    damage *= damageReductionFactor; // Reduce damage if blocking
    //    //}
    //    health -= damage;
    //    AudioMgr.Instance.PlaySFX("Enemy Damage");
    //    if (health <= 0)
    //    {
    //        Destroy(gameObject);
    //    }

    //}
        IEnumerator attackWaiter()
    {
        while (callieAlive == true)
        {
            // yield return new WaitForSeconds(2);
            RandomizeAttack();
            //Wait for 5 seconds    
            yield return new WaitForSeconds(5);
            updateSpawnersNothing();
            yield return new WaitForSeconds(2);
        }
    }

    //private void StartBlocking()
    //{
    //    isBlocking = true;
    //    canBlock = false;
    //    blockTimer = blockDuration;
    //}

    //private bool ShouldBlock()
    //{
    //    // Trigger based on player attacking
    //    if (playerIsAttacking && Random.value < blockProbability)
    //    {
    //        return true;
    //    }

    //    // Trigger based on health threshold
    //    if (health / maxHealth < blockTriggerHealthThreshold && Random.value < blockProbability)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    // Simulate player attack detection
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("PlayerAttack"))
    //    {
    //        playerIsAttacking = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("PlayerAttack"))
    //    {
    //        playerIsAttacking = false;
    //    }
    //}
    public void RandomizeAttack()
    {
        //animator.SetTrigger("Attack");
        int attackChosen;
        attackChosen = Random.Range(1, 4);
        // print(attackChosen);
        switch (attackChosen)
        {
            case 1:
                updateSpawnersRight();
                break;
            case 2:
                updateSpawnersLeft();
                break;
            case 3:
                updateSpawnersDown();
                break;
            case 4:
                updateSpawnersUp();
                break;
        }
    }

    public void updateSpawnersRight()
    {
        int counter = 0;
        float[] positions = {  2, 6, 8, 10 };
        foreach (GameObject spawner in knifeList)
        {
            if (counter <= 3)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Straight;
                bulletSpawn.firingRate = 0.2f;
                bulletSpawn.bullet = knifeList[0];
                spawner.transform.position = new Vector3 (12.5f,positions[counter],0);
                spawner.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else
                spawner.SetActive(false);
            counter++;

        }
    }

    public void updateSpawnersLeft()
    {
        int counter = 0;
        float[] positions = { 2, 6, 8, 10 };
        foreach (GameObject spawner in knifeList)
        {
            if (counter <= 3)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Straight;
                bulletSpawn.firingRate = 0.2f;
                bulletSpawn.bullet = knifeList[0];
                spawner.transform.position = new Vector3 (-12.5f,positions[counter],0);
                spawner.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            else
                spawner.SetActive(false);
            counter++;

        }
    }

    public void updateSpawnersDown()
    {
        int counter = 0;
        float[] positions = { 2, 4, 6, 8 };
        foreach (GameObject spawner in knifeList)
        {
            if (counter <= 3)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Straight;
                bulletSpawn.firingRate = 0.2f;
                bulletSpawn.bullet = knifeList[0];
                spawner.transform.position = new Vector3 (positions[counter],12.5f,0);
                spawner.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else
                spawner.SetActive(false);
            counter++;

        }
    }

    public void updateSpawnersUp()
    {
        int counter = 0;
        float[] positions = { -6.5f, -3, 3, 6.5f };
        foreach (GameObject spawner in knifeList)
        {
            if (counter <= 3)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Straight;
                bulletSpawn.firingRate = 0.2f;
                bulletSpawn.bullet = knifeList[0];
                spawner.transform.position = new Vector3 (positions[counter],-12.28f,0);
                spawner.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
                spawner.SetActive(false);
            counter++;

        }

    }

    public void updateSpawnersNothing()
    {
        foreach (GameObject spawner in spawnerList)
            spawner.SetActive(false);
    }
}