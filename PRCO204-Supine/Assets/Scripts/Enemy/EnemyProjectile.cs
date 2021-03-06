using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Variables
    [SerializeField] 
    private float velocity;
    [SerializeField] 
    private int damage;

    [SerializeField]
    private AudioSource playerHurt;

    [SerializeField]
    private GameObject fireBall;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject debris;

    // Start is called before the first frame update
    // Adds a force the the projectile.
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * velocity);
        StartCoroutine(Despawn());
    }

    // Waits 5 seconds to check to see if anything collides with it,
    // if not it destroys itself.
    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5.0f);
        FireDebris();
    }

    // If the bullet collides with a gameobject with the tag, "player" it 
    // sends a message to the player's health script to run the take damage
    // method. It then kills itself.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FireDebris();

            playerHurt.Play();
            other.gameObject.GetComponentInChildren<HealthSystem>().gameObject.SendMessage("TakeDamage", damage);

            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        else 
        {
            List<string> excludedTags = new List<string>() { "Weapon", "Key", "Trap Door", "RoomSpawn", "RoomTrigger", "RoomSpawnSensor", "Damage", "Enemy"};
            if (excludedTags.TrueForAll(tag => !other.gameObject.CompareTag(tag))) {
                FireDebris();
            }
        }
    }


    // Destroys the gameobject this is attached to
    // and enables particle systems.
    private void FireDebris()
    {
        CapsuleCollider cc = GetComponent<CapsuleCollider>();
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeAll;
        cc.enabled = false;

        debris.SetActive(true);
        fireBall.SetActive(false);

        Invoke("Die", 4f);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    Vector3 GetBehindPosition(Transform target)
    {
        return target.position - target.forward;
    }
}
