using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTalkingScript : MonoBehaviour
{
    public Dialogue lilithDialogue;
    public Dialogue NyxDialogue;
    public PlayerMovement playerMovement;
    public playerCombat Combat;
    public LilithAttack attack;
    public UIHealthPanel uipanel;
    bool dialogueDone = false;
    bool nyxDone = false;
    bool lilithDone = false;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovement.enabled = false;
        Combat.enabled = false;
        // lilithDialogue.StartDialogue();
        // lilithDialogue.inprogress = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(lilithDialogue.inprogress == false && lilithDone == false)
        {
            lilithDialogue.StartDialogue();
            lilithDialogue.inprogress = true;
            lilithDone = true;

        }
        if(lilithDialogue.inprogress == false && lilithDone == true &&  nyxDone == false)
        {
            NyxDialogue.StartDialogue();
            nyxDone = true;
        }
        if(NyxDialogue.inprogress == false && nyxDone == true && dialogueDone == false)
        {
            playerMovement.enabled = true;
            Combat.enabled = true;
            attack.talking = false;
            dialogueDone = true;
            uipanel.updatePanel();
        }
    }
}
