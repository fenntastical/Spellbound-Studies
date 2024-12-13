using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KnockBackFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float strength = 16, delay= 0.15f;

    public UnityEvent OnBegin, OnDone;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    public void PlayFeedback()
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position-player.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
