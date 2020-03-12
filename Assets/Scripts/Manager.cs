using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviourPun
{
    [SerializeField] private GameObject Player = null;
    [SerializeField] private Transform[] SpawnPositionsForPlayer = null;

    [SerializeField] private GameObject[] Boxes = null;
    [SerializeField] private Transform[] SpawnPositionsForBoxes = null;

    [SerializeField] private GameObject[] Weapons = null;
    [SerializeField] private Transform[] SpawnPositionsForWeapons = null;

    [SerializeField] private int CountLife = 10;
    GameObject MyPlayer;
    ApplyDamage PlayerHealth;

    private void Start()
    {
        Spawn();
    }
    private void Update()
    {
        if (!IsInvoking(nameof(GetRandomPositionAndBox)))
            Invoke(nameof(GetRandomPositionAndBox), 15);

        if (!IsInvoking(nameof(SpawnWeapons)))
            Invoke(nameof(SpawnWeapons), 15);
        try
        {
            PlayerHealth = MyPlayer.GetComponent<ApplyDamage>();
        }
        catch
        {
        }


        if(CountLife == 0)
        {
            PhotonNetwork.Destroy(MyPlayer);
        }


        if (PlayerHealth.isDied)
        {
            CountLife -= 1;
            print(CountLife);
            PhotonNetwork.Destroy(MyPlayer);
            Spawn();
            PlayerHealth.isDied = false;
        }

    }

    public void Spawn()
    {
        var index = UnityEngine.Random.Range(0, SpawnPositionsForPlayer.Length);
        MyPlayer = PhotonNetwork.Instantiate(Player.name, SpawnPositionsForPlayer[index].position, SpawnPositionsForPlayer[index].rotation);
        print($"You are spawned in {index} spawn");
    }
    private void GetRandomPositionAndBox()
    {
        int[] spawnpositions = new int[20];
        int[] numbersForSpawns = new int[20];
        for (int i = 0; i < 20; i++)
        {
            spawnpositions[i] = UnityEngine.Random.Range(0, SpawnPositionsForBoxes.Length);
            numbersForSpawns[i] = UnityEngine.Random.Range(0, Boxes.Length);
        }
        int[] indexBox = numbersForSpawns.Distinct().ToArray();
        int[] indexPosition = spawnpositions.Distinct().ToArray();

        for (int i = 0; i < Boxes.Length; i++)
        {
            photonView.RPC(nameof(SpawnBoxes), RpcTarget.All, indexBox[i], indexPosition[i]);
        }
    }

    private void SpawnWeapons()
    {
        int[] spawnpositions = new int[20];
        int[] WeaponsIndexes = new int[20];
        for (int i = 0; i < 20; i++)
        {
            spawnpositions[i] = UnityEngine.Random.Range(0, SpawnPositionsForWeapons.Length);
            WeaponsIndexes[i] = UnityEngine.Random.Range(0, Weapons.Length);
        }

        int[] indexSpawn = spawnpositions.Distinct().ToArray();
        int[] indexWeapon = WeaponsIndexes.Distinct().ToArray();
        for (int i = 0; i < Weapons.Length; i++)
        {
            photonView.RPC(nameof(SpawnWeapons1), RpcTarget.All, indexSpawn[i], indexWeapon[i]);
        }

    }
    [PunRPC]
    private void SpawnWeapons1(int indexSpawn, int indexWeapon)
    {
        Weapons[indexWeapon].transform.position = SpawnPositionsForWeapons[indexSpawn].position;
        Weapons[indexWeapon].SetActive(true);
    }

    [PunRPC]
    private void SpawnBoxes(int indexBox,int indexPosition)
    {
        Boxes[indexBox].transform.position = SpawnPositionsForBoxes[indexPosition].position;
        Boxes[indexBox].SetActive(true);
        print($"{Boxes[indexBox].name} has spawned");
    }
}
