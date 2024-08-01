using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] Rigidbody2D rb;
    private void Start()
    {
        playerMovement = GameManager.instance.player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.isOnPlatform = true;
            playerMovement.platformRb = rb;
            playerMovement.playerRb.gravityScale *= 50;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.isOnPlatform = false;
            playerMovement.platformRb = null;
            playerMovement.playerRb.gravityScale /= 50;
        }
    }
}
