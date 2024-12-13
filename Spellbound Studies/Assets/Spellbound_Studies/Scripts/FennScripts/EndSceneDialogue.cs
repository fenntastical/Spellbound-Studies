using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneDialogue : MonoBehaviour
{
    public Dialogue lilithDialogue;
    public Dialogue NyxDialogue;
    public UpdateSceneOnTimer scenechange;
    public SpriteRenderer lilithsprite;
    [HideInInspector] public bool canStart = false;
    bool dialogueDone = false;
    bool nyxDone = false;
    bool lilithDone = false;
    public int desiredScene = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(canStart == true && lilithDialogue.inprogress == false && lilithDone == false)
        {
            lilithDialogue.StartDialogue();
            lilithDone = true;
            lilithDialogue.inprogress = true;

        }
        if(lilithDialogue.inprogress == false && nyxDone == false && canStart == true)
        {
            lilithsprite.enabled = false;
            lilithDialogue.StopAllCoroutines();
            NyxDialogue.StopAllCoroutines();
            NyxDialogue.textComponent.text = string.Empty;
            NyxDialogue.StartDialogue();
            nyxDone = true;
        }
        if(NyxDialogue.inprogress == false && nyxDone == true && dialogueDone == false && canStart == true)
        {
            dialogueDone = true;
            scenechange.secondHalf = true;
            scenechange.StartTimeline();
        }
    }
}
