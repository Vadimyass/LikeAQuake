using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAmmo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            if(character.GetComponentInChildren<Bow>().gameObject.activeSelf == true)
            {
                character.GetComponentInChildren<Bow>().magazine += 20;
                gameObject.SetActive(false);
            }
        }
    }
}
