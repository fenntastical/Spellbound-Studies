using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class LilithAttack : MonoBehaviour
{
    enum AttackPattern { Nothing, Cone, Tornado, Circle }
    public GameObject enemySpawnPoints;
    public List<Vector3> enemySpawnPositions;
    public int minEnemies;
    public int maxEnemies;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> enemies;
    public GameObject[] spawnerList;
    public GameObject[] bulletList;
    public Animator animator;

    [SerializeField] private AttackPattern patternType;
    private int attackTimer;
    private int currentTime = 0;
    private bool lilithAlive = true;

    [HideInInspector] public bool talking = true;
    private bool continueAttack = false;

    float maxHealth = 50f;
    public Enemy lHealth;
    bool bigAttack = false;
    float health;

    // Start is called before the first frame update
    void Start()
    {
        updateSpawnersNothing();
        animator = GetComponent<Animator>();
        enemySpawnPositions = new List<Vector3>();
        foreach(Transform transform in enemySpawnPoints.GetComponentsInChildren<Transform>())
        {
            enemySpawnPositions.Add(transform.position);
        }
        enemySpawnPositions.Remove(transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if(talking == false & continueAttack == false)
        {
            StartCoroutine(attackWaiter());
            continueAttack = true;
        }

        if(lHealth.health <= 250 && bigAttack == false)
        {
            SpawnWave();
            bigAttack = true;
        }
        AudioMgr.Instance.PlaySFX("Lilith Attack");
        for (int i = enemies.Count - 1; i > -1; i--)
            {
                if (enemies[i] == null)
                    enemies.RemoveAt(i);
            }

        // if(continueAttack == true)
        // {
        //     while(lilithAlive == true)
        //     {
        //         attackWaiter();
        //     }
        // }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // public void OnCollisionEnter2D(Collision2D collider)
    // {
    //     if(collider.gameObject.tag == "Weapon"){
    //         TakeDamage(1);
    //     }
    // }
    IEnumerator attackWaiter()
    {
        while(lilithAlive == true)
        {
            // yield return new WaitForSeconds(2);
            RandomizeAttack();
            //Wait for 5 seconds    
            yield return new WaitForSeconds(5);
            updateSpawnersNothing();
            yield return new WaitForSeconds(2);
        }
    }
    
    public void SpawnWave()
    {
        int upperBound = Mathf.Min(maxEnemies, enemySpawnPositions.Count);
        int numToSpawn = Random.Range(minEnemies, upperBound);
        List<Vector3> spawnsToUse = new List<Vector3>(enemySpawnPositions);
        for (int i = 0; i < numToSpawn; i++)
        {
            int spawnPosIndex = Random.Range(0, spawnsToUse.Count);
            int enemyPrefabIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject newEnemy = Instantiate(enemyPrefabs[enemyPrefabIndex], spawnsToUse[spawnPosIndex], Quaternion.identity);
            enemies.Add(newEnemy);
            spawnsToUse.RemoveAt(spawnPosIndex);
        }
    }

    public void RandomizeAttack()
    {
        animator.SetTrigger("Attack");
        int attackChosen;
        attackChosen = Random.Range(1, 4);
        // print(attackChosen);
        switch(attackChosen)
        {
            case 1:
                updateSpawnersCone();
            break;
            case 2:
                updateSpawnersTornado();
            break;
            case 3:
                updateSpawnersCircle();
            break;
        }
    }

    public void updateSpawnersCone()
    {
        int counter = 0;
        float[] rotations = {10, 20, 30, 40};
        foreach(GameObject spawner in spawnerList){
            if(counter <= 3)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Spin;
                bulletSpawn.bullet = bulletList[1];
                bulletSpawn.firingRate = 0.2f;
                spawner.transform.rotation = Quaternion.Euler(0,0,rotations[counter]);
            }
            else
                spawner.SetActive(false);
            counter++;
        }
    }

    public void updateSpawnersTornado()
    {
        int counter = 0;
        float[] rotations = {0, 90, 180, 270};
        foreach(GameObject spawner in spawnerList){
            if(counter <= 3)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Spin;
                bulletSpawn.firingRate = 0.2f;
                bulletSpawn.bullet = bulletList[0]; 
                spawner.transform.rotation = Quaternion.Euler(0,0,rotations[counter]);
            }
            else
                spawner.SetActive(false);
            counter++;
        }
    }

    public void updateSpawnersCircle()
    {
        int counter = 0;
        float[] rotations = {0, 45, 90, 135, 180, 225, 270, 315};
        foreach(GameObject spawner in spawnerList){
            if(counter <= 7)
            {
                spawner.SetActive(true);
                BulletSpawner bulletSpawn = spawner.GetComponent<BulletSpawner>();
                bulletSpawn.spawnerType = BulletSpawner.SpawnerType.Straight;
                bulletSpawn.firingRate = 0.6f; 
                bulletSpawn.bullet = bulletList[2]; 
                spawner.transform.rotation = Quaternion.Euler(0,0,rotations[counter]);
            }
            else
                spawner.SetActive(false);
            counter++;
        }
    }

    public void updateSpawnersNothing()
    {
        foreach(GameObject spawner in spawnerList)
            spawner.SetActive(false);
    }
}
