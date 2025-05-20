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
            PlayerShoot.lightAttackInput += LightAttack;
            PlayerShoot.heavyAttackInput += HeavyAttack;
        }
        /* if (isEnemy)
        {
            EnemyAI.attackInput += ttack;
        } */
    }

    private bool CanLightAttack() => timeSinceLastAttack > 1f / (gunData.lightFireRate / 60f);

    private bool CanHeavyAttack() => timeSinceLastAttack > 1f / (gunData.heavyFireRate / 60f);

    private void LightAttack()
    {
        if (CanLightAttack())
        {
            pm.animator.Play("Great Sword Attack");
            if (Physics.Raycast(eyes.position, eyes.forward, out RaycastHit hitInfo, gunData.maxDistance))
            {
                IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                damageable?.Damage(gunData.lightDamage);
            }
            timeSinceLastAttack = 0;
        }
    }

    private void HeavyAttack()
    {
        if (CanHeavyAttack())
        {
            pm.animator.Play("Great Sword Slash");
            if (Physics.Raycast(eyes.position, eyes.forward, out RaycastHit hitInfo, gunData.maxDistance))
            {
                IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                damageable?.Damage(gunData.heavyDamage);
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