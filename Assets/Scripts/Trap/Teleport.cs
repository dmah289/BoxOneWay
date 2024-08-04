using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class Teleport : MonoBehaviour
{
    [SerializeField] Animation animPlayer;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Transform destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(KeySave.player))
        {
            if(Vector2.Distance(playerRb.transform.position, transform.position) > 0.3f)
            {
                StartCoroutine(PortalIn());
            }
        }
    }

    IEnumerator PortalIn()
    {
        playerRb.simulated = false;
        animPlayer.Play(KeySave.portalIn);
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(1);
        playerRb.transform.position = destination.position;
        playerRb.velocity = Vector2.zero;
        animPlayer.Play(KeySave.portalOut);
        yield return new WaitForSeconds(0.5f);
        playerRb.simulated = true;
    }

    IEnumerator MoveInPortal()
    {
        float timer = 0;
        while(timer < 0.5f)
        {
            playerRb.transform.position = Vector2.MoveTowards(playerRb.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
