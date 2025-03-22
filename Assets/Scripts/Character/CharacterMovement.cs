using Photon.Pun;
using UnityEngine;

public class CharacterMovement : MonoBehaviourPun
{
    CharacterController controller;

    [SerializeField] float pSpeed = 1;
    [SerializeField] bool isGrounded = true;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckDistance = 2;
    [SerializeField] Animator animator;
    [SerializeField] string MoveAnim;
    [SerializeField] string IdleAnim;
    public Vector3 dir;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        CheckGround();
        HandleMovement();

    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y;
        float z = Input.GetAxisRaw("Vertical");

        if (!isGrounded)
        {
            y = -1;
        }
        else
        {
            y = 0;
        }


        dir = new Vector3(x, y, z);
        if (dir.magnitude != 0)
        {
            dir.Normalize();
            controller.Move(dir * pSpeed * Time.deltaTime);
            animator.Play(MoveAnim);
        }
        else
        {
            animator.Play(IdleAnim);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
