using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterShooting : MonoBehaviourPun
{
    [Header("Settings: Shoot")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform aimPoint;
    [SerializeField] float bSpeed;

    void Update()
    {
        if (photonView.IsMine && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire");
            photonView.RPC("Shoot", RpcTarget.All);
        }
    }

    [PunRPC]

    void Shoot()
    {
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", aimPoint.position, aimPoint.rotation);
        //bullet.GetComponent<Bullet>().photonView.TransferOwnership(photonView.Owner);
        bullet.GetComponent<Bullet>().Initialize(bSpeed, photonView.Owner);
    }
}
