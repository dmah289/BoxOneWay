using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] CameraController cameraController;
    public ParticleController particleController;

    private Vector2 checkPointPos;

    private void Start()
    {
        checkPointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(KeySave.obstacle))
        {
            Die();
        }
    }

    private void Die()
    {
        cameraController.animator.Play(KeySave.whiteScreen);
        particleController.PlayParticle(ParticleTypes.die, transform.position);
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        playerRb.simulated = false;
        playerRb.velocity = Vector2.zero;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        transform.position = checkPointPos;
        transform.localScale = Vector3.one;
        playerRb.simulated = true;
    }

    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPointPos = pos.With(x: pos.x + 0.5f, y: pos.y + 2f);
    }
}
