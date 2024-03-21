using UnityEngine;
using UnityEngine.UIElements;

public class CanvasFlip : MonoBehaviour
{
    public Transform enemyTransform;
    private Vector3 offset;

    private Vector3 initialScale;

    private void Start()
    {
        enemyTransform = transform.parent;
        offset = transform.position - enemyTransform.position;
        transform.SetParent(null);
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (enemyTransform != null)
        {
            transform.localScale = Vector3.one;
            transform.position = enemyTransform.position + offset;


            if(enemyTransform.gameObject.active == true)
            {
                Vector3 newScale = new Vector3(Mathf.Abs(initialScale.x), Mathf.Abs(initialScale.y), Mathf.Abs(initialScale.z));
                transform.localScale = newScale;
            }
            else
            {
                transform.localScale = Vector3.zero;
            }
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
}