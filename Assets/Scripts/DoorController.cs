using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    CapsuleCollider2D capsuleCollider;
    public static bool open = false;
    public int keyNumber;
    public bool alarm = false;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && KeyController.key >= keyNumber)
        {
            open = true;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (alarm == true && KeyController.key >= keyNumber)
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<EnemyBehaviour>().aware = true;
            }
        }
    }
}
