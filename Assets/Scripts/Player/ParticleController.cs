using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleTypes
{
    move, fall, touch, die
}

public class ParticleController : MonoBehaviour
{
    // Components
    [SerializeField] ParticleSystem movementParticle;
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem touchParticle;
    [SerializeField] ParticleSystem dieParticle;
    [SerializeField] Rigidbody2D playerRb;

    // Stats
    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;
    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;
    
    // Logic
    float counter;
    bool isOnGround;

    private void OnEnable()
    {
        touchParticle.transform.parent = null;
        dieParticle.transform.parent = null;
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if(isOnGround && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if(counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }

    public void PlayParticle(ParticleTypes particleType, Vector3 pos)
    {
        switch(particleType)
        {
            case ParticleTypes.touch:
                touchParticle.transform.position = pos;
                touchParticle.Play();
                break;
            case ParticleTypes.die:
                dieParticle.transform.position = pos;
                dieParticle.Play();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(KeySave.ground))
        {
            fallParticle.Play();
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(KeySave.ground))
        {
            isOnGround = false;
        }
    }
}
