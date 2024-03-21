using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    public Transform RobotGraphic;
    public LayerMask ElementLayer;
    public float rayDistance;
    public float maxXwalk;
    public float velocity2D = 2f;

    public float ThrowForce;

    private bool isFacingRight = true;

    public float robotDelay;

    private Rigidbody2D rb;
    public GameObject portalEffect;

    public GameObject[] ObjectsToActivate;

    private void Start()
    {
        if (ObjectsToActivate.Length != 0)
        {
            StartCoroutine(ActivateObjectsCoroutine());
        }
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RobotLoop());
    }

    private IEnumerator ActivateObjectsCoroutine()
    {
        yield return new WaitForSeconds(robotDelay);
        foreach (GameObject obj in ObjectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    public void Portal()
    {
        StopAllCoroutines();
        rb.velocity = Vector2.zero;
        portalEffect.SetActive(true);
    }


    private IEnumerator RobotLoop()
    {
        yield return new WaitForSeconds(robotDelay);
        bool pickedUp = false;
        while (true)
        {
            if (!pickedUp)
            {
                // Robot moves left
                rb.velocity = new Vector2(-velocity2D, 0);
                RobotGraphic.localScale = new Vector3(1, RobotGraphic.localScale.y, RobotGraphic.localScale.z);
            }

            // Shoot a raycast in front
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, ElementLayer);
            Debug.DrawLine(transform.position, transform.position + Vector3.left * rayDistance, Color.red);

            if (hit.collider != null)
            {
                hit.collider.GetComponent<Element>().highlight.SetActive(true);
                rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1f);
                hit.collider.GetComponent<Element>().highlight.SetActive(false);
                hit.collider.GetComponent<Rigidbody2D>().isKinematic = true;
                hit.transform.parent = transform;
                hit.transform.position = transform.position + new Vector3(0, 1, 0);
                pickedUp = true;
            }

            if (pickedUp)
            {
                // Robot moves right
                while (transform.position.x < maxXwalk)
                {
                    rb.velocity = new Vector2(velocity2D, 0);
                    RobotGraphic.localScale = new Vector3(-1, RobotGraphic.localScale.y, RobotGraphic.localScale.z);

                    yield return null;
                }
                rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1f);

                if (hit.collider != null)
                {
                    hit.collider.GetComponent<Rigidbody2D>().isKinematic = false;
                    hit.collider.transform.SetParent(null);
                    hit.collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * ThrowForce, ForceMode2D.Impulse);
                    yield return new WaitForSeconds(1f);
                    pickedUp = false;
                }
            }
            yield return null;
        }
    }
}
