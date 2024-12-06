using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Interactions;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemySpawnPoints;
    public Transform playerSpawnPoint;
    public List<Vector3> enemySpawnPositions;
    public bool inRoom;
    public int minEnemies;
    public int maxEnemies;
    public int numWaves;
    public int currentWave;
    public List<GameObject> enemyPrefabs;
    public List<Door> doors;
    public PanelMover unlockUI;
    public List<GameObject> enemies;
    private bool uiDisplay = false;
    public List<GameObject> patrollers;
    public GameObject powerupSpeed;

    // public List<Interactable> interactables;

    void Start()
    {
        enemySpawnPositions = new List<Vector3>();
        foreach(Transform transform in enemySpawnPoints.GetComponentsInChildren<Transform>())
        {
            enemySpawnPositions.Add(transform.position);
        }
        enemySpawnPositions.Remove(transform.position);
        foreach (GameObject patroller in patrollers)
        {
            patroller.SetActive(false);
        }
        powerupSpeed.SetActive(false);
    }

    public void EnterRoom(Transform player, int minEnemies, int maxEnemies, int numWaves)
    {
        unlockUI.isVisible = false;
        uiDisplay = false;
        inRoom = true;
        // player.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        player.position = playerSpawnPoint.position;
        // player.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        this.minEnemies = minEnemies;
        this.maxEnemies = maxEnemies;
        this.numWaves = numWaves;
        currentWave = 0;
        SetDoors(true);
        int randomPatroller = Random.Range(0, 3);
        switch(randomPatroller)
        {
            case 0:
                patrollers[0].SetActive(true);
                patrollers[1].SetActive(false);
                patrollers[2].SetActive(false);
            break;
            case 1:
                patrollers[0].SetActive(false);
                patrollers[1].SetActive(true);
                patrollers[2].SetActive(false);
            break;
            case 2:
                patrollers[0].SetActive(false);
                patrollers[1].SetActive(false);
                patrollers[2].SetActive(true);
            break;
        }

        int isPowerUp = Random.Range(0, 2);
        if (isPowerUp == 0)
        {
            powerupSpeed.SetActive(true);
        }
    }

    public void ExitRoom(Transform player, Transform camera, int minEnemies, int maxEnemies)
    {
        inRoom = false;
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

    public void SetDoors(bool locked)
    {
        foreach (Door door in doors)
        {
            
            if(locked == true){
                SpriteRenderer doorRender = door.GetComponent<SpriteRenderer>();
                doorRender.enabled = false;
            }
            if(locked == false){
                SpriteRenderer doorRender = door.GetComponent<SpriteRenderer>();
                doorRender.enabled = true;
                unlockUI.isVisible = true;
            }
            door.locked = locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRoom)
        {
            if (currentWave < numWaves && enemies.Count == 0)
            {
                currentWave++;
                SpawnWave();
            }

            for (int i = enemies.Count - 1; i > -1; i--)
            {
                if (enemies[i] == null)
                    enemies.RemoveAt(i);
            }

            if (currentWave == numWaves && enemies.Count == 0 && uiDisplay == false)
            {
                SetDoors(false);
                uiDisplay = true;
                foreach (GameObject patroller in patrollers)
                {
                    patroller.SetActive(false);
                }
            }
        }
    }
}