using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public Transform loot;

    public float health = 100f;
    private Rigidbody rb;

    private float time;

    public bool isEnemy;

    private void Update()
    {
        if (health > 100f)
        {
            health = 100f;
        }

        if (health <= 0)
        {
            rb.freezeRotation = false;
            StartCoroutine(Despawn());
        }
    }

    public void Damage(float damage)
    {
        rb = GetComponent<Rigidbody>();
        health -= damage;

        rb.AddForce(-rb.velocity * 2f, ForceMode.Impulse);
    }

    IEnumerator Despawn()
    {
        if (isEnemy)
        {
            GetComponent<EnemyAI>().enabled = false;
            yield return new WaitForSeconds(7);

            Destroy(gameObject);
            Instantiate(loot, gameObject.transform.position, gameObject.transform.rotation);
        }
        else
        {
            GetComponent<PlayerShoot>().enabled = false;
        }
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
    }
}
