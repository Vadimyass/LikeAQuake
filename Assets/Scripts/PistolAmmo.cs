using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            if (character.GetComponentInChildren<WeaponHit>().gameObject.activeSelf == true)
            {
                character.GetComponentInChildren<WeaponHit>().magazine += 20;
                gameObject.SetActive(false);
            }
        }
    }
}
