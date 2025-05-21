using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BatData batData;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private Transform eyes;

    float timeSinceLastAttack;

    void Awake()
    {
        PlayerShoot.lightAttackInput += LightAttack;
        PlayerShoot.heavyAttackInput += HeavyAttack;
    }

    private bool CanLightAttack() => timeSinceLastAttack > 1f / (batData.lightFireRate / 60f);

    private bool CanHeavyAttack() => timeSinceLastAttack > 1f / (batData.heavyFireRate / 60f);

    private void LightAttack()
    {
        if (CanLightAttack())
        {
            pm.animator.Play("Great Sword Attack");
            if (Physics.Raycast(eyes.position, eyes.forward, out RaycastHit hitInfo, batData.maxDistance))
            {
                IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                damageable?.Damage(batData.lightDamage);
            }
            timeSinceLastAttack = 0;
        }
    }

    private void HeavyAttack()
    {
        if (CanHeavyAttack())
        {
            pm.animator.Play("Great Sword Slash");
            if (Physics.Raycast(eyes.position, eyes.forward, out RaycastHit hitInfo, batData.maxDistance))
            {
                IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                damageable?.Damage(batData.heavyDamage);
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