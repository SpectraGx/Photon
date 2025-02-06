using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] Camera mainCamera;

    [SerializeField] float pSpeed = 1;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject aimPoint;

    void Awake()
    {
        mainCamera = FindAnyObjectByType<Camera>();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");


        Vector3 dir = new Vector3(x, 0f, z);
        if(dir.magnitude > 0)
        {
            dir.Normalize();
            controller.Move(dir * pSpeed * Time.deltaTime);
        }

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
        
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire");
            Instantiate(bullet, aimPoint.transform.position, transform.rotation);
        }
        //aimPoint.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rSpeed);
    }
}
