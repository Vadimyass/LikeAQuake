using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRandomizer : MonoBehaviourPun,IPunObservable
{
    [NonSerialized] public Transform[] weapons;

    PhotonView photonView1;
    Character selectedWeapon;
    private void Awake()
    {
        photonView1 = GetComponent<PhotonView>();
        selectedWeapon = FindObjectOfType<Character>();

        List<Transform> ListOfWeapons = new List<Transform>();

        foreach (Transform child in transform)
        {
            ListOfWeapons.Add(child);
        }

        weapons = ListOfWeapons.ToArray();

        int choice = UnityEngine.Random.Range(0,weapons.Length);

        selectedWeapon.Weapon = choice;
        SelectWeapon(selectedWeapon.Weapon);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(selectedWeapon.Weapon);
        }
        else if (stream.IsReading)
        {
            int SelectedWeapon = (int) stream.ReceiveNext();
            SelectOtherWeapon(SelectedWeapon);
        }
    }
    public void SelectWeapon(int choice)
    {
        if (photonView1.IsMine)
        {
            foreach (Transform weapon in weapons)
            {
                weapon.gameObject.SetActive(false);
            }
            weapons[choice].gameObject.SetActive(true);
        }
    }

    public void SelectOtherWeapon(int choice23)
    {
        foreach (Transform weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        weapons[choice23].gameObject.SetActive(true);
    }
}
