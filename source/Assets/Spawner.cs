using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        GameObject created = Instantiate(item, gameObject.transform.position, Quaternion.identity);
        created.GetComponent<drag>().OnMouseDown();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
