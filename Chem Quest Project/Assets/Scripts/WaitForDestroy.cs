using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForDestroy : MonoBehaviour
{
    public float delay;
    public bool OnlyDeactive;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForDestroyCor());
    }
    IEnumerator WaitForDestroyCor()
    {
        yield return new WaitForSeconds(delay);
        if (OnlyDeactive)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
