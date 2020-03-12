using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedGrenadeGun : SpawnedBow
{
    void Awake()
    {
        FullAmmo = 2;
        FullMagazine = 48;
    }
}
