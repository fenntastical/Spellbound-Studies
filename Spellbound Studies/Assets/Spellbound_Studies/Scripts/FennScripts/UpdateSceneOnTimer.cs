using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class UpdateSceneOnTimer : MonoBehaviour
{
    public float changeTime;
    public float changeTime2;
    public EndSceneDialogue dialogue;
    public PlayableDirector director;
    public int desiredScene = 4;
    [HideInInspector] public bool secondHalf = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(secondHalf == false)
            changeTime -= Time.deltaTime;
        if (changeTime <= 0 && secondHalf == false)
        {
            director.playableGraph.GetRootPlayable(0).SetSpeed(0);
            dialogue.canStart = true;
        }
        if(secondHalf == true)
            changeTime2 -= Time.deltaTime;
        if( changeTime2 <= 0 && secondHalf == true)
        {
            SceneManager.LoadScene(desiredScene);

        }
    }

    public void StartTimeline()
    {
        director.time = director.time;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
    public void StopTimeline()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
}
