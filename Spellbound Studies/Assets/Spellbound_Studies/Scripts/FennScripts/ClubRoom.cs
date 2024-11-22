using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubRoom : MonoBehaviour
{

    public Dialogue nyxDialogue;
    public bool interactable = false;
    public GameObject ePrompt;
    public PanelMover confirmPanel;
    public bool locked = true;

    // Start is called before the first frame update
    void Start()
    {
        ePrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(nyxDialogue.inprogress == false && interactable == true)
        {
            confirmPanel.isVisible = true;
            interactable = false;
        }

        if(Input.GetKeyDown(KeyCode.E) && locked == false && interactable == false )
                Interact();
    }

    public void Interact()
    {
        ePrompt.SetActive(false);
        nyxDialogue.StartDialogue();
        interactable = true;
    }


   void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            ePrompt.SetActive(true);
            locked = false;
            // if(Input.GetKeyDown(KeyCode.Mouse0))
            //     Interact();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            ePrompt.SetActive(false);
            locked = true;
        }
    }

    public void setNonInteractive()
    {
        interactable = false;
    }
}
