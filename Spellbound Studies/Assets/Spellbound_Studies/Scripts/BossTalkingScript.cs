using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTalkingScript : MonoBehaviour
{
    public Dialogue lilithDialogue;
    public Dialogue NyxDialogue;

    public LilithAttack attack;
    public UIHealthPanel uipanel;
    bool dialogueDone = false;
    bool nyxDone = false;
    bool lilithDone = false;
    // Start is called before the first frame update
    void Awake()
    {
        lilithDialogue.StartDialogue();
        lilithDialogue.inprogress = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(lilithDialogue.inprogress == false & nyxDone == false)
        {
            NyxDialogue.StartDialogue();
            nyxDone = true;
            lilithDone = true;
        }
        if(NyxDialogue.inprogress == false & lilithDone == true & dialogueDone == false)
        {
            attack.talking = false;
            dialogueDone = true;
            uipanel.updatePanel();
        }
    }
}
