using Unity.Physics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMoveController : MonoBehaviour {
    public float moveSpeed = 5;
    public float runSpeed = 5;
    public float jumpForce = 5;
    public float lookRotationDampFactor = 10;
    public bool uncontrolledFall;
    private State state = State.Moving;
    private PlayerInput playerInput;
    private CharacterController controller;
    private Transform ourCamera;
    public Transform respawnAnchor;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction runAction;
    private InputAction respawnAction;
    private bool runTriggered;
    private bool jumpTriggered;
    private Vector3 moveVelocity;
    private Vector2 moveInput;
    private Vector2 lookInput;

    enum State {
        Jumping,
        Falling,
        Moving,
    }

    void Awake() {
        ourCamera = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        playerInput = FindFirstObjectByType<PlayerInput>();
        moveAction = playerInput.currentActionMap.FindAction("Move");
        lookAction = playerInput.currentActionMap.FindAction("Look");
        jumpAction = playerInput.currentActionMap.FindAction("Jump");
        runAction = playerInput.currentActionMap.FindAction("Run");
        respawnAction = playerInput.currentActionMap.FindAction("Respawn");

        if (uncontrolledFall) {
            controller.enabled = false;
            Rigidbody body = gameObject.AddComponent<Rigidbody>();
            body.AddForce(Random.onUnitSphere * 100);
        }
    }

    void OnEnable() {
        playerInput.onActionTriggered += HandleAction;
    }

    void OnDisable() {
        playerInput.onActionTriggered -= HandleAction;
    }

    // void OnControllerColliderHit(ControllerColliderHit hit) {
    //     DoorPusherTag tag = hit.gameObject.GetComponent<DoorPusherTag>();
    //     if (!tag) {
    //         return;
    //     }

    //     controller.enabled = false;
    //     Rigidbody body = gameObject.AddComponent<Rigidbody>();
    //     body.AddForce(hit.normal * tag.force);
    // }

    private void HandleAction(InputAction.CallbackContext context) {
        if (context.action == moveAction) {
            moveInput = context.ReadValue<Vector2>();
            // Debug.Log($"move ${moveInput}");
        }
        if (context.action == lookAction) {
            lookInput = context.ReadValue<Vector2>();
        }
        if (context.action == jumpAction && context.performed) {
            jumpTriggered = true;
        }
        if (context.action == runAction) {
            runTriggered = context.ReadValue<float>() > 0;
        }
        if (context.action == respawnAction && context.performed) {
            Destroy(gameObject.GetComponent<Rigidbody>());
            controller.enabled = false;
            transform.position = respawnAnchor.position;
            moveVelocity.y = 0;
            controller.enabled = true;
        }
    }

    private void CalculateMoveDirection() {
        Vector3 cameraForward = new(ourCamera.forward.x, 0, ourCamera.forward.z);
        Vector3 cameraRight = new(ourCamera.right.x, 0, ourCamera.right.z);

        Vector3 moveDirection = cameraForward.normalized * moveInput.y + cameraRight.normalized * moveInput.x;

        float speed = runTriggered ? runSpeed : moveSpeed;
        moveVelocity.x = moveDirection.x * speed;
        moveVelocity.z = moveDirection.z * speed;
    }

    private void FaceMoveDirection() {

        Vector3 faceDirection = new(moveVelocity.x, 0f, moveVelocity.z);

        if (faceDirection == Vector3.zero) {
            return;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(faceDirection), lookRotationDampFactor * Time.deltaTime);
    }

    private void ApplyGravity() {
        if (moveVelocity.y > Physics.gravity.y) {
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    private void Move() {
        controller.Move(moveVelocity * Time.deltaTime);
    }

    private void HandleJumping() {
        // Should we wait for velocity to become negative here?
        if (!controller.isGrounded) {
            state = State.Falling;
        }

        CalculateMoveDirection();
        FaceMoveDirection();
        ApplyGravity();
        Move();
    }

    private void HandleFalling() {
        if (controller.isGrounded) {
            // Clear any jump queried while jumping (could check in event handler above though)
            jumpTriggered = false;
            state = State.Moving;
        }

        CalculateMoveDirection();
        FaceMoveDirection();
        ApplyGravity();
        Move();
    }

    private void HandleMoving() {
        if (jumpTriggered) {
            moveVelocity.Set(moveVelocity.x, jumpForce, moveVelocity.z);
            state = State.Jumping;
            jumpTriggered = false;
        }

        CalculateMoveDirection();
        FaceMoveDirection();
        ApplyGravity();
        Move();
    }

    void Update() {
        if (state == State.Jumping) {
            HandleJumping();
        }
        if (state == State.Falling) {
            HandleFalling();
        }
        if (state == State.Moving) {
            HandleMoving();
        }
    }
}
