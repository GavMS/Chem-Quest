using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    public float OutlineWidth;
    public int OrderInLayer;
    public Material OutlineMaterial;

    GameObject outline0;
    GameObject outline1;
    GameObject outline2;
    GameObject outline3;

    GameObject outline4;
    GameObject outline5;
    GameObject outline6;
    GameObject outline7;

    void Start()
    {
        outline0 = new GameObject();
        CreateOutline(outline0);

        outline1 = new GameObject();
        CreateOutline(outline1);

        outline2 = new GameObject();
        CreateOutline(outline2);

        outline3 = new GameObject();
        CreateOutline(outline3);

        outline4 = new GameObject();
        CreateOutline(outline4);

        outline5 = new GameObject();
        CreateOutline(outline5);

        outline6 = new GameObject();
        CreateOutline(outline6);

        outline7 = new GameObject();
        CreateOutline(outline7);
    }
    void CreateOutline(GameObject outlineObject)
    {
        var outlineRenderer = outlineObject.AddComponent<SpriteRenderer>();
        outlineObject.name = "Outline";
        outlineObject.transform.SetParent(transform);
        outlineObject.transform.localScale = Vector3.one;
        outlineObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        outlineObject.tag = "Outline";
        outlineRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        outlineRenderer.material = OutlineMaterial;
        outlineRenderer.sortingOrder = OrderInLayer;
        outlineObject.transform.localPosition = Vector3.zero;
    }
    void Update()
    {
        outline0.transform.localPosition = new Vector3(OutlineWidth, 0, 0);
        outline1.transform.localPosition = new Vector3(-OutlineWidth, 0, 0);
        outline2.transform.localPosition = new Vector3(0, OutlineWidth,  0);
        outline3.transform.localPosition = new Vector3(0,-OutlineWidth, 0);

        outline4.transform.localPosition = new Vector3(OutlineWidth, OutlineWidth, 0);
        outline5.transform.localPosition = new Vector3(-OutlineWidth, OutlineWidth, 0);
        outline6.transform.localPosition = new Vector3(OutlineWidth, -OutlineWidth, 0);
        outline7.transform.localPosition = new Vector3(-OutlineWidth, -OutlineWidth, 0);
    }
}
