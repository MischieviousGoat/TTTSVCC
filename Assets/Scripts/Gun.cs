using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform eyes;

    float timeSinceLastAttack;

    void Awake()
    {
        EnemyAI.attackInput += Attack;
        EnemyAI.reloadInput += StartReload;
        gunData.currentAmmo = gunData.magSize;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    private bool CanAttack() => !gunData.reloading && timeSinceLastAttack > 1f / (gunData.fireRate / 60f);

    private void Attack()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanAttack())
            {
                if (Physics.Raycast(eyes.position, eyes.forward, out RaycastHit hitInfo, gunData.range))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.Damage(gunData.damage);
                }

                gunData.currentAmmo--;
                timeSinceLastAttack = 0;
            }
        }
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (gunData.currentAmmo == 0)
        {
            StartReload();
        }

        Debug.DrawRay(eyes.position, eyes.forward);
    }
}
