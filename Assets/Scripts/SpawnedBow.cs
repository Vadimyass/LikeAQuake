using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedBow : MonoBehaviour
{
    [NonSerialized] public int FullAmmo;
    [NonSerialized] public int FullMagazine;
    void Awake()
    {
        FullAmmo = 3;
        FullMagazine = 30;
    }
}
