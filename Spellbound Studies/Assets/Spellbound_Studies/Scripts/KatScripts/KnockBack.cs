using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Rigidbody rb2d;
    [SerializeField]
    private float strength= 16, delay= 0.15f;
    public UnityEvent OnBegin, OnDone;

    public void PlayFeedBack(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position-sender.transform.position).normalized;
        rb2d.AddForce(direction * strength, ForceMode.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
