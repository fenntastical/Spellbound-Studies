using UnityEngine;

public class PotScript : MonoBehaviour
{
    public GameObject brokenPot; 

    public void BreakPot()
    {
        Instantiate(brokenPot, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
