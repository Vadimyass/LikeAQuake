using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : ArrowFly
{
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        ExplosionDamage(transform.position, 3f);
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        foreach (Collider collider in hitColliders)
            if (collider.TryGetComponent<ApplyDamage>(out ApplyDamage enemy))
            {
                enemy.TakeDamage(_damage);
            }

        rb.isKinematic = true;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject, 5f);
    }
}
