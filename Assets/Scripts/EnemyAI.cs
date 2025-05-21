using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    public Transform orientation;

    public Animator a;

    public float moveSpeed;
    public float groundDrag;

    private bool grounded;
    public LayerMask ground;
    public float enemyHeight;

    private Rigidbody rb;

    private Vector3 moveDir;
    private Vector3 randomDir;

    public static Action attackInput;

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
        transform.forward = orientation.forward;
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

            if (Vector3.Distance(transform.position, player.transform.position) > 5)
            {
                a.SetFloat("Speed", moveSpeed);

                rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
            } else {
                a.SetFloat("Speed", 0);

                attackInput?.Invoke();
            }
        }
    }
}
