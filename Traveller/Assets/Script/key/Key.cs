using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private SpringJoint2D spring;

    public bool playerHasKey;

    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        playerHasKey = false;
        spring = GetComponent<SpringJoint2D>();
        GameObject backpack = GameObject.FindWithTag("BackPack");
        spring.connectedBody = backpack.GetComponent<Rigidbody2D>();
        spring.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !spring.enabled)
        {
            spring.enabled = true;
            playerHasKey = true;
            rend.enabled = false;

        }
    }

}