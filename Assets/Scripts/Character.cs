using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    [SerializeField]private Camera cam;
    private Canvas Canvas;
    private WeaponRandomizer weapons;
    float gravity = 10;
    float gravityForce;
    public int Weapon;

    Vector3 moveDir = Vector3.zero;
    Vector3 moveVector;


    CharacterController controller;

    private float MovementSpeed = 20;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Canvas = GetComponentInChildren<Canvas>();
        weapons = GetComponentInChildren<WeaponRandomizer>();
        TurnOnTheCameraAndUI();
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        GetInput();
        GamingGravity();
    }

    private void GetInput()  //Getting input data
    {
        if (controller.isGrounded)
        {
            CharacterMove();
        }
        else
        {
            Movement();
            gravityForce = -1f;
        }

    }


    private void CharacterMove()
    {
        moveVector.z = Input.GetAxis("Vertical")* MovementSpeed;
        moveVector.x = Input.GetAxis("Horizontal")* MovementSpeed;
        moveVector.y = gravityForce * Time.deltaTime;

        Vector3 forwardMovement = transform.forward * moveVector.z;
        Vector3 rightMovement = transform.right * moveVector.x;

        moveVector.y -= gravity * Time.deltaTime;
        controller.SimpleMove(forwardMovement + rightMovement);

    }

    private void Movement()
    {
        moveDir.y = moveDir.y - gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
        transform.TransformDirection(moveDir);
    }

    private void GamingGravity()
    {
        gravityForce -= 20f * Time.deltaTime;
    }

    private void TurnOnTheCameraAndUI()
    {
        cam.enabled = photonView.IsMine;
        Canvas.enabled = photonView.IsMine;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SpawnedBow bow))
        {
            weapons.weapons[0].gameObject.SetActive(false);
            weapons.weapons[1].gameObject.SetActive(true);
            weapons.weapons[2].gameObject.SetActive(false);
            weapons.weapons[1].GetComponent<Bow>().ammo = bow.FullAmmo;
            weapons.weapons[1].GetComponent<Bow>().magazine = bow.FullMagazine;
            Weapon = 1;
            bow.gameObject.SetActive(false);
        }
        if (other.TryGetComponent(out SpawnedPistol pistol))
        {
            weapons.weapons[0].gameObject.SetActive(true);
            weapons.weapons[1].gameObject.SetActive(false);
            weapons.weapons[2].gameObject.SetActive(false);
            weapons.weapons[0].GetComponent<WeaponHit>().ammo = pistol.FullAmmo;
            weapons.weapons[0].GetComponent<WeaponHit>().magazine = pistol.FullMagazine;
            Weapon = 0;
            pistol.gameObject.SetActive(false);
        }
        if (other.TryGetComponent(out SpawnedGrenadeGun grenadeGun))
        { 
            weapons.weapons[0].gameObject.SetActive(false);
            weapons.weapons[1].gameObject.SetActive(false);
            weapons.weapons[2].gameObject.SetActive(true);
            weapons.weapons[2].GetComponent<GrenadeGun>().ammo = grenadeGun.FullAmmo;
            weapons.weapons[2].GetComponent<GrenadeGun>().magazine = grenadeGun.FullMagazine;
            grenadeGun.gameObject.SetActive(false);
            Weapon = 2;
        }


    }
}
