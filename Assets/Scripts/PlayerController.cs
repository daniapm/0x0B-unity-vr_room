using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveH;
    [SerializeField] private float MoveV;
    [SerializeField] private float Speed = 6.0f;
    [SerializeField] private float gravity = 40.0f;
    [SerializeField] private float fall;
    [SerializeField] private float jump = 10.0f;

    public CharacterController Player;
    public Camera mainCamera;
    private Vector3 movePlayer;
    private Vector3 playerInput;
    private Vector3 camForward;
    private Vector3 camRight;
    void Start()
    {
        Player = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        MoveH = Input.GetAxis("Horizontal");
        MoveV = Input.GetAxis("Vertical");

        playerInput = new Vector3(MoveH, 0, MoveV);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * Speed;

        Player.transform.LookAt(Player.transform.position + movePlayer);

        setGravity();

        PlayerSkills();

        Player.Move(movePlayer * Time.deltaTime);

        if (transform.position.y < -40)
            transform.position = new Vector3(0, 60, 0);
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void PlayerSkills()
    {
        if (Player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fall = jump;
            movePlayer.y = fall;
        }
    }

    void setGravity()
    {
        if (Player.isGrounded)
        {
            fall = -gravity * Time.deltaTime;
            movePlayer.y = fall;
        }
        else
        {
            fall -= gravity * Time.deltaTime;
            movePlayer.y = fall;
        }
    }
}