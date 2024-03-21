using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject NaCLParticle;

    public Transform[] points; // Declare the transform[] points variable

    private int currentPointIndex = 0;
    private bool movingForward = true;

    public float moveSpeed = 5f; // Adjust the move speed as desired

    // Start is called before the first frame update
    void Start()
    {
        if (points.Length != 0)
        {

            foreach (Transform point in points)
            {
                point.parent = null;
            }

            StartCoroutine(GlideBackAndForth());
        }
    }

    IEnumerator GlideBackAndForth()
    {
        while (true)
        {
            if (movingForward)
            {
                if (currentPointIndex >= points.Length)
                {
                    currentPointIndex = 0;
                }

                transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex].position, moveSpeed * Time.deltaTime);

                if (transform.position == points[currentPointIndex].position)
                {
                    currentPointIndex++;
                    movingForward = false;
                    yield return new WaitForSeconds(1f);
                }
            }
            else
            {
                if (currentPointIndex >= points.Length)
                {
                    currentPointIndex = 0;
                }

                transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex].position, moveSpeed * Time.deltaTime);

                if (transform.position == points[currentPointIndex].position)
                {
                    currentPointIndex++;
                    movingForward = true;
                    yield return new WaitForSeconds(1f);
                }
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("ReactionPlayer");
        }
    }
    public void Reaction()
    {
        Instantiate(NaCLParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
