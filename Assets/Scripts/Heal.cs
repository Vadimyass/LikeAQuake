using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour
{
    private float health;
    ApplyDamage damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            damage = character.GetComponent<ApplyDamage>();
            if(damage.health<=damage.startingHealth-20)
                damage.health += 20;
            gameObject.SetActive(false);
        }

    }
}
