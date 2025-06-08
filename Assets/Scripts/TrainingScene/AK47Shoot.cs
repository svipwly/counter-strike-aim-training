using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class AK47Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform muzzlePoint;
    public float bulletSpeed = 985f;
    public float fireRate = 0.1f;

    public float recoilBackDistance = 0.15f;
    public float recoilSpring = 120f;
    public float recoilDamping = 25f;

    public AudioClip fireSound;
    private AudioSource audioSource;

    [Header("Mixer Group (Shoot)")]
    public AudioMixerGroup shootMixerGroup; // 拖入你的Shoot分组

    private float nextFireTime;
    private Vector3 originalLocalPosition;
    private Vector3 currentPosition;
    private Vector3 velocity;

    [HideInInspector]
    public bool canShoot = true;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
        currentPosition = originalLocalPosition;
        velocity = Vector3.zero;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // 设置Mixer分组
        if (shootMixerGroup != null)
            audioSource.outputAudioMixerGroup = shootMixerGroup;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        if (!canShoot) return;

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }

        Vector3 displacement = currentPosition - originalLocalPosition;
        Vector3 springForce = -recoilSpring * displacement;
        Vector3 dampingForce = -recoilDamping * velocity;

        Vector3 acceleration = springForce + dampingForce;
        velocity += acceleration * Time.deltaTime;
        currentPosition += velocity * Time.deltaTime;

        transform.localPosition = currentPosition;
    }

    void Shoot()
    {
        Vector3 shootDir;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            shootDir = (hit.point - muzzlePoint.position).normalized;
        else
            shootDir = ray.direction;

        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(shootDir));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = shootDir * bulletSpeed;

        if (muzzleFlashPrefab != null && muzzlePoint != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation);
            flash.transform.SetParent(muzzlePoint);
            Destroy(flash, 0.2f);
        }

        GameManager.Instance?.AddShot();

        if (fireSound != null && audioSource != null)
            audioSource.PlayOneShot(fireSound);

        Vector3 recoilDirection = -Camera.main.transform.forward;
        Vector3 localRecoil = transform.parent.InverseTransformDirection(recoilDirection);
        velocity += localRecoil * recoilBackDistance * 50f;
    }
}

//using UnityEngine;
//using UnityEngine.EventSystems;

//public class AK47Shoot : MonoBehaviour
//{
//    public GameObject bulletPrefab;
//    public GameObject muzzleFlashPrefab;
//    public Transform muzzlePoint;
//    public float bulletSpeed = 985f;
//    public float fireRate = 0.1f;

//    public float recoilBackDistance = 0.15f;
//    public float recoilSpring = 120f;
//    public float recoilDamping = 25f;

//    public AudioClip fireSound;
//    private AudioSource audioSource;

//    private float nextFireTime;
//    private Vector3 originalLocalPosition;
//    private Vector3 currentPosition;
//    private Vector3 velocity;

//    [HideInInspector]
//    public bool canShoot = true;

//    void Start()
//    {
//        originalLocalPosition = transform.localPosition;
//        currentPosition = originalLocalPosition;
//        velocity = Vector3.zero;

//        audioSource = GetComponent<AudioSource>();
//        if (audioSource == null)
//            audioSource = gameObject.AddComponent<AudioSource>();
//    }

//    void Update()
//    {
//        // 游戏暂停或不允许射击时直接返回
//        if (Time.timeScale == 0) return;
//        if (!canShoot) return;

//        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
//        {
//            if (!EventSystem.current.IsPointerOverGameObject())
//            {
//                Shoot();
//                nextFireTime = Time.time + fireRate;
//            }
//        }

//        // 后坐力回弹
//        Vector3 displacement = currentPosition - originalLocalPosition;
//        Vector3 springForce = -recoilSpring * displacement;
//        Vector3 dampingForce = -recoilDamping * velocity;

//        Vector3 acceleration = springForce + dampingForce;
//        velocity += acceleration * Time.deltaTime;
//        currentPosition += velocity * Time.deltaTime;

//        transform.localPosition = currentPosition;
//    }

//    void Shoot()
//    {
//        Vector3 shootDir;
//        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
//        RaycastHit hit;

//        if (Physics.Raycast(ray, out hit))
//            shootDir = (hit.point - muzzlePoint.position).normalized;
//        else
//            shootDir = ray.direction;

//        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(shootDir));
//        Rigidbody rb = bullet.GetComponent<Rigidbody>();
//        if (rb != null)
//            rb.velocity = shootDir * bulletSpeed;

//        if (muzzleFlashPrefab != null && muzzlePoint != null)
//        {
//            GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation);
//            flash.transform.SetParent(muzzlePoint);
//            Destroy(flash, 0.2f);
//        }

//        GameManager.Instance?.AddShot();

//        if (fireSound != null && audioSource != null)
//            audioSource.PlayOneShot(fireSound);

//        Vector3 recoilDirection = -Camera.main.transform.forward;
//        Vector3 localRecoil = transform.parent.InverseTransformDirection(recoilDirection);
//        velocity += localRecoil * recoilBackDistance * 50f;
//    }
//}



//using UnityEngine;
//using UnityEngine.EventSystems;

//public class AK47Shoot : MonoBehaviour
//{
//    public GameObject bulletPrefab;
//    public GameObject muzzleFlashPrefab; // 火焰特效预制体
//    public Transform muzzlePoint;
//    public float bulletSpeed = 985f;
//    public float fireRate = 0.1f;

//    public float recoilBackDistance = 0.15f;    // 每枪后坐力瞬间位移大小
//    public float recoilSpring = 120f;           // 弹簧刚度
//    public float recoilDamping = 25f;           // 阻尼

//    public AudioClip fireSound;                 //  开火音效
//    private AudioSource audioSource;            // 音频播放器组件

//    private float nextFireTime;
//    private Vector3 originalLocalPosition;
//    private Vector3 currentPosition;
//    private Vector3 velocity;

//    [HideInInspector]
//    public bool canShoot = true;

//    void Start()
//    {
//        originalLocalPosition = transform.localPosition;
//        currentPosition = originalLocalPosition;
//        velocity = Vector3.zero;

//        // 自动添加或获取 AudioSource 组件
//        audioSource = GetComponent<AudioSource>();
//        if (audioSource == null)
//            audioSource = gameObject.AddComponent<AudioSource>();
//    }

//    void Update()
//    {
//        // 新增判断：点击是否在UI上，避免点击按钮时射击
//        if (canShoot && Input.GetMouseButton(0) && Time.time >= nextFireTime)
//        {
//            if (!EventSystem.current.IsPointerOverGameObject())
//            {
//                Shoot();
//                nextFireTime = Time.time + fireRate;
//            }
//        }

//        // 弹簧阻尼模拟后坐力回弹
//        Vector3 displacement = currentPosition - originalLocalPosition;
//        Vector3 springForce = -recoilSpring * displacement;
//        Vector3 dampingForce = -recoilDamping * velocity;

//        Vector3 acceleration = springForce + dampingForce;
//        velocity += acceleration * Time.deltaTime;
//        currentPosition += velocity * Time.deltaTime;

//        transform.localPosition = currentPosition;
//    }

//    void Shoot()
//    {
//        Vector3 shootDir;
//        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
//        RaycastHit hit;

//        if (Physics.Raycast(ray, out hit))
//            shootDir = (hit.point - muzzlePoint.position).normalized;
//        else
//            shootDir = ray.direction;

//        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(shootDir));
//        Rigidbody rb = bullet.GetComponent<Rigidbody>();
//        if (rb != null)
//            rb.velocity = shootDir * bulletSpeed;

//        if (muzzleFlashPrefab != null && muzzlePoint != null)
//        {
//            Debug.Log("生成火焰特效");
//            GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation);
//            flash.transform.SetParent(muzzlePoint);  // 可选：跟随枪口方向
//            Destroy(flash, 0.2f);  // 自动销毁防止积累
//        }

//        GameManager.Instance?.AddShot();

//        // 播放开火音效
//        if (fireSound != null && audioSource != null)
//            audioSource.PlayOneShot(fireSound);

