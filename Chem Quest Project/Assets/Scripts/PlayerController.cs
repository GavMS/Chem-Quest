using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    public GameObject heldElement;

    public Element highlightedElement;

    public float raycastDistance;
    public float raycastRadius;

    public LayerMask boxLayer;
    public LayerMask elementLayer;

    public GameObject Gun;

    bool CanShoot = false;

    public GameObject bulletPrefab;
    public float shootingDelay = 0.5f;
    public float bulletSpeed = 20f;

    private bool isShooting = false;

    public Color slowColor;

    public float slowDuration = 5;

    float originalSpeed;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Gun.SetActive(false);
        originalSpeed = moveSpeed;
    }




    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

        rb.velocity = movement * moveSpeed;

        if (moveHorizontal > 0)
        {
            transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
        }
        if (moveHorizontal < 0)
        {
            transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
        }
        if (heldElement == null)
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, raycastRadius, Vector2.right, raycastDistance, elementLayer);

            if (hit.collider != null && hit.collider.GetComponent<Element>())
            {

                if (highlightedElement != hit.collider.GetComponent<Element>())
                {
                    if (highlightedElement != null)
                    {
                        highlightedElement.highlight.SetActive(false);
                    }

                    highlightedElement = hit.collider.GetComponent<Element>();
                    highlightedElement.highlight.SetActive(true);
                }

            }
            else if (highlightedElement != null)
            {
                highlightedElement.highlight.SetActive(false);
                highlightedElement = null;
            }
        }

        if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Space))
        {

            if (!heldElement)
            {

                RaycastHit2D hitElement = Physics2D.CircleCast(transform.position, raycastRadius, Vector2.right, raycastDistance, elementLayer);

                if (hitElement.collider != null && hitElement.collider.GetComponent<Element>())
                {
                    GameObject.Find("Game Manager").GetComponent<AudioScript>().PlayAudio(0);
                    hitElement.collider.GetComponent<Element>().Pickup();

                    heldElement = hitElement.collider.gameObject;
                }

            }
            else
            {

                RaycastHit2D hitBox = Physics2D.CircleCast(transform.position, raycastRadius, Vector2.right, raycastDistance, boxLayer);

                if (hitBox.collider != null)
                {
                    GameObject.Find("Game Manager").GetComponent<AudioScript>().PlayAudio(1);
                    hitBox.collider.GetComponent<Box>().PlaceElement(heldElement.GetComponent<Element>());

                }

            }
        }

        if (Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0))
        {
            if (CanShoot && !isShooting)
            {
                StartCoroutine(ShootCoroutine());
            }
        }
    }

    IEnumerator ShootCoroutine()
    {
        isShooting = true;
        GameObject bullet = Instantiate(bulletPrefab, Gun.transform.GetChild(0).transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (transform.GetChild(0).transform.localScale.x == -1)
        {
            bulletRb.velocity = new Vector2(bulletSpeed, 0);
        }
        else
        {
            bulletRb.velocity = new Vector2(-bulletSpeed, 0);
            bullet.transform.localScale = new Vector3(-1, 1, 1);
        }
        yield return new WaitForSeconds(shootingDelay);
        isShooting = false;
    }

    public void EquipGun()
    {
        CanShoot = true;
        Gun.SetActive(true);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.right * raycastDistance, raycastRadius);
    }

    public void ReactionPlayer()
    {
        StartCoroutine(ReactCoroutine());
    }

    IEnumerator ReactCoroutine()
    {
        SpriteRenderer playerSpriteRenderer = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();

        // decrease speed by half
        moveSpeed /= 2;

        // change sprite color to slowColor
        playerSpriteRenderer.color = slowColor;

        // wait for slowDuration
        yield return new WaitForSeconds(slowDuration);

        // set speed back to normal
        moveSpeed = originalSpeed;

        // change sprite color back to white
        playerSpriteRenderer.color = Color.white;
    }
}