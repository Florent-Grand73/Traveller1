using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject connectedDoor;
    public bool teleported = false;
    private GameObject player;
    public GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (teleported && Input.GetAxisRaw("Vertical") < 1)
        {
            teleported = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetAxisRaw("Vertical") == 1)
        {
            if (Input.GetAxisRaw("Vertical") == 1 && !teleported)
            {
                if (key.GetComponent<Key>().playerHasKey)
                {
                    player.transform.position = connectedDoor.transform.position;
                    connectedDoor.GetComponent<Door>().teleported = true;
                }
                
            }
        }
    }
}