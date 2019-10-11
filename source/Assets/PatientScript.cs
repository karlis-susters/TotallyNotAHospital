using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientScript : MonoBehaviour
{
    public string itemNeeded;
    public Animator animator;
    public GameObject gameC = null;
    public AudioManager am;
    public GameController gc = null;
    // Start is called before the first frame update
    public bool satisfied = false;
    public bool childrenActive = false;
    // Update is called once per frame
    public float timeLeft;
    private float dyingTime;
    bool dying = false;
    public int idx;
    private void Start()
    {
        am = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();
        gameC = GameObject.Find("GameController");
        gc = gameC.GetComponent<GameController>();
        timeLeft = gc.patience;
        dyingTime = timeLeft / 2f;
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < (dyingTime) && !dying)
        {
            animator.SetBool("Dying", true);
            dying = true;
        }
        if (timeLeft < 0 && !satisfied)
        {
            am.Play("AngryCustomer");
            satisfied = true;
        }
        if (!satisfied && gameObject.transform.position.y < 0.5f)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            Vector3 pos = gameObject.transform.position;
            pos.y += 0.05f;
            gameObject.transform.position = pos;
        }
        else if (!childrenActive)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            childrenActive = true;
        }
        if (satisfied)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            Vector3 pos = gameObject.transform.position;
            pos.y -= 0.05f;
            gameObject.transform.position = pos;
            if (pos.y < -3.7f)
            {
                gc.activePeople--;
                gc.taken[idx] = false;
                Destroy(gameObject);
            }
        }
        
        
    }
}
