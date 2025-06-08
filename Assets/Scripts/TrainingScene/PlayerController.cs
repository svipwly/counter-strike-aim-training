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
        // ����������򿪻���Ϸ��ͣ������Ӧ����
        if ((GameManager.Instance != null && GameManager.Instance.isSettingsOpen) || Time.timeScale == 0)
        {
            // ��ֹ���
            if (gunScript != null)
                gunScript.canShoot = false;
            return;
        }

        // �¶׼��
        isCrouching = Input.GetKey(KeyCode.LeftControl);

        // ������߶�ƽ��
        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        Vector3 camPos = playerCamera.localPosition;
        camPos.y = Mathf.Lerp(camPos.y, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        playerCamera.localPosition = camPos;

        // �ӽ���ת
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // �ƶ�
        float speed = isCrouching ? crouchSpeed : walkSpeed;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (controller.isGrounded)
            velocity.y = -1f;
        else
            velocity += Physics.gravity * Time.deltaTime;

        controller.Move((move * speed + velocity) * Time.deltaTime);

        // ���ƿ�����ɣ�Ψһ��ֵ��
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

//    // ǹе�ű�����
//    public AK47Shoot gunScript;

//    void Start()
//    {
//        controller = GetComponent<CharacterController>();
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    void Update()
//    {
//        // ����Ƿ��� LeftControl ʵ�ֳ����¶�
//        isCrouching = Input.GetKey(KeyCode.LeftControl);

//        // ������߶�ƽ������
//        float targetHeight = isCrouching ? crouchHeight : standingHeight;
//        Vector3 camPos = playerCamera.localPosition;
//        camPos.y = Mathf.Lerp(camPos.y, targetHeight, Time.deltaTime * crouchTransitionSpeed);
//        playerCamera.localPosition = camPos;

//        // ����ӽǿ���
//        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
//        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

//        xRotation -= mouseY;
//        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

//        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
//        transform.Rotate(Vector3.up * mouseX);

//        // ����״̬�����ƶ��ٶ�
//        float speed = isCrouching ? crouchSpeed : walkSpeed;
//        float moveX = Input.GetAxis("Horizontal");
//        float moveZ = Input.GetAxis("Vertical");
//        Vector3 move = transform.right * moveX + transform.forward * moveZ;

//        if (controller.isGrounded)
//            velocity.y = -1f; // ȷ��ճ��
//        else
//            velocity += Physics.gravity * Time.deltaTime;

//        controller.Move((move * speed + velocity) * Time.deltaTime);

//        // ����������ã������¶׸���
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

//    // ������ǹе�ű����ã�Inspector��ֵ
//    public AK47Shoot gunScript;

//    void Start()
//    {
//        controller = GetComponent<CharacterController>();
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    void Update()
//    {
//        // �¶׼��
//        isCrouching = Input.GetKey(KeyCode.LeftControl);

//        // ������߶�ƽ���仯
//        float targetHeight = isCrouching ? crouchHeight : standingHeight;
//        Vector3 camPos = playerCamera.localPosition;
//        camPos.y = Mathf.Lerp(camPos.y, targetHeight, Time.deltaTime * crouchTransitionSpeed);
//        playerCamera.localPosition = camPos;

//        // �������ӽ�
//        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
//        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

//        xRotation -= mouseY;
//        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

//        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
//        transform.Rotate(Vector3.up * mouseX);

//        // �ƶ��ٶȿ���
//        float speed = isCrouching ? crouchSpeed : walkSpeed;
//        float moveX = Input.GetAxis("Horizontal");
//        float moveZ = Input.GetAxis("Vertical");
//        Vector3 move = transform.right * moveX + transform.forward * moveZ;

//        if (controller.isGrounded)
//            velocity.y = 0f;
//        else
//            velocity += Physics.gravity * Time.deltaTime;

//        controller.Move((move * speed + velocity) * Time.deltaTime);

//        // **��֤��ǹ�ű����Կ�������û�����¶ף�**
//        if (gunScript != null)
//        {
//            gunScript.canShoot = true;
//        }
//    }
//}
