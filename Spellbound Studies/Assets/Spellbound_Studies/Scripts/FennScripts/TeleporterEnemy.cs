using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterEnemy : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private GameObject player;
    public List<GameObject> teleLoations;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(player != null)
            playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator attackWaiter()
    {

        yield return new WaitForSeconds(2);

    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            playerHealth.TakeDamage(damage);
            int randomLocation = Random.Range(0, 4); 
            gameObject.transform.position = teleLoations[randomLocation].transform.position;
            attackWaiter();
        }
    }
}
