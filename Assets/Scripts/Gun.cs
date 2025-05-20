using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private Transform eyes;

    public bool isEnemy;

    float timeSinceLastAttack;

    void Awake()
    {

        if (!isEnemy)
        {
            PlayerShoot.attackInput += Attack;
        }
        if (isEnemy)
        {
            EnemyAI.attackInput += Attack;
        }
    }

    private bool CanAttack() => timeSinceLastAttack > 1f / (gunData.fireRate / 60f);

    private void Attack()
    {
        pm.animator.Play("Great Sword Attack");
        if (CanAttack())
        {
            if (Physics.Raycast(eyes.position, eyes.forward, out RaycastHit hitInfo, gunData.maxDistance))
            {
                IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                damageable?.Damage(gunData.damage);
            }
            timeSinceLastAttack = 0;
        }
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        Debug.DrawRay(eyes.position, eyes.forward);
    }
}