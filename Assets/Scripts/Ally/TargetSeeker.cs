using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSeeker : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15;
    [SerializeField] ParticleSystem projectile;
    Transform target;

    void Update()
    {
        FindCloseTarget();
        AimWeapon();
    }

    private void FindCloseTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closeTarget= null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance< maxDistance )
            {
                closeTarget = enemy.transform;
                maxDistance = targetDistance;
            }

            target= closeTarget;
        }
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);

        if (targetDistance < range)
        {
            ManageProjectile(true);
        }
        else
        {
            ManageProjectile(false);
        }
    }

    void ManageProjectile(bool allow)
    {
        var projectileEmission = projectile.emission;
        projectileEmission.enabled = allow;
    }
}
