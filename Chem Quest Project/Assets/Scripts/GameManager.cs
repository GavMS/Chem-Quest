using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Color lockedColor;

    [Header("Level 1")]
    public Element[] elementsGroup1;
    public Transform group1MinSpawn;
    public Transform group1MaxSpawn;

    public Box[] boxesGroup1;

    public GameObject Door1;

    [Space]

    [Header("Level 2")]
    public Element[] elementsGroup2;
    public Transform group2MinSpawn;
    public Transform group2MaxSpawn;

    public Box[] boxesGroup2;

    public GameObject Door2;

    [Space]

    [Header("Level 3")]
    public Element[] elementsGroup3;
    public Transform group3MinSpawn;
    public Transform group3MaxSpawn;

    public Box[] boxesGroup3;

    public GameObject Door3;

    [Space]

    [Header("Level 4")]
    public Element[] elementsGroup4;
    public Transform group4MinSpawn;
    public Transform group4MaxSpawn;

    public Box[] boxesGroup4;

    public GameObject Door4;

    [Space]

    [Header("Level 5")]
    public Element[] elementsGroup5;
    public Transform group5MinSpawn;
    public Transform group5MaxSpawn;

    public Box[] boxesGroup5;

    public GameObject Door5;


    void Start()
    {
        ScatterElements(elementsGroup1, group1MinSpawn, group1MaxSpawn);
        ScatterElements(elementsGroup2, group2MinSpawn, group2MaxSpawn);
        ScatterElements(elementsGroup3, group3MinSpawn, group3MaxSpawn);
        ScatterElements(elementsGroup4, group4MinSpawn, group4MaxSpawn);
        ScatterElements(elementsGroup5, group5MinSpawn, group5MaxSpawn);

    }
    public void ScatterElements(Element[] elements, Transform minSpawn, Transform maxSpawn)
    {
        foreach (Element element in elements)
        {
            float xPosition = Random.Range(minSpawn.position.x, maxSpawn.position.x);
            float yPosition = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            element.transform.position = new Vector2(xPosition, yPosition);
        }
    }

    public IEnumerator ThrowElement(Transform element)
    {
        GameObject.Find("Game Manager").GetComponent<AudioScript>().PlayAudio(2);
        element.GetComponent<Rigidbody2D>().isKinematic = false;
        element.GetComponent<Collider2D>().enabled = true;

        element.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1,1),2f),ForceMode2D.Impulse);

        yield return new WaitForSeconds(1);

        element.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        element.GetComponent<Rigidbody2D>().isKinematic = true;
        element.GetComponent<Collider2D>().enabled = false;


        yield return null;
    }

    private bool door1Opened = false;
    private bool door2Opened = false;
    private bool door3Opened = false;
    private bool door4Opened = false;
    private bool door5Opened = false;

    void Update()
    {
        if (AllBoxesFilled(boxesGroup1))
        {
            if (!door1Opened && CheckCorrectBoxes(elementsGroup1, boxesGroup1, group1MinSpawn, group1MaxSpawn))
            {
                OpenDoor(Door1);
                door1Opened = true;
            }
        }

        if (AllBoxesFilled(boxesGroup2))
        {
            if (!door2Opened && CheckCorrectBoxes(elementsGroup2, boxesGroup2, group2MinSpawn, group2MaxSpawn))
            {
                OpenDoor(Door2);
                door2Opened = true;
            }
        }

        if (AllBoxesFilled(boxesGroup3))
        {
            if (!door3Opened && CheckCorrectBoxes(elementsGroup3, boxesGroup3, group3MinSpawn, group3MaxSpawn))
            {
                OpenDoor(Door3);
                door3Opened = true;
            }
        }

        if (AllBoxesFilled(boxesGroup4))
        {
            if (!door4Opened && CheckCorrectBoxes(elementsGroup4, boxesGroup4, group4MinSpawn, group4MaxSpawn))
            {
                OpenDoor(Door4);
                door4Opened = true;
            }
        }

        if (AllBoxesFilled(boxesGroup5))
        {
            if (!door5Opened && CheckCorrectBoxes(elementsGroup5, boxesGroup5, group5MinSpawn, group5MaxSpawn))
            {
                OpenDoor(Door5);
                door5Opened = true;
            }
        }
    }

    bool CheckCorrectBoxes(Element[] elementsGroup, Box[] boxesGroup, Transform groupMinSpawn, Transform groupMaxSpawn)
    {
        bool AllCorrectElement = true;
        foreach (Box box in boxesGroup)
        {
            if (box.placedElement.GetComponent<Element>().code != box.code)
            {
                AllCorrectElement = false;
                box.placedElement.transform.SetParent(null);
                StartCoroutine(ThrowElement(box.placedElement.transform));
                box.placedElement = null;
            }
            else
            {
                box.highlightCorrect.SetActive(true);
                box.placedElement.GetComponent<SpriteRenderer>().color = lockedColor;
                box.placedElement.layer = LayerMask.NameToLayer("Default");
            }
                
        }
        return AllCorrectElement;
    }

    void OpenDoor(GameObject go) 
    {
        GameObject.Find("Game Manager").GetComponent<AudioScript>().PlayAudio(3);
        go.GetComponent<SpriteRenderer>().enabled = false;
        go.GetComponent<Collider2D>().enabled = false;
    }

    bool AllBoxesFilled(Box[] boxesGroup)
    {
        foreach (Box box in boxesGroup)
        {
            if (box.placedElement == null)
            {
                return false;
            }
        }
        return true;
    }

}