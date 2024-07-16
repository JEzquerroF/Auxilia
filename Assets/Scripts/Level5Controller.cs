using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Level5Controller : MonoBehaviour
{
    public static Level5Controller Instance;

    public int enemies = 0;
    private int spawnEnemy=0;
    private int spawnSlime=0;

    public GameObject enemy;
    public GameObject slime;

    private float x;
    private float y;

    private float tiempoSpawn;

    private void Awake()
    {
        
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }

    void Start()
    {
        tiempoSpawn = 0f;
    }

    void Update()
    {
        CoordenadasSpawn();
        int chooseWeapon;
        chooseWeapon = Random.Range(0, 8);
        bool fase1 =false, fase2 = false,fase3 = false;

        tiempoSpawn += Time.deltaTime;

        Debug.Log(tiempoSpawn);
       //Debug.Log(spawnSlime);
       //Debug.Log(spawnEnemy);

        if (tiempoSpawn >=10 && fase1 == false) 
        {
            if(spawnEnemy <=8) 
            {

                GameObject enemy1 = Instantiate(enemy, new Vector3(x, y), Quaternion.identity);
                enemy1.GetComponent<EnemyBehaviour>().weapon = (Weapons)chooseWeapon;
                
                spawnEnemy++;
            }
            if (spawnSlime <= 4)
            {
                Instantiate(slime, new Vector3(x, y), Quaternion.identity);
                spawnSlime++;
            }

            if (spawnEnemy == 9 && spawnSlime == 5) {  fase1 = true; }
        }


        if (tiempoSpawn >= 20 && fase2 ==false)
        {
            if (spawnEnemy <= 16)
            {

                GameObject enemy1 = Instantiate(enemy, new Vector3(x, y), Quaternion.identity);
                enemy1.GetComponent<EnemyBehaviour>().weapon = (Weapons)chooseWeapon;

                spawnEnemy++;
            }
            if (spawnSlime <= 9)
            {
                Instantiate(slime, new Vector3(x, y), Quaternion.identity);
                spawnSlime++;
            }

            if (spawnEnemy == 17 && spawnSlime == 10) { fase2 = true; }
        }

        if (tiempoSpawn >= 30 && fase3 == false)
        {
            if (spawnEnemy <= 32)
            {

                GameObject enemy1 = Instantiate(enemy, new Vector3(x, y), Quaternion.identity);
                enemy1.GetComponent<EnemyBehaviour>().weapon = (Weapons)chooseWeapon;

                spawnEnemy++;
            }
            if (spawnSlime <= 14)
            {
                Instantiate(slime, new Vector3(x, y), Quaternion.identity);
                spawnSlime++;
            }

            if (spawnEnemy == 33 && spawnSlime == 15) { fase3 = true; }
        }

        if (tiempoSpawn >= 56 && (int)tiempoSpawn%20 ==0)
        {
            if (spawnEnemy <= 52)
            {

                GameObject enemy1 = Instantiate(enemy, new Vector3(x, y), Quaternion.identity);
                enemy1.GetComponent<EnemyBehaviour>().weapon = (Weapons)chooseWeapon;

                spawnEnemy++;
            }
            if (spawnSlime <= 19)
            {
                Instantiate(slime, new Vector3(x, y), Quaternion.identity);
                spawnSlime++;
            }

            if (spawnEnemy == 53 && spawnSlime == 20) {spawnEnemy = 30; }
        }
    }

    private void CoordenadasSpawn()
    {
        x = Random.Range((float)-14.49,(float)15.58);
        y = Random.Range((float)12.68, (float)-13.82);
    }
}
