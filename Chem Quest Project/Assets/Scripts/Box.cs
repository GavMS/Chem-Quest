using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int code;

    public GameObject highlight;
    public GameObject highlightCorrect;

    public GameObject placedElement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (placedElement == null && collision.GetComponent<PlayerController>().heldElement)
            {
                highlight.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (placedElement != null)
        {
            if (placedElement.transform.parent != transform)
            {
                placedElement = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            highlight.SetActive(false);
        }
    }

    public void PlaceElement(Element element)
    {
        if (placedElement) return;
        GameObject.Find("Player").GetComponent<PlayerController>().heldElement = null;
        placedElement = element.gameObject;
        element.transform.parent = transform;
        element.transform.localPosition = new Vector3(0, 0, 0);
        element.pickedUp = false;
    }
}