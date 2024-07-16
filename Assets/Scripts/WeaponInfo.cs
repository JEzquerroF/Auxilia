using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapons { Sword, Pistol, Shotgun, Bow, Wand, MachineGun, Bazooka, Bouncer };

public class WeaponInfo : MonoBehaviour
{
    public Weapons weapon;
    public int damage;
    public float reloadTime;
    public float speed;
    public float speedVariation;
    public float acceleration;
    public float bulletLifeTime;
    public float preferredDistance;
    public Vector2 instancePoint;
    public int bulletNumber;

    public float spread;

    public GameObject bullet;

    ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void Shoot(float angle)
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            GameObject blt = Instantiate(bullet, transform.position + transform.right * instancePoint.x + transform.up * instancePoint.y, Quaternion.Euler(0, 0, -angle + Random.Range(-spread, spread)));
            blt.GetComponent<BulletBehaviour>().speed = speed + Random.Range(-speedVariation, speedVariation);
            blt.GetComponent<BulletBehaviour>().acceleration = acceleration;
            blt.GetComponent<BulletBehaviour>().iLifeTime = bulletLifeTime;
            blt.GetComponent<BulletBehaviour>().damage = damage;
            blt.GetComponent<BulletBehaviour>().bulletType = weapon;

            if (weapon == Weapons.Bazooka)
                blt.GetComponent<BulletBehaviour>().explodeOnTrigger = true;

            if (weapon == Weapons.Sword)
                blt.transform.localScale = new Vector3(4.0f, 4.0f, 1);
            else
                blt.transform.localScale = new Vector3(0.8f, 0.8f, 1);

            if (transform.parent.parent.tag == "Player")
                blt.GetComponent<BulletBehaviour>().damageToPlayer = false;

            if (weapon == Weapons.Bow)
                blt.GetComponent<BulletBehaviour>().destroyOnTrigger = false;

            if (weapon == Weapons.Bouncer)
                blt.GetComponent<BulletBehaviour>().bounce = true;
        }

        if (particle)
        {
            particle.Play();
        }

        switch (weapon)
        {
            case Weapons.Sword:
                AudioManager.PlaySFX("Sword");
                break;
            case Weapons.Bow:
                AudioManager.PlaySFX("Bow");
                break;
            case Weapons.Pistol:
                AudioManager.PlaySFX("Pistol");
                break;
            case Weapons.MachineGun:
                AudioManager.PlaySFX("Machinegun");
                break;
            case Weapons.Shotgun:
                AudioManager.PlaySFX("Shotgun");
                break;
            case Weapons.Wand:
                AudioManager.PlaySFX("Wand");
                break;
            case Weapons.Bazooka:
                AudioManager.PlaySFX("Bazooka");
                break;
            case Weapons.Bouncer:
                AudioManager.PlaySFX("Bouncer");
                break;
        }
    }
}
