using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    GameController gameController;

    public enum enemyType { goblin, slime };

    public float maxHealth = 5.0f;
    public float health = 5.0f;
    [SerializeField] FloatingHealthbar enemyHealthbar;

    public bool aware;

    public enemyType eType;

    public Weapons weapon;
    public GameObject weaponRotate;
    public GameObject currWeapon;

    public GameObject weaponDrop;

    float attackTime = 0.0f;

    public float rotateSpeed = 0.5f;

    public SpriteRenderer sprite;

    Animator animator;

    public float awareRange = 20.0f;
    public float rangeDistance = 3.5f;

    public GameObject player;
    NavMeshAgent agent;

    

    void Start()
    {
        enemyHealthbar = GetComponentInChildren<FloatingHealthbar>();

        animator = GetComponentInChildren<Animator>();
        aware = false;
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponentInChildren<SpriteRenderer>();
          

        agent.stoppingDistance = 0.0f;
        sprite.transform.rotation = Quaternion.Euler(90, 0, 0);
        
        if (eType == enemyType.goblin)
        {
            currWeapon = Instantiate(gameController.GiveWeapon(weapon), weaponRotate.transform);
            currWeapon.transform.position += transform.right * 1.3f;
            rangeDistance = currWeapon.GetComponent<WeaponInfo>().preferredDistance;

            if (sprite.flipX)
            {
                weaponRotate.transform.rotation = Quaternion.Euler(90, 0, 230);
                currWeapon.GetComponent<SpriteRenderer>().flipY = true;
            }
            else
                weaponRotate.transform.rotation = Quaternion.Euler(90, 0, -40);
        }
    }

    void Update()
    {
        enemyHealthbar.UpdateHealthbar(health);

        attackTime += 1.0f * Time.deltaTime;
        float distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), player.transform.position);
        if (!aware)
        {
            if (distance < awareRange)
            {
                if ((!sprite.flipX && player.transform.position.x + 0.5f > transform.position.x) || (sprite.flipX && player.transform.position.x - 0.5f < transform.position.x))
                {
                    RaycastHit2D[] hit;
                    hit = Physics2D.RaycastAll(transform.position, (player.transform.position - transform.position), Vector3.Distance(transform.position, player.transform.position));

                    bool terrainFound = false;
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if (hit[i].collider.tag == "Terrain")
                            terrainFound = true;
                    }
                    if (!terrainFound)
                        aware = true;
                }
            }
        }
        else
        {
            if (eType == enemyType.goblin)
                agent.destination = player.transform.position;
            else
                agent.destination = gameController.transform.position;

            animator.SetBool("Moving", true);

            RaycastHit2D[] hit;
            hit = Physics2D.RaycastAll(transform.position, (player.transform.position - transform.position), Vector3.Distance(transform.position, player.transform.position));

            bool terrainFound = false;
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.tag == "Terrain")
                    terrainFound = true;
            }

            if (!terrainFound)
            {
                Vector3 mPos = player.transform.position - transform.position;
                float angle = Mathf.Atan2(mPos.y, mPos.x) * Mathf.Rad2Deg;

                Quaternion qtn = Quaternion.Euler(0, 0, angle);
                weaponRotate.transform.rotation = Quaternion.RotateTowards(weaponRotate.transform.rotation, qtn, rotateSpeed * Time.deltaTime);

                angle = qtn.eulerAngles.z;
                if (eType != enemyType.slime)
                {
                    if (weaponRotate.transform.localEulerAngles.y < 275 && weaponRotate.transform.localEulerAngles.y > 90)
                        currWeapon.GetComponent<SpriteRenderer>().flipY = true;
                    else
                        currWeapon.GetComponent<SpriteRenderer>().flipY = false;
                }

                if (distance <= rangeDistance && eType != enemyType.slime)
                {
                    if (attackTime >= currWeapon.GetComponent<WeaponInfo>().reloadTime * 2 && Mathf.Abs(Mathf.Abs(weaponRotate.transform.eulerAngles.z) - Mathf.Abs(angle)) < 1.0f)
                    {
                        attackTime = 0;
                        currWeapon.GetComponent<WeaponInfo>().Shoot(-angle + 90);
                    }
                    agent.destination = transform.position;
                    if (distance < (rangeDistance * 3)/ 4)
                        agent.destination = GameObject.FindWithTag("Finish").transform.position;
                    if (animator.GetBool("Moving"))
                        animator.SetBool("Moving", false);
                }

                if (player.transform.position.x > transform.position.x)
                    sprite.flipX = false;
                else
                    sprite.flipX = true;
            }

            if (distance > awareRange * 2.0f)
            {
                agent.destination = transform.position;
                aware = false;
            }
        }

        if (health <= 0)
        {
            

            GameObject w = Instantiate(weaponDrop, transform.position, Quaternion.identity);
            if (eType == enemyType.goblin)
                w.GetComponent<DropBehaviour>().weapon = weapon;
            else
                w.GetComponent<DropBehaviour>().isHealth = true;
            w.GetComponent<DropBehaviour>().timeDeath = true;
            w.GetComponent<DropBehaviour>().lifeTime = 7.5f;
           
            if (FindObjectOfType<Level5Controller>() != null)
                Level5Controller.Instance.enemies++;
            Destroy(gameObject);

            if (FindObjectOfType<Level5Controller>() != null)
                Level5Controller.Instance.enemies++;
        }
    }
}
