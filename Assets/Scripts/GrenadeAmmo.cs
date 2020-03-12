using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAmmo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Character character))
        {
            if (character.GetComponentInChildren<GrenadeGun>().gameObject.activeSelf == true)
            {
                character.GetComponentInChildren<GrenadeGun>().magazine += 20;
                gameObject.SetActive(false);
            }
        }
    }
}
