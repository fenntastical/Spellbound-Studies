using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


public class EnterOccultRoomDialogue : MonoBehaviour
{
    public Dialogue NyxDialogue;
    public float changeTime;
    // public UpdateSceneOnTimer scenechange;
    [HideInInspector] public bool canStart = false;
    public PlayableDirector director;
    bool dialogueDone = false;
    bool nyxDone = false;
    public int desiredScene = 0;
    // Start is called before the first frame update
    void Start()
    {
        nyxDone = false;
        NyxDialogue.inprogress = false;
        canStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            director.playableGraph.GetRootPlayable(0).SetSpeed(0);
            canStart = true;
        }
        if(canStart == true && NyxDialogue.inprogress == false && nyxDone == false)
        {
            print("made it here");
            NyxDialogue.StopAllCoroutines();
            NyxDialogue.StartDialogue();
            nyxDone = true;
            NyxDialogue.inprogress = true;
            // NyxDialogue.StopAllCoroutines();

        }
        if(NyxDialogue.inprogress == false && nyxDone == true && canStart == true )
        {
            // scenechange.secondHalf = true;
            SceneManager.LoadScene(desiredScene);
        }
    }
}
