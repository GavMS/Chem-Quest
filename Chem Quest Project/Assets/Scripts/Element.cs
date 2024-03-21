using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{

    public int code;

    public GameObject highlight;

    public bool pickedUp;

    public bool intro = false;

    private void Start()
    {

        highlight.SetActive(false);
        Collider2D[] playerColliders = GameObject.Find("Player").GetComponents<Collider2D>();
        Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();

        foreach (Collider2D playerCollider in playerColliders)
        {
            foreach (Collider2D childCollider in childColliders)
            {
                Physics2D.IgnoreCollision(playerCollider, childCollider, true);
            }
        }

    }

    private void Update()
    {
        if (!intro)
        {
            if (pickedUp)
            {
                GetComponent<SpriteRenderer>().sortingOrder = 100;
                GetComponentInChildren<Canvas>().sortingOrder = 100;


                GetComponent<Rigidbody2D>().isKinematic = true;
                Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();

                foreach (Collider2D childCollider in childColliders)
                {
                    childCollider.enabled = false;
                }
            }
            else
            {
                GetComponentInChildren<Canvas>().sortingOrder = 0;
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                GetComponent<Rigidbody2D>().isKinematic = false;
                Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();

                foreach (Collider2D childCollider in childColliders)
                {
                    childCollider.enabled = true;
                }
            }
        }
    }

    public void Pickup()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pickedUp = true;
        transform.parent = GameObject.Find("Player").transform;
        transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 1, 0);
        highlight.SetActive(false);
    }

}