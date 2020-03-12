using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHit : MonoBehaviourPun
{
    [SerializeField] public Transform FirePoint;
    [SerializeField] public Text AmmoText;

    [NonSerialized] public int FullAmmo;
    [NonSerialized] public int FullMagazine;
    [NonSerialized] public int magazine;
    [NonSerialized] public int ammo;


    [SerializeField]
    [Range(0.5f, 1.5f)]
    private float _fireRate = 1;


    [NonSerialized]private  float _timer;
    private void Start()
    {
        FullAmmo = 5;
        FullMagazine = 30;
        ammo = FullAmmo;
        magazine = FullMagazine;
        try
        {
            AmmoText.text = $"{ammo}/{magazine}";
        }
        catch
        {
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        AmmoText.text = $"{ammo}/{magazine}";
        _timer += Time.deltaTime;
        if (_timer >= _fireRate)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) & ammo > 0)
            {
                photonView.RPC("FireGun", RpcTarget.All);
                _timer = 0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) & magazine > FullAmmo)
        {
            magazine -= FullAmmo - ammo;
            ammo = FullAmmo;
        }
        if(gameObject.activeSelf == false)
        {
            FullAmmo = 5;
            FullMagazine = 30;
        }
    }

    [PunRPC]
    public void FireGun()
    {
        RaycastHit hit;
        if (Physics.Raycast(FirePoint.position, transform.forward, out hit, 50))
        {
            hit.collider.GetComponent<ApplyDamage>()?.TakeDamage(10);
        }
        ammo--;
    }
}
