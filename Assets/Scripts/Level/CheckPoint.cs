using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] BoxCollider2D boxCollider;

    private void Start()
    {
        gameController = GameManager.instance.player.GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(KeySave.player))
        {
            gameController.UpdateCheckPoint(transform.position);
            boxCollider.enabled = false;
        }
    }
}
