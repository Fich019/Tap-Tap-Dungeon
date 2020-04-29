﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    private Collider hitbox;

    [SerializeField] 
    private GameObject bombRad;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject bombModel;

    [SerializeField] 
    private int damage;

    private Collider[] hitColliders;

    [SerializeField]
    private float bombRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = bombRad.GetComponent<SphereCollider>();
        hitbox.enabled = false;
        StartCoroutine(DisableCollider());

        Invoke("Explode", 3f);
    }

    private IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(3.0f);

        hitbox.enabled = true;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        other.gameObject.SendMessage("TakeDamage", damage);
    //        hitbox.enabled = false;
    //    }

    //    if (other.gameObject.tag == "Player")
    //    {
    //        HealthManager.playerHealth.TakeDamage(damage / 2);
    //        hitbox.enabled = false;
    //    }
    //}

    private void Explode()
    {
        hitColliders = Physics.OverlapSphere(transform.position, bombRadius);

        foreach (Collider hit in hitColliders)
        {
            GameObject go = hit.gameObject;

            if (go.tag == "Enemey")
            {
                go.SendMessage("TakeDamage", damage);
                hitbox.enabled = false;
            }
            else if (go.tag == "Player")
            {
                HealthManager.playerHealth.TakeDamage(damage / 2);
                hitbox.enabled = false;
            }
        }

        explosion.SetActive(true);
        bombModel.SetActive(false);

        Invoke("Die", 3f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    
}
