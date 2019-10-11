using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{

    public GameObject gameC = null;
    public GameController gc = null;
    public GameObject dragC = null;
    private DragController dc = null;
    private bool isHeld = false;
    public GameObject trashC = null;
    public TrashController tc = null;
    public AudioManager am;
    void Start()
    {
        dragC = GameObject.Find("DragController");
        dc = dragC.GetComponent<DragController>();
        trashC = GameObject.Find("TrashCan");
        tc = trashC.GetComponent<TrashController>();
        gameC = GameObject.Find("GameController");
        gc = gameC.GetComponent<GameController>();
        am = FindObjectOfType<AudioManager>();
    }
    public void OnMouseDown()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mp2 = mp;
            mp2.z += 1;
            RaycastHit2D[] hits = Physics2D.RaycastAll(mp,
                mp2-mp);

            bool thisHit = false;
            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.tag == "Item")
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        thisHit = true;
                    }
                    break;
                }
            }
            if (!thisHit)
            {
                return;
            }
            dragC = GameObject.Find("DragController");
            dc = dragC.GetComponent<DragController>();
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            isHeld = true;
            dc.objDragged = gameObject;

            foreach(RaycastHit2D hit in hits)
            {
                GameObject gb = hit.collider.gameObject;
                if (hit.collider.gameObject.name == "CraftingSlot1")
                {
                    if (gb.GetComponent<CraftingSlot>().inSlot == gameObject)
                    {
                        gb.GetComponent<CraftingSlot>().inSlot = null;
                    }
                }
                else if (hit.collider.gameObject.name == "CraftingSlot2")
                {
                    if (gb.GetComponent<CraftingSlot>().inSlot == gameObject)
                    {
                        gb.GetComponent<CraftingSlot>().inSlot = null;
                    }
                }

            }

        }
        if (isHeld && Input.GetMouseButtonUp(0))
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mp2 = mp;
            mp2.z += 1;
            RaycastHit2D[] hits = Physics2D.RaycastAll(mp,
                mp2-mp);

            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.name == "TrashCan")
                {
                    dc.objDragged = null;
                    isHeld = false;
                    Debug.Log("Trashcan hit");
                    Destroy(gameObject);
                }
                else if (hit.collider.gameObject.name == "CraftingSlot1")
                {
                    GameObject gb = hit.collider.gameObject;
                    if (gb.GetComponent<CraftingSlot>().inSlot == null)
                    {
                        am.Play("Blob");
                        Vector3 pos = gb.transform.position;
                        pos.x += 0.08f;
                        pos.y -= 0.19f;
                        transform.position = pos;
                        gb.GetComponent<CraftingSlot>().inSlot = gameObject;
                    }
                }
                else if (hit.collider.gameObject.name == "CraftingSlot2")
                {
                    GameObject gb = hit.collider.gameObject;
                    if (gb.GetComponent<CraftingSlot>().inSlot == null)
                    {
                        am.Play("Blob");
                        Vector3 pos = gb.transform.position;
                        pos.x += 0.08f;
                        pos.y -= 0.19f;
                        transform.position = pos;
                        gb.GetComponent<CraftingSlot>().inSlot = gameObject;
                    }
                }
                else if (hit.collider.gameObject.name.StartsWith("Face"))
                {
                    GameObject gb = hit.collider.gameObject;
                    if (!gb.GetComponent<PatientScript>().childrenActive)
                    {

                    }
                    else if (gameObject.name.StartsWith(gb.GetComponent<PatientScript>().itemNeeded))
                    {
                        //this is the item needed
                        gc.AddScore();
                        //Destroy(gb);
                        am.Play("SatisfiedCustomer");
                        gb.GetComponent<PatientScript>().satisfied = true;
                        Destroy(gameObject);
                    }
                    else
                    {
                        //Patient not satisfied
                        am.Play("AngryCustomer");
                        gb.GetComponent<PatientScript>().satisfied = true;
                        Destroy(gameObject);
                    }
                }
            }
            dc.objDragged = null;
            isHeld = false;
        }
        if (isHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;
            transform.position = mousePos;
        }
        
    }
}