//        // 后坐力：摄像机方向转换为局部方向向后推
//        Vector3 recoilDirection = -Camera.main.transform.forward;
//        Vector3 localRecoil = transform.parent.InverseTransformDirection(recoilDirection);
//        velocity += localRecoil * recoilBackDistance * 50f;
//    }
//}


//using UnityEngine;
//using UnityEngine.EventSystems;

//public class AK47Shoot : MonoBehaviour
//{
//    public GameObject bulletPrefab;
//    public GameObject muzzleFlashPrefab; // 火焰特效预制体
//    public Transform muzzlePoint;
//    public float bulletSpeed = 985f;
//    public float fireRate = 0.1f;

//    public float recoilBackDistance = 0.15f;    // 每枪后坐力瞬间位移大小
//    public float recoilSpring = 120f;           // 弹簧刚度
//    public float recoilDamping = 25f;           // 阻尼

//    public AudioClip fireSound;                 //  开火音效
//    private AudioSource audioSource;            // 音频播放器组件

//    private float nextFireTime;
//    private Vector3 originalLocalPosition;
//    private Vector3 currentPosition;
//    private Vector3 velocity;

//    [HideInInspector]
//    public bool canShoot = true;


//    void Start()
//    {
//        originalLocalPosition = transform.localPosition;
//        currentPosition = originalLocalPosition;
//        velocity = Vector3.zero;

//        //  自动添加或获取 AudioSource 组件
//        audioSource = GetComponent<AudioSource>();
//        if (audioSource == null)
//            audioSource = gameObject.AddComponent<AudioSource>();
//    }

//    void Update()
//    {
//        // 修改点：只监听鼠标左键，避免 LeftControl 误触 Fire1
//        if (canShoot && Input.GetMouseButton(0) && Time.time >= nextFireTime)
//        {
//            Shoot();
//            nextFireTime = Time.time + fireRate;
//        }

//        // 弹簧阻尼模拟后坐力回弹
//        Vector3 displacement = currentPosition - originalLocalPosition;
//        Vector3 springForce = -recoilSpring * displacement;
//        Vector3 dampingForce = -recoilDamping * velocity;

//        Vector3 acceleration = springForce + dampingForce;
//        velocity += acceleration * Time.deltaTime;
//        currentPosition += velocity * Time.deltaTime;

//        transform.localPosition = currentPosition;



//    }

//    void Shoot()
//    {
//        Vector3 shootDir;
//        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
//        RaycastHit hit;

//        if (Physics.Raycast(ray, out hit))
//            shootDir = (hit.point - muzzlePoint.position).normalized;
//        else
//            shootDir = ray.direction;

//        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(shootDir));
//        Rigidbody rb = bullet.GetComponent<Rigidbody>();
//        if (rb != null)
//            rb.velocity = shootDir * bulletSpeed;

//        if (muzzleFlashPrefab != null && muzzlePoint != null)
//        {
//            print ("生成火焰特效");
//            GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation);
//            flash.transform.SetParent(muzzlePoint);  // 可选：跟随枪口方向
//            Destroy(flash, 0.2f);  // 自动销毁防止积累
//        }

//        GameManager.Instance?.AddShot();

//        //  播放开火音效
//        if (fireSound != null && audioSource != null)
//            audioSource.PlayOneShot(fireSound);

//        // 后坐力：摄像机方向转换为局部方向向后推
//        Vector3 recoilDirection = -Camera.main.transform.forward;
//        Vector3 localRecoil = transform.parent.InverseTransformDirection(recoilDirection);
//        velocity += localRecoil * recoilBackDistance * 50f;
//    }
//}



//using UnityEngine;

//public class AK47Shoot : MonoBehaviour
//{
//    public GameObject bulletPrefab;
//    public Transform muzzlePoint;
//    public float bulletSpeed = 50f;
//    public float fireRate = 0.1f;

//    public float recoilBackDistance = 0.15f;    // 每枪后坐力瞬间位移大小
//    public float recoilSpring = 120f;           // 弹簧刚度
//    public float recoilDamping = 25f;           // 阻尼

//    private float nextFireTime;

//    private Vector3 originalLocalPosition;
//    private Vector3 currentPosition;
//    private Vector3 velocity;

