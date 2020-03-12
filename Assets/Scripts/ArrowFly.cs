using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFly : MonoBehaviour
{
    [SerializeField] public float _speed;
    [NonSerialized]public Rigidbody rb;
    [SerializeField] public float _damage;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = transform.forward * _speed * Bow._tension * 1.5f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ApplyDamage>(out ApplyDamage enemy))
        {
            enemy.TakeDamage(30);
        }
        rb.isKinematic = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }
}
