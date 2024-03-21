using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public bool main = false;

    public float delay = 0;

    [SerializeField] float moveSpeed;

    public GameObject myClone;

    private void Start()
    {
        if (main)
        {
            StartCoroutine(CreateClone());
        }
    }

    IEnumerator CreateClone()
    {
        while (true)
        {
            GameObject myClone2 = Instantiate(myClone, transform.position,Quaternion.identity);
            myClone2.GetComponent<BackgroundParallax>().moveSpeed = moveSpeed;
            yield return new WaitForSeconds(delay);
            yield return null;
        }
    }


    private void Update()
    {
        if (!main)
        {
            // Move the main object and clone to the left
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

}
