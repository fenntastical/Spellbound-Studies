using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Callie : MonoBehaviour
{
   enum AttackPattern { Nothing, Right, Left, Down, Up}

    public GameObject[] spawnerList;
    public GameObject[] knifeList;
    public Animator animator;

   [SerializeField] private AttackPattern patternType;

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

    void Start()
    {
        updateSpawnersNothing();
        animator = GetComponent<Animator>();
       // health = maxHealth;
    }

    void Update()
    {
        if (talking == false & continueAttack == false)
        {
            StartCoroutine(attackWaiter());
            continueAttack = true;
        }

        if (lHealth.health <= 250 && bigAttack == false)
        {
            HealMechanic();
            bigAttack = true;
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
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

    IEnumerator HealMechanic()
    {
        while (callieAlive == true)
        {
            health += 50;
            animator.SetTrigger("Heal");
            yield return new WaitForSeconds(20);

        }
    }
    public void RandomizeAttack()
    {
        animator.SetTrigger("Attack");
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