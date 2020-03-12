using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyDamage : MonoBehaviourPun
{
    [NonSerialized]public float startingHealth = 100;
    [NonSerialized] public float health;

    private float _currentHealth { get { return health; } set { health = value; } }

    public event Action<float> OnHealthChanged = delegate { };

    public bool isDied;

     private void Awake()
    {
        isDied = false;
    }
    private void OnEnable()
    {
        _currentHealth = startingHealth;
    }

    public void TakeDamage(float _damageAmount)
    {
        _currentHealth -= _damageAmount;
        print(health);

        if (_currentHealth <= 0)
        {
            isDied = true;
        }
    }
    private void Update()
    {
        OnHealthChanged(_currentHealth / startingHealth);
    }


}
