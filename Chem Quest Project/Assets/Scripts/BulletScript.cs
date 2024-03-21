using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public LayerMask layerToIgnore;
    public LayerMask layerToBeDestroyed;
    public string enemyTag = "Enemy";

    void Start()
    {
        Collider2D[] collidersToIgnore = Physics2D.OverlapCircleAll(transform.position, 500, layerToIgnore);
        foreach (Collider2D collider in collidersToIgnore)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collider, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (layerToBeDestroyed == (layerToBeDestroyed | (1 << collision.gameObject.layer)))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag(enemyTag))
        {
            collision.gameObject.SendMessage("Reaction");
            Destroy(gameObject);
        }
    }
}
