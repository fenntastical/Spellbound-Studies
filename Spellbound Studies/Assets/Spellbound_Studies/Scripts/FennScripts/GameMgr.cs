using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public PanelMover gameOverUI;
    public PanelMover HUD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        gameOverUI.isVisible = true;
        HUD.isVisible = false;
        Time.timeScale = 0;
    }
}
