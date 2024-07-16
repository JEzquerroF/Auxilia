using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    EnemyBehaviour m_EnemyBehaviour;
    PlayerController playerController;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    private float currentTime = 0f;
    private float timeToArrive = 3f;
    private bool fire = false;
    private bool fireSound = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController.health >= 0 && fire == true)
            {
                playerController.health -= 50 * Time.deltaTime;
            }

        }
        if (collision.tag == "Enemy")
        {
            m_EnemyBehaviour = collision.gameObject.GetComponent<EnemyBehaviour>();
            if (m_EnemyBehaviour.health >= 0 && fire == true)
            {
                m_EnemyBehaviour.health -= 50 * Time.deltaTime;
            }
        }
    }


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        currentTime += Time.deltaTime;
         
        if (fire && fireSound && Vector3.Distance(Camera.main.transform.position, transform.position) < 16.0f && !AudioManager.IsSoundPlaying("Fire"))
        {
            GetComponent<ParticleSystem>().Play();
            AudioManager.PlaySFX("Fire");
            fireSound = true;
        }
        
        if (currentTime >= timeToArrive)
        {
            currentTime = 0f;
            fire = !fire;
            fireSound = true;
        }

        if (fire)
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;


            if (fireSound)
            {
                if (Vector3.Distance(Camera.main.transform.position, transform.position) < 16.0f)
                {
                    GetComponent<ParticleSystem>().Play();
                    AudioManager.PlaySFX("Fire");
                    fireSound = false;
                }
            }
        }
        else
        {
            AudioManager.StopSFX("Fire");
            GetComponent<ParticleSystem>().Stop();
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
            fireSound = true;
        }
    }
}
