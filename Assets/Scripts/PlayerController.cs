using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameController gameController;
    AudioSource audioSource;

    public float acceleration = 1.0f;
    public float maxSpeed = 1.0f;

    private bool canMove = true;
    private bool grab = false;

    public GameObject weaponRotate;
    public Rigidbody2D rB;

    float dashTime = 0.0f;
    public float dashTimer = 0.4f;
    public float dashSpeed = 5.0f;

    private float attackTime = 0.0f;
    private float angle = 0.0f;

    public float maxHealth = 100.0f;
    public float health = 100.0f;
    public Healthbar healthBar;

    public Weapons weapon;
    public GameObject currWeapon;

    Vector2 lastMove = new Vector2(0, 0);

    Animator animator;
    public float magnitudVelocidad;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        currWeapon = Instantiate(gameController.GiveWeapon(weapon), weaponRotate.transform);
        currWeapon.transform.position += transform.right * 1.3f;
        healthBar.SetMaxHealth((int)maxHealth);
        animator = GetComponentInChildren<Animator>();
        audioSource = GameObject.FindWithTag("AudioSourcePlayer").GetComponent<AudioSource>();
        AudioManager.PlayRandomMusic();
    }

    void Update()
    {
        if (PauseMenu.isGamePaused)
            return;

        healthBar.SetHealth(health);

        if (Input.GetKey(KeyCode.LeftShift) && dashTime >= dashTimer)
        {
            rB.AddForce(new Vector2(gameController.move.x, gameController.move.y) * dashSpeed, ForceMode2D.Impulse);
            if (gameController.move == Vector2.zero)
                gameController.move = lastMove;
            dashTime = 0;
        }

        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        angle = Mathf.Atan2(mPos.y, mPos.x) * Mathf.Rad2Deg;

        if (angle < -90 || angle > 90)
            GetComponentInChildren<SpriteRenderer>().flipY = true;
        else
            GetComponentInChildren<SpriteRenderer>().flipY = false;

        weaponRotate.transform.localEulerAngles = new Vector3(0, 0, angle);

        if (Input.GetMouseButton(0) && attackTime >= currWeapon.GetComponent<WeaponInfo>().reloadTime)
        {
            attackTime = 0;
            currWeapon.GetComponent<WeaponInfo>().Shoot(-angle + 90);
            CinemachineShake.Instance.ShakeCamera(1f, 0.2f);

        }

        if (Input.GetMouseButton(1))
            grab = true;
        else
            grab = false;

        if (rB.velocity.magnitude > magnitudVelocidad)
        {
            AudioManager.PlaySFX("PlayerRun", audioSource);
        }
        else
        {
            audioSource.Stop();
        }

        if (health <= 0)
        {
            if (GameObject.FindWithTag("Finish").GetComponent<LevelEnd>().level == 5)
                GameObject.FindWithTag("Finish").GetComponent<LevelEnd>().EnemyRush();
            animator.gameObject.SetActive(false);
            PauseMenu pause = GameObject.Find("/Player/PauseCanvas").GetComponent<PauseMenu>();
            pause.Pause();
            pause.canBeUnpaused = false;
            GameObject.Find("/Player/PauseCanvas/PauseMenu/ResumeButton").SetActive(false);
            GameObject.Find("/Player/PauseCanvas/PauseMenu/OptionsButton").SetActive(false);
            GameObject.Find("/Player/PauseCanvas/PauseMenu/QuitButton").SetActive(false);
        }
    }

    void FixedUpdate()
    {
        canMove = true;
        if (dashTime < dashTimer)
        {
            if (rB.velocity.magnitude > maxSpeed * 1.75f)
                canMove = false;
            dashTime += 1.0f * Time.deltaTime;
        }
        attackTime += 1.0f * Time.deltaTime;
        
        if (gameController.move != Vector2.zero && rB.velocity.magnitude < maxSpeed && canMove)
        {
            if (gameController.move.x < 0)
                animator.GetComponent<SpriteRenderer>().flipX = true;
            else if (gameController.move.x > 0)
                animator.GetComponent<SpriteRenderer>().flipX = false;
            rB.AddForce(new Vector2(gameController.move.x, gameController.move.y) * maxSpeed, ForceMode2D.Force);
            lastMove = gameController.move; 
        }
        GetAnimationState();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Drop" && grab)
        {
            if (!coll.GetComponent<DropBehaviour>().isHealth)
            {
                Destroy(currWeapon);
                currWeapon = Instantiate(gameController.GiveWeapon(coll.GetComponent<DropBehaviour>().weapon), weaponRotate.transform);
                currWeapon.transform.position += currWeapon.transform.right * 1.3f;
            }
            else
            {
                health += coll.GetComponent<DropBehaviour>().health;
                if (health > maxHealth)
                    health = maxHealth;
            }
            Destroy(coll.gameObject);
        }
    }

    public void GetAnimationState()
    {
        if ((int)rB.velocity.magnitude != 0)
        {
            if (lastMove.x != 0)
                animator.SetBool("Side", true);
            else if (lastMove.y > 0)
                animator.SetBool("Up", true);
            else
                animator.SetBool("Down", true);
        }
        else
        {
            animator.SetBool("Side", false);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
        }
    }
}
        

