using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour
{
    public int healAmount = 2;
    public float displayRange = 8f;
    public float healDuration = 1.5f;
    public TextMeshProUGUI healText;
    private Animator animator;
    private Transform player;
    private bool isHealing = false;
    private bool isInRange = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healText.gameObject.SetActive(false);
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (healText != null)
        {
            if (distanceToPlayer <= displayRange)
            {
                ShowHealText();
                isInRange = true;
            }
            else
            {
                HideHealText();
                isInRange = false;
            }
        }
        
        if (Input.GetKey(KeyCode.E) && isInRange && !isHealing)
        {
            StartCoroutine (HealPlayer());
        }
    }

    void ShowHealText()
    {
        healText.gameObject.SetActive(true);
    }

    void HideHealText()
    {
        healText.gameObject.SetActive(false);
    }

    IEnumerator HealPlayer ()
    {
        isHealing = true;
        Debug.Log("Heal Player starts");

        float healProgress = 0f; 

        while (healProgress < healDuration)
        {
            if (!Input.GetKey(KeyCode.E))
            {
                isHealing = false;
                Debug.Log("heal stops");
                yield break;
            }

            healProgress += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Heal Complete");
        animator.SetTrigger("Drink");

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Player is healed");
            playerHealth.Heal(healAmount);
        }

        if (healText != null)
        {
            Destroy(healText.gameObject);
        }

        isHealing = false;
    }
}
