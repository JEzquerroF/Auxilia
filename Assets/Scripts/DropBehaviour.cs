using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehaviour : MonoBehaviour
{
    GameObject gameController;
    public float anglePerSecond = -25.0f;
    public bool timeDeath = false;
    public float lifeTime = 1.0f;

    public bool isHealth = false;
    public float health = 75.0f;

    public Weapons weapon;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        transform.localScale = new Vector3(1, 1, 1);
        if (!isHealth)
            Instantiate(gameController.GetComponent<GameController>().GiveWeapon(weapon), gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= 1.0f * Time.deltaTime;
        transform.Rotate(0, 0, anglePerSecond * Time.deltaTime);
        if (timeDeath && lifeTime <= 0.0f)
            Destroy(gameObject);
    }
}
