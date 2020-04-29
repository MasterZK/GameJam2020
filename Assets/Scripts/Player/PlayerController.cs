using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool sprinting;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Unity Components")]
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    private Rigidbody playerRb;
    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        playerRb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SprintCheck();
        SprintCamera();
        
    }

    private void FixedUpdate()
    {
        InitMovement();
        Jump();
    }

    #region Jump
    private void Jump()
    {
        if (!isGrounded)
            return;

        if (Input.GetKey(KeyCode.Space))
        {
            playerRb.velocity += Vector3.up * jumpForce;
        }

        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (playerRb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("floor"))
            isGrounded = true;
    }
    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
    #endregion

    #region Sprint
    private void SprintCheck()
    {
        sprinting = false;
        if (Input.GetKey(KeyCode.LeftShift))
            sprinting = true;
    }
    private void SprintCamera()
    {
        if (sprinting)
            playerCamera.m_Lens.FieldOfView = 80;
        else
            playerCamera.m_Lens.FieldOfView = 70;
    }
    #endregion

    private void InitMovement()
    {
        if (!canMove)
            return;

        MoveForward();
        MoveBackwards();
        MoveLeft();
        MoveRight();
    }

    #region movement
    private void Move(Vector3 direction)
    {
        if (!isGrounded)
            direction = direction / 2;
        playerRb.MovePosition(this.transform.position + (direction * Time.deltaTime * speed)); 
    }
    private Vector3 CalDirection(Vector3 moveDirection)
    {
        var cameraDirection = playerCamera.transform.eulerAngles.y;
        var finalMoveDirection = Quaternion.Euler(0, cameraDirection, 0) * moveDirection;
        return finalMoveDirection;
    }
    private void MoveForward()
    {
        if (!Input.GetKey(KeyCode.W))
            return;

        var vector = Vector3.forward;
        if (sprinting)
            vector *= sprintMultiplier;

        Move(CalDirection(vector));
    }
    private void MoveBackwards()
    {
        if (!Input.GetKey(KeyCode.S))
            return;

        Move(CalDirection(Vector3.back));
    }
    private void MoveRight()
    {
        if (!Input.GetKey(KeyCode.D))
            return;

        Move(CalDirection(Vector3.right));
    }
    private void MoveLeft()
    {
        if (!Input.GetKey(KeyCode.A))
            return;

        Move(CalDirection(Vector3.left));
    }
    #endregion

}
