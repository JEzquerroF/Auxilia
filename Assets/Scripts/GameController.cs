using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Vector2 move = new Vector2(0, 0);

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject bow;
    public GameObject wand;
    public GameObject sword;
    public GameObject machineGun;
    public GameObject bazooka;
    public GameObject bouncer;

    public static GameController Instance { get { return gC; } }
    public static Time time = new Time();
    public Texture2D cursor;

    //private static GameController gC;
    private static GameController gC;

    private void Awake()
    {
        if (gC != null && gC != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            gC = this;
        }

        Cursor.SetCursor(cursor, new Vector2(35.0f, 35.0f), CursorMode.ForceSoftware);
    }

    void Update()
    {
        move = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            move.y += 1.0f;
        if (Input.GetKey(KeyCode.S))
            move.y -= 1.0f;
        if (Input.GetKey(KeyCode.D))
            move.x += 1.0f;
        if (Input.GetKey(KeyCode.A))
            move.x -= 1.0f;
        move.Normalize();
    }

    public GameObject GiveWeapon(Weapons weapon)
    {
        switch (weapon)
        {
            case Weapons.Pistol:
                return (pistol);
            case Weapons.Shotgun:
                return (shotgun);
            case Weapons.Bow:
                return (bow);
            case Weapons.Wand:
                return (wand);
            case Weapons.Sword:
                return (sword);
            case Weapons.MachineGun:
                return (machineGun);
            case Weapons.Bazooka:
                return (bazooka);
            case Weapons.Bouncer:
                return (bouncer);
            default: return null;
        }
    }
}