using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float crouchSpeed = 2.5f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;

    public float standingHeight = 1.7f;
    public float crouchHeight = 1.0f;
    public float crouchTransitionSpeed = 5f;

    private float xRotation = 0f;
    private bool isCrouching = false;

    private CharacterController controller;
    private Vector3 velocity;

    public AK47Shoot gunScript;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 如果设置面板打开或游戏暂停，则不响应输入
        if ((GameManager.Instance != null && GameManager.Instance.isSettingsOpen) || Time.timeScale == 0)
        {
            // 禁止射击
            if (gunScript != null)
                gunScript.canShoot = false;
            return;
        }

        // 下蹲检测
        isCrouching = Input.GetKey(KeyCode.LeftControl);

        // 摄像机高度平滑
        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        Vector3 camPos = playerCamera.localPosition;
        camPos.y = Mathf.Lerp(camPos.y, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        playerCamera.localPosition = camPos;

        // 视角旋转
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // 移动
        float speed = isCrouching ? crouchSpeed : walkSpeed;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (controller.isGrounded)
            velocity.y = -1f;
        else
            velocity += Physics.gravity * Time.deltaTime;

        controller.Move((move * speed + velocity) * Time.deltaTime);

        // 控制开火许可：唯一赋值处
        if (gunScript != null)
        {
            gunScript.canShoot = !isCrouching || (isCrouching && Input.GetMouseButton(0));
        }
    }
}

//using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
//public class PlayerController : MonoBehaviour
//{
//    public float walkSpeed = 5f;
//    public float crouchSpeed = 2.5f;
//    public float mouseSensitivity = 2f;
//    public Transform playerCamera;

//    public float standingHeight = 1.7f;
//    public float crouchHeight = 1.0f;
//    public float crouchTransitionSpeed = 5f;

//    private float xRotation = 0f;
//    private bool isCrouching = false;

//    private CharacterController controller;
//    private Vector3 velocity;

//    // 枪械脚本引用
//    public AK47Shoot gunScript;

//    void Start()
//    {
//        controller = GetComponent<CharacterController>();
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    void Update()
//    {
//        // 检测是否按下 LeftControl 实现长按下蹲
//        isCrouching = Input.GetKey(KeyCode.LeftControl);

//        // 摄像机高度平滑过渡
//        float targetHeight = isCrouching ? crouchHeight : standingHeight;
//        Vector3 camPos = playerCamera.localPosition;
//        camPos.y = Mathf.Lerp(camPos.y, targetHeight, Time.deltaTime * crouchTransitionSpeed);
//        playerCamera.localPosition = camPos;

//        // 鼠标视角控制
//        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
//        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

//        xRotation -= mouseY;
//        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

//        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
//        transform.Rotate(Vector3.up * mouseX);

//        // 根据状态设置移动速度
//        float speed = isCrouching ? crouchSpeed : walkSpeed;
//        float moveX = Input.GetAxis("Horizontal");
//        float moveZ = Input.GetAxis("Vertical");
//        Vector3 move = transform.right * moveX + transform.forward * moveZ;

//        if (controller.isGrounded)
//            velocity.y = -1f; // 确保粘地
//        else
//            velocity += Physics.gravity * Time.deltaTime;

//        controller.Move((move * speed + velocity) * Time.deltaTime);

//        // 开火许可设置，避免下蹲干扰
//        if (gunScript != null)
//        {
//            gunScript.canShoot = !isCrouching || (isCrouching && Input.GetMouseButton(0));
//        }
//    }
//}



//using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
//public class PlayerController : MonoBehaviour
//{
//    public float walkSpeed = 5f;
//    public float crouchSpeed = 2.5f;
//    public float mouseSensitivity = 2f;
//    public Transform playerCamera;

//    public float standingHeight = 1.7f;
//    public float crouchHeight = 1.0f;
//    public float crouchTransitionSpeed = 5f;

//    private float xRotation = 0f;
//    private bool isCrouching = false;

//    private CharacterController controller;
//    private Vector3 velocity;

//    // 新增：枪械脚本引用，Inspector赋值
//    public AK47Shoot gunScript;

//    void Start()
//    {
//        controller = GetComponent<CharacterController>();
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    void Update()
//    {
//        // 下蹲检测
//        isCrouching = Input.GetKey(KeyCode.LeftControl);

//        // 摄像机高度平滑变化
//        float targetHeight = isCrouching ? crouchHeight : standingHeight;
//        Vector3 camPos = playerCamera.localPosition;
//        camPos.y = Mathf.Lerp(camPos.y, targetHeight, Time.deltaTime * crouchTransitionSpeed);
//        playerCamera.localPosition = camPos;

//        // 鼠标控制视角
//        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
//        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

//        xRotation -= mouseY;
//        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

//        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
//        transform.Rotate(Vector3.up * mouseX);

//        // 移动速度控制
//        float speed = isCrouching ? crouchSpeed : walkSpeed;
//        float moveX = Input.GetAxis("Horizontal");
//        float moveZ = Input.GetAxis("Vertical");
//        Vector3 move = transform.right * moveX + transform.forward * moveZ;

//        if (controller.isGrounded)
//            velocity.y = 0f;
//        else
//            velocity += Physics.gravity * Time.deltaTime;

//        controller.Move((move * speed + velocity) * Time.deltaTime);

//        // **保证开枪脚本可以开火（这里没限制下蹲）**
//        if (gunScript != null)
//        {
//            gunScript.canShoot = true;
//        }
//    }
//}
