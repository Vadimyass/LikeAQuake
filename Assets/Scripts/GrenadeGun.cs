using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeGun : WeaponHit
{
    [SerializeField] private GameObject bulletPref = null;
    private PhotonView pv;

    private void Start()
    {
        FullAmmo = 2;
        FullMagazine = 24;
        magazine = FullMagazine;
        ammo = FullAmmo;
        try
        {
            AmmoText.text = $"{ammo}/{magazine}";
        }
        catch
        {
        }
           
    }
    void Update()
    {
        if (!photonView.IsMine) return;
        AmmoText.text = $"{ammo}/{magazine}";
        if (Input.GetKeyDown(KeyCode.Mouse0) & ammo > 0)
        {
            photonView.RPC(nameof(Shoot),RpcTarget.All);
            ammo -= 1;;
        }
        if (Input.GetKeyDown(KeyCode.R) & magazine >= FullAmmo)
        {
            magazine -= FullAmmo - ammo;
            ammo = FullAmmo;
        }
        if (gameObject.activeSelf == false)
        {
            FullAmmo = 5;
            FullMagazine = 30;
        }
    }


    [PunRPC]
    private void Shoot()
    {
        Instantiate(bulletPref, FirePoint.position, FirePoint.rotation);
    }
}
