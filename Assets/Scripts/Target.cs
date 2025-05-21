using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public Transform loot;
    public Animator animator;

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

        if (health >= 0)
        {
            if (isEnemy)
            {
                animator.Play("Hit Reaction");
            } else
            {
                animator.Play("Great Sword Impact");
            }
        }
    }

    IEnumerator Despawn()
    {
        if (isEnemy)
        {
            animator.Play("Death");

            GetComponent<EnemyAI>().enabled = false;

            yield return new WaitForSeconds(2);

            Destroy(gameObject);
        }
        else
        {
            animator.Play("Two Handed Sword Death");

            GetComponent<PlayerShoot>().enabled = false;
            GetComponent<Sliding>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;

            yield return new WaitForSeconds(2);

            Destroy(gameObject);

        }
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
    }
}
