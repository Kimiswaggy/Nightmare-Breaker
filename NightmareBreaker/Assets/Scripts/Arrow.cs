using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int arrowDamage = 1;

    private void OnTriggerEnter2D (Collider2D collision)
    {
       if (collision.CompareTag("Enemy"))
        {
            Enemy enemyCurrrentHealth = collision.GetComponent<Enemy>();

            if (enemyCurrrentHealth != null)
            {
                enemyCurrrentHealth.TakeDamage(arrowDamage);
            }

            Destroy(gameObject);
        }
    }
}
