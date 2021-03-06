using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private Collider hitbox;

    [SerializeField]
    private GameObject splashEffect;
    [SerializeField]
    private GameObject potionModel;
    [SerializeField]
    private GameObject dropShadow;
    private GameObject aoeIndicator;

    [SerializeField]
    private int heal;

    private Collider[] hitColliders;

    [SerializeField]
    private float healRadius = 5f;

    void Awake()
    {
        aoeIndicator = Instantiate(dropShadow, new Vector3(transform.position.x, -4f, transform.position.z), dropShadow.transform.rotation);
    }

    void Start()
    {
        hitbox = GetComponent<SphereCollider>();
        hitbox.enabled = false;
        StartCoroutine(DisableCollider());
    }

    private IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(3.0f);

        hitbox.enabled = true;
    }

    void OnCollisionEnter(Collision other)
    {
        Heal();
    }

    // Heals the player if they are within a certain radius.
    private void Heal()
    {
        hitColliders = Physics.OverlapSphere(transform.position, healRadius);

        foreach (Collider hit in hitColliders)
        {
            GameObject go = hit.gameObject;

            if (go.tag == "Player")
            {
                go.GetComponentInChildren<HealthSystem>().gameObject.SendMessage("Heal", heal);
                hitbox.enabled = false;
            }
        }

        potionModel.GetComponent<MeshRenderer>().enabled = false;

        splashEffect.SetActive(true);
        Destroy(aoeIndicator);
        
        Invoke("Die", 3f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
