using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public int damage = 2;
    public float speed = 3f;
    public float trackingTime = 1f;
    public float scatterSpeed = 5f;
    public float scatterDuration = 1f;
    private Transform player;
    private Vector2 scatterDirection;
    private bool isTracking = true;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ProjectileLifeCycle());
    }

    void Update()
    {
        if (isTracking && player != null)
        {
            TrackPlayer();
        }
    }

    private void TrackPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void Scatter()
    {
        scatterDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = scatterDirection * scatterSpeed;
    }

    private IEnumerator ProjectileLifeCycle()
    {
        yield return new WaitForSeconds(trackingTime);
        isTracking = false;

        Scatter();
        yield return new WaitForSeconds(scatterDuration);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
