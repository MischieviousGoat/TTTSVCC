using System;
using System.Collections;
using Unity.VisualScripting;
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
        grounded = Physics.Raycast(transform.position, Vector3.down, enemyHeight * 0.5f + 0.2f, ground);

        orientation.LookAt(player.transform);
        transform.forward = orientation.forward;

        if (Vector3.Distance(transform.position, player.transform.position) < 7)
        {
            attackInput?.Invoke();
        }
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

            if (Vector3.Distance(transform.position, player.transform.position) > 4)
            {
                rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
            }
        }
    }
}
