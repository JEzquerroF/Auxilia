using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveBehaviour : MonoBehaviour
{
    public new Animator animation;
    public Collider2D coll;
    
    public float explosionRadius = 3.0f;
    public float explosionDamage = 5.0f;

    public float speed = 1.0f;

    public bool bullet = false;

    float endTime = 0.0f;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        if (!bullet)
            animation.enabled = false;
        animation.speed = speed;
        sprite = GetComponent<SpriteRenderer>();
        sprite.size = new Vector2(8, 8);
    }

    // Update is called once per frame
    void Update()
    {
        if (animation.isActiveAndEnabled)
        {
            endTime += Time.deltaTime;
            if (endTime > speed * 0.6f)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Bullet")
            Explode();
    }

    public void Explode()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag == "Enemy")
            {
                hit[i].collider.gameObject.GetComponent<EnemyBehaviour>().aware = true;
                hit[i].collider.gameObject.GetComponent<EnemyBehaviour>().health -= explosionDamage;
                hit[i].collider.gameObject.GetComponent<Rigidbody2D>().AddForce(((Vector2)coll.transform.position - (Vector2)transform.position).normalized * 20, ForceMode2D.Impulse);
            }
            if (hit[i].collider.tag == "Player")
            {
                hit[i].collider.gameObject.GetComponent<PlayerController>().health -= explosionDamage;
                hit[i].collider.gameObject.GetComponent<Rigidbody2D>().AddForce(((Vector2)coll.transform.position - (Vector2)transform.position).normalized * 20, ForceMode2D.Impulse);
            }
        }
        animation.enabled = true;
        AudioManager.PlaySFX("Explosion");
        coll.enabled = false;
        transform.localScale = transform.localScale * 1.25f;
        CinemachineShake.Instance.ShakeCamera(5f, 0.5f);
    }
}
