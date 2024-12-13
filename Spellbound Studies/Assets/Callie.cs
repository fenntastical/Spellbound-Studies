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
        Enemy healthscript = gameObject.GetComponent<Enemy>();
        Debug.Log(healthscript.health);
        //I will add this back when i add dialogue
        // if (talking == false & continueAttack == false)
        if (continueAttack == false)
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
            yield return new WaitForSeconds(4);
            updateSpawnersNothing();
            yield return new WaitForSeconds(3);
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
        attackChosen = Random.Range(1, 5);
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
                updateSpawnersUp();
                break;
            case 4:
                updateSpawnersDown();
                break;
        }
    }

    public void updateSpawnersRight()
    {
        Debug.Log("1");
        int counter = 0;
        float[] positions = { -6, 0, 6};
        foreach (GameObject spawner in spawnerList)
        {
            if (counter <= 2)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Straight;
                bulletSpawn.firingRate = 1f;
                // bulletSpawn.bullet = knifeList[1];
                // Bullet foundBullet = bulletSpawn.bullet.GetComponent<Bullet>();
                // foundBullet.transform.rotation = Quaternion.Euler(0, 0, 90);
                // bulletSpawn.bullet = knifeList[0];
                spawner.transform.position = new Vector3 (12.5f,positions[counter],0);
                spawner.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else
                spawner.SetActive(false);
            counter++;

        }
    }

    public void updateSpawnersLeft()
    {
        Debug.Log("2");
        int counter = 0;
        float[] positions = {-6, 0, 6};
        foreach (GameObject spawner in spawnerList)
        {
            if (counter <= 2)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Straight;
                bulletSpawn.firingRate = 1f;
                // bulletSpawn.bullet = knifeList[0];
                // GameObject foundBullet = bulletSpawn.bullet;
                // foundBullet.transform.eulerAngles = new Vector3(0, 0, 90);
                // bulletSpawn.bullet = knifeList[0];
                spawner.transform.position = new Vector3 (-12.5f,positions[counter],0);
                spawner.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
                spawner.SetActive(false);
            counter++;

        }
    }

    public void updateSpawnersDown()
    {
        Debug.Log("3");
        int counter = 0;
        float[] positions = { -6.5f, 0, 6.5f};
        foreach (GameObject spawner in spawnerList)
        {
            if (counter <= 2)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Straight;
                bulletSpawn.firingRate = 1f;
                // bulletSpawn.bullet = knifeList[3];
                // Bullet foundBullet = bulletSpawn.bullet.GetComponent<Bullet>();
                // foundBullet.transform.rotation = Quaternion.Euler(0, 0, 180);
                // bulletSpawn.bullet = knifeList[0];
                spawner.transform.position = new Vector3 (positions[counter],12.5f,0);
                spawner.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            else
                spawner.SetActive(false);
            counter++;

        }
    }

    public void updateSpawnersUp()
    {
        Debug.Log("4");
        int counter = 0;
        float[] positions = { -6.5f, 0, 6.5f};
        foreach (GameObject spawner in spawnerList)
        {
            if (counter <= 2)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Straight;
                bulletSpawn.firingRate = 1f;
                // bulletSpawn.bullet = knifeList[4];
                // Bullet foundBullet = bulletSpawn.bullet.GetComponent<Bullet>();
                // foundBullet.transform.rotation = Quaternion.Euler(0, 0, 0);
                // bulletSpawn.bullet = knifeList[0];
                spawner.transform.position = new Vector3 (positions[counter],-12.28f,0);
                spawner.transform.rotation = Quaternion.Euler(0, 0, 90);
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