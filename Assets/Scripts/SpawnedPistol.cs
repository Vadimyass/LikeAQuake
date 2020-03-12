using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedPistol : SpawnedBow
{
    void Awake()
    {
        FullAmmo = 5;
        FullMagazine = 60;
    }
}
