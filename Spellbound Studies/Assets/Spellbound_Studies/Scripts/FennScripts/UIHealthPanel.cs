using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthPanel : MonoBehaviour
{
    public GameObject heart;
    public PlayerHealth healthScript;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go;
        for(int i = 0; i <= healthScript.maxHealth; i++){
            go = Instantiate(heart, new Vector3 (0,0,0), Quaternion.identity);
            go.transform.parent = parent.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
