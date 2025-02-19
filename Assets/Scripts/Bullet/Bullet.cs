using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 5f;
    private Photon.Realtime.Player owner;

    public void Initialize(float bulletSpeed, Photon.Realtime.Player bulletOwner)
    {
        speed = bulletSpeed;
        owner = bulletOwner;
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            Invoke("DestroyBullet", 1f);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (!other.CompareTag("Player"))
            {
                DestroyBullet();
            }
        }
    }

    void DestroyBullet()
    {
        // Solo el dueño del PhotonView ejecuta esta logica
        if (photonView.IsMine)
        {
            //Destuye la bala en la red
            PhotonNetwork.Destroy(gameObject);
        }
    }

}