//    [HideInInspector]
//    public bool canShoot = true;

//    void Start()
//    {
//        originalLocalPosition = transform.localPosition;
//        currentPosition = originalLocalPosition;
//        velocity = Vector3.zero;
//    }

//    void Update()
//    {
//        // 修改点：只监听鼠标左键，避免 LeftControl 误触 Fire1
//        if (canShoot && Input.GetMouseButton(0) && Time.time >= nextFireTime)
//        {
//            Shoot();
//            nextFireTime = Time.time + fireRate;
//        }

//        // 弹簧阻尼模拟后坐力回弹
//        Vector3 displacement = currentPosition - originalLocalPosition;
//        Vector3 springForce = -recoilSpring * displacement;
//        Vector3 dampingForce = -recoilDamping * velocity;

//        Vector3 acceleration = springForce + dampingForce;
//        velocity += acceleration * Time.deltaTime;
//        currentPosition += velocity * Time.deltaTime;

//        transform.localPosition = currentPosition;
//    }

//    void Shoot()
//    {
//        Vector3 shootDir;
//        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
//        RaycastHit hit;

//        if (Physics.Raycast(ray, out hit))
//            shootDir = (hit.point - muzzlePoint.position).normalized;
//        else
//            shootDir = ray.direction;

//        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(shootDir));
//        Rigidbody rb = bullet.GetComponent<Rigidbody>();
//        if (rb != null)
//            rb.velocity = shootDir * bulletSpeed;

//        // 后坐力：摄像机方向转换为局部方向向后推
//        Vector3 recoilDirection = -Camera.main.transform.forward;
//        Vector3 localRecoil = transform.parent.InverseTransformDirection(recoilDirection);

//        velocity += localRecoil * recoilBackDistance * 50f;
//    }
//}



//using UnityEngine;

//public class AK47Shoot : MonoBehaviour
//{
//    public GameObject bulletPrefab;
//    public Transform muzzlePoint;
//    public float bulletSpeed = 50f;
//    public float fireRate = 0.1f;

//    public float recoilBackDistance = 0.15f;    // 每枪后坐力瞬间位移大小
//    public float recoilSpring = 120f;           // 弹簧刚度
//    public float recoilDamping = 25f;           // 阻尼

//    private float nextFireTime;

//    private Vector3 originalLocalPosition;
//    private Vector3 currentPosition;
//    private Vector3 velocity;

//    // 新增：是否允许开枪
//    [HideInInspector]
//    public bool canShoot = true;

//    void Start()
//    {
//        originalLocalPosition = transform.localPosition;
//        currentPosition = originalLocalPosition;
//        velocity = Vector3.zero;
//    }

//    void Update()
//    {
//        if (canShoot && Input.GetButton("Fire1") && Time.time >= nextFireTime)
//        {
//            Shoot();
//            nextFireTime = Time.time + fireRate;
//        }

//        // 弹簧阻尼运动计算，恢复到原位
//        Vector3 displacement = currentPosition - originalLocalPosition;
//        Vector3 springForce = -recoilSpring * displacement;
//        Vector3 dampingForce = -recoilDamping * velocity;

//        Vector3 acceleration = springForce + dampingForce;

//        velocity += acceleration * Time.deltaTime;
//        currentPosition += velocity * Time.deltaTime;

//        transform.localPosition = currentPosition;
//    }

//    void Shoot()
//    {
//        Vector3 shootDir;
//        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
//        RaycastHit hit;

//        if (Physics.Raycast(ray, out hit))
//            shootDir = (hit.point - muzzlePoint.position).normalized;
//        else
//            shootDir = ray.direction;

//        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(shootDir));
//        Rigidbody rb = bullet.GetComponent<Rigidbody>();
//        if (rb != null)
//            rb.velocity = shootDir * bulletSpeed;

//        // 后坐力方向改为摄像机向后方向，转换成枪本地局部方向
//        Vector3 recoilDirection = -Camera.main.transform.forward; // 世界空间向后
//        Vector3 localRecoil = transform.parent.InverseTransformDirection(recoilDirection);

//        velocity += localRecoil * recoilBackDistance * 50f;
//    }
//}
