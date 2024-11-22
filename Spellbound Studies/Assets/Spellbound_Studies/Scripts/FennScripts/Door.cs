using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool locked;
    public Room room;
    public GameMgrLvl lvl;
    public bool interactable;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerMgr.inst.interactables.Add(this);
        room = gameObject.GetComponentInParent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        lvl.currentRoom++;
        if(lvl.currentRoom < lvl.numRooms)
        {
            lvl.loadNext = true;
        }
        else
        {
            lvl.endReached = true;
        }
    }


   void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            if(locked == false)
                Interact();
        }
    }

    void OnCollisionxit2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            interactable = false;
        }
    }
}