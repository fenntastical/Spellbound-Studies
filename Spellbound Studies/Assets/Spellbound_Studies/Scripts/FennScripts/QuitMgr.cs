using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMgr : MonoBehaviour
{
    public PanelMover quitScreen;
    // Start is called before the first frame update
    void Start()
    {
        quitScreen.isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitScreen.isVisible = true;
            Time.timeScale = 0;
        }
    }
}
