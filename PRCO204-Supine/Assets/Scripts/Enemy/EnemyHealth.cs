﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Variables 
    [SerializeField]
    private GameObject keyPrefab;

    [SerializeField] 
    private int health = 100;
    [SerializeField] 
    private int knockback;

    private float shakeHitAmount = 0.5f;
    private float shakeDieAmount = 0.5f;

    private Room parentRoom;

    private EnemyCountManager enemyCountManager;
    public GameObject crossbowPrefab;

    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject smallSlime;

    [SerializeField]
    private bool isSlime = false;

    private bool hasDied = false;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<Room>())
        {
            parentRoom = GetComponentInParent<Room>();
        }
        else
        {
            Debug.LogError("No parent room found attached to enemy " + this.gameObject.name + ". Enemies need to be children of the room object.");
        }

        enemyCountManager = GameObject.FindGameObjectWithTag("EnemyCountMan").GetComponent<EnemyCountManager>();
    }

    // Update is called once per frame
    // Checks to 
    void Update()
    {
        if (health <= 0 && !hasDied)
        {
            hasDied = true;

            if (!isSlime)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }
            else
            {
                parentRoom.enemyCountManager.enemyCount += 3;

                Instantiate(smallSlime, transform.position, transform.rotation, parentRoom.transform);
                Instantiate(smallSlime, transform.position, transform.rotation, parentRoom.transform);
            }

            if (CameraShake.shake <= shakeDieAmount)
            {
                CameraShake.shake = shakeDieAmount;
            }

            Die();
        }
    }


    // Takes the amount sent to the method away from the enemies health
    // then applies a knockback affect to the enemy.
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (CameraShake.shake <= shakeHitAmount)
        {
            CameraShake.shake = shakeHitAmount;
        }

        gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * knockback,ForceMode.Impulse);
    }
    

    public void DropItem()
    {
        System.Random rand = new System.Random();
        int drop = rand.Next(1, 10);

        if (drop == 10)
        {
            //Spawn dropped item here.
        }
    }

    void Die()
    {
        triggerCrossbowCheck();
        DropItem();

        parentRoom.enemyKilled(this);

        if (parentRoom.enemyCountManager.enemyCount == 0)
        {
            Instantiate(keyPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
    
    public void triggerCrossbowCheck()
    {
        // if half the enemies have been killed, spawn a crossbow
        if (enemyCountManager.halfEnemyCount == enemyCountManager.enemyCount)
        {
            Instantiate(crossbowPrefab, parentRoom.transform, false);
        }
    }
}
