using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    public Transform orientation;

    public float moveSpeed;
    public float groundDrag;

    private bool grounded;
    public LayerMask ground;
    public float enemyHeight;

    private Rigidbody rb;

    private Vector3 moveDir;
    private Vector3 randomDir;

    public static Action attackInput;
    public static Action reloadInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = groundDrag;

        player = GameObject.Find("Player");
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down, enemyHeight * 0.5f + 0.2f, ground);

        orientation.LookAt(player.transform);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (grounded)
        {
            Vector3 playerDir = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            moveDir = playerDir - transform.position;

            if (Vector3.Distance(transform.position, player.transform.position) <= 15 && Vector3.Distance(transform.position, player.transform.position) > 2)
            {
                StopCoroutine(Wander());

                rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            else if (Vector3.Distance(transform.position, player.transform.position) > 15)
            {
                // randomly move the enemy but for now it stops moving
                // rb.AddForce(-(moveDir.normalized * moveSpeed * 10f), ForceMode.Force);
                StartCoroutine(Wander());
            }

            if (Vector3.Distance(transform.position, player.transform.position) < 7)
            {
                attackInput?.Invoke();
            }
        }
    }

    IEnumerator Wander()
    {
        yield return new WaitForSeconds(5);

        randomDir = new Vector3(UnityEngine.Random.Range(-5, 5) - transform.position.x, transform.position.y, UnityEngine.Random.Range(-5, 5) - transform.position.z);

        rb.AddForce(randomDir.normalized * moveSpeed * 10f, ForceMode.Force);

    }
}
