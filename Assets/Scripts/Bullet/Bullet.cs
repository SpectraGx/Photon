using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float lifeTime;
    private Photon.Realtime.Player owner;

    public void Initialize(float bulletSpeed, Photon.Realtime.Player owner)
    {
        this.bulletSpeed = bulletSpeed;
        this.owner = owner;
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }

    }

}
