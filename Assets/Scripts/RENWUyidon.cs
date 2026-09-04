using UnityEngine;

public class FirstPersonMove : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;

    private CharacterController controller;
    private Transform cam;
    private float xRotation = 0f;
    private Vector3 velocity;
    private float gravity = -9.81f;
    public string text;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked;
        //Debug.Log(renwuguanli.instance.Finish(text));
    }

    void Update()
    {
        // 移动
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (cam.right * x + cam.forward * z);
        move.y = 0;
        controller.Move(move.normalized * speed * Time.deltaTime);

        // 视角
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // 跳跃 + 重力
        if (controller.isGrounded && velocity.y < 0) velocity.y = -2f;
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * 2f * 2f);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}