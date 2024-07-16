using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public bool damageToPlayer = true;
    public bool destroyOnTrigger = true;
    public bool explodeOnTrigger = false;
    public bool bounce = false;
    public float acceleration = 0.0f;
    public float speed = 1.0f;
    public float iLifeTime = 1.0f;
    public float damage = 1.0f;

    float lifeTime;

    public SpriteRenderer sprite;

    public Sprite slashSprite;
    public Sprite bulletSprite;
    public Sprite spellSprite;
    public Sprite arrowSprite;
    public Sprite explosiveSprite;
    public Sprite bubbleSprite;

    public Weapons bulletType;

    public GameObject explosive;

    void Start()
    {
        switch (bulletType)
        {
            case Weapons.Bow:
                sprite.sprite = arrowSprite;
                break;
            case Weapons.Sword:
                sprite.sprite = slashSprite;
                break;
            case Weapons.Wand:
                sprite.sprite = spellSprite;
                break;
                case Weapons.Bazooka: 
                sprite.sprite = explosiveSprite;
                break;
            case Weapons.Bouncer:
                sprite.sprite = bubbleSprite;
                CircleCollider2D c = gameObject.AddComponent<CircleCollider2D>();
                c.isTrigger = false;
                break;
            default: 
                sprite.sprite = bulletSprite;    
                break;
        }
        lifeTime = iLifeTime;
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= 1.0f * Time.deltaTime;

        speed += acceleration * Time.deltaTime;
        if (speed < 0)
            speed = 0;

        gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity.normalized * speed * Time.deltaTime, ForceMode2D.Impulse);

        if (lifeTime <= 0.0f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
 
        if (coll.tag == "Enemy")
        {
          
            coll.attachedRigidbody.AddForce(transform.up * damage, ForceMode2D.Impulse);
            coll.GetComponent<EnemyBehaviour>().aware = true;
            coll.GetComponent<EnemyBehaviour>().health -= damage;
            if (explodeOnTrigger)
            {
                GameObject ex = Instantiate(explosive, transform.position, Quaternion.identity);
                ex.GetComponent<ExplosiveBehaviour>().explosionDamage = damage;
                ex.GetComponent<ExplosiveBehaviour>().Explode();
                ex.GetComponent<ExplosiveBehaviour>().bullet = true;
            }
            if (destroyOnTrigger)
                Destroy(gameObject);
            return;
        }

        if (coll.tag == "Player" && damageToPlayer)
        {
            coll.attachedRigidbody.AddForce(transform.up * damage, ForceMode2D.Impulse);
            coll.GetComponent<PlayerController>().health -= damage;
            CinemachineShake.Instance.ShakeCamera(3f, 0.2f);
            if (explodeOnTrigger)
            {
                GameObject ex = Instantiate(explosive, transform.position, Quaternion.identity);
                ex.GetComponent<ExplosiveBehaviour>().explosionDamage = damage;
                ex.GetComponent<ExplosiveBehaviour>().Explode();
                ex.GetComponent<ExplosiveBehaviour>().bullet = true;
            }

            if (destroyOnTrigger)
                Destroy(gameObject);
            return;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Terrain")
        {
            if (explodeOnTrigger)
            {
                GameObject ex = Instantiate(explosive, transform.position, Quaternion.identity);
                ex.GetComponent<ExplosiveBehaviour>().explosionDamage = damage;
                ex.GetComponent<ExplosiveBehaviour>().Explode();
                ex.GetComponent<ExplosiveBehaviour>().bullet = true;
                Destroy(gameObject);       
            }

            if ((iLifeTime - lifeTime > 0.1f || (iLifeTime - lifeTime > 0.03f && bulletType == Weapons.Bow)) && bounce == false)
                Destroy(gameObject);
        }
    }
}
