using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponHit
{
    [SerializeField] private GameObject Arrow = null;
    private Animator animator;

    public static float _tension;
    private static float _bowtension
    {
        set
        {
            if (value <= 2)
            {
                _tension = value;
            }
            else
            {
                value = 2;
            }
        }
        get { return _tension; }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        FullAmmo = 3;
        FullMagazine = 15;
        magazine = FullMagazine;
        ammo = FullAmmo;
    }
    void Update()
    {
        if (!photonView.IsMine) return;
        AmmoText.text = $"{ammo}/{magazine}";
        if (Input.GetKey(KeyCode.Mouse0) & ammo > 0 )
        {
            _bowtension += Time.deltaTime;
            animator.SetFloat("Tension", _bowtension);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) & ammo > 0 )
        {
            ammo -= 1;
            ShootBow();
        }
        if (Input.GetKeyDown(KeyCode.R) & magazine > FullAmmo)
        {
            magazine -= FullAmmo - ammo;
            ammo = FullAmmo;

        }
        if (gameObject.activeSelf == false)
        {
            magazine = FullMagazine;
        }
    }

    [PunRPC]
    private void ShootBow()
    {
        animator.SetFloat("Tension", 0);
        PhotonNetwork.Instantiate(Arrow.name, FirePoint.position, FirePoint.rotation);
        _bowtension = 0;
    }

}
