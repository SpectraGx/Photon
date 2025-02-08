using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterShooting : MonoBehaviourPun
{
    [Header("Settings: Shoot")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject aimPoint;

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Fire");
                //Instantiate(bullet, aimPoint.transform.position, transform.rotation);
                photonView.RPC("Shoot", RpcTarget.All);
            }
            //aimPoint.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rSpeed);
        }
    }

    [PunRPC]
    void Shoot()
    {
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", aimPoint.transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().photonView.TransferOwnership(photonView.Owner);
    }
}
