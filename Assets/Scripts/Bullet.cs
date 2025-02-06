using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;

    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        Destroy(gameObject, 3);
    }
}
