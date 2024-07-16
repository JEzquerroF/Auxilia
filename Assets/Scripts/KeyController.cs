using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public static int key;
    public GameObject KeyObject;
    public Vector2 keyPos;
   
    private void Start()
    {
        key = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            key = key + 1;

            KeyObject.transform.position = keyPos;
        }
    }

    private void Update()
    {
        if (DoorController.open == true)
        {
            Destroy(KeyObject);
        }
    }
}
