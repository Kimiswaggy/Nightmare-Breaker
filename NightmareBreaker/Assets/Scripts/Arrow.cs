using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int arrowDamage = 1;

    public float gravity = 0.5f;
    public float arrowLifeTime = 3f;
    public float arrowSpeed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, arrowLifeTime); // Destroy arrow after 3s
    }

    void Update()
    {
       //apply gravity to simulate realistic arrow behavior
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravity*Time.deltaTime);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
       if (collision.CompareTag("Enemy"))
        {
            Enemy_Base enemyCurrrentHealth = collision.GetComponent<Enemy_Base>();

            if (enemyCurrrentHealth != null)
            {
                enemyCurrrentHealth.TakeDamage(arrowDamage);
            }

            Destroy(gameObject);
        }
    }
}
