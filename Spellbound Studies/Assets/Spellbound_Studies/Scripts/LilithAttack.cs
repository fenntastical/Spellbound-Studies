using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilithAttack : MonoBehaviour
{
    enum AttackPattern { Nothing, Cone, Tornado, Circle }
    public float health = 3f;
    public BulletSpawner spawnerList[];

    [SerializeField] private AttackPattern patternType;
    private int attackTimer;
    private int currentTime = 0;

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Weapon"){
            TakeDamage(1);
        }
    }

    public void RandomizeAttack()
    {
        int attackChosen;
        attackChosen = Random.Range(1, 3);
        switch(attackChosen)
        {
            case 1:
                patternType = AttackPattern.Cone;
            break;
            case 2:
                patternType = AttackPattern.Tornado;
            break;
            case 3:
                patternType = AttackPattern.Circle;
            break;
        }
    }

    public void updateSpawnersCone()
    {
        int counter = 0;
        float rotations[] = [10, 20, 30, 40];
        foreach(spawner in spawnerList){
            if(counter <= 4)
            {
                spawner.SetActive(true);
                spawner.spawnerType = SpawnerType.Straight;
            }
            else
                spawner.SetActive(false);
            counter++;
        }
    }
}
