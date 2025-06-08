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
    public AudioMixerGroup shootMixerGroup; // �������Shoot����

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

        // ����Mixer����
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
//        // ��Ϸ��ͣ���������ʱֱ�ӷ���
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

//        // �������ص�
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
//    public GameObject muzzleFlashPrefab; // ������ЧԤ����
//    public Transform muzzlePoint;
//    public float bulletSpeed = 985f;
//    public float fireRate = 0.1f;

//    public float recoilBackDistance = 0.15f;    // ÿǹ������˲��λ�ƴ�С
//    public float recoilSpring = 120f;           // ���ɸն�
//    public float recoilDamping = 25f;           // ����

//    public AudioClip fireSound;                 //  ������Ч
//    private AudioSource audioSource;            // ��Ƶ���������

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

//        // �Զ���ӻ��ȡ AudioSource ���
//        audioSource = GetComponent<AudioSource>();
//        if (audioSource == null)
//            audioSource = gameObject.AddComponent<AudioSource>();
//    }

//    void Update()
//    {
//        // �����жϣ�����Ƿ���UI�ϣ���������ťʱ���
//        if (canShoot && Input.GetMouseButton(0) && Time.time >= nextFireTime)
//        {
//            if (!EventSystem.current.IsPointerOverGameObject())
//            {
//                Shoot();
//                nextFireTime = Time.time + fireRate;
//            }
//        }

//        // ��������ģ��������ص�
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
//            Debug.Log("���ɻ�����Ч");
//            GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation);
//            flash.transform.SetParent(muzzlePoint);  // ��ѡ������ǹ�ڷ���
//            Destroy(flash, 0.2f);  // �Զ����ٷ�ֹ����
//        }

//        GameManager.Instance?.AddShot();

//        // ���ſ�����Ч
//        if (fireSound != null && audioSource != null)
//            audioSource.PlayOneShot(fireSound);

//        // �����������������ת��Ϊ�ֲ����������
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
//    public GameObject muzzleFlashPrefab; // ������ЧԤ����
//    public Transform muzzlePoint;
//    public float bulletSpeed = 985f;
//    public float fireRate = 0.1f;

//    public float recoilBackDistance = 0.15f;    // ÿǹ������˲��λ�ƴ�С
//    public float recoilSpring = 120f;           // ���ɸն�
//    public float recoilDamping = 25f;           // ����

//    public AudioClip fireSound;                 //  ������Ч
//    private AudioSource audioSource;            // ��Ƶ���������

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

//        //  �Զ���ӻ��ȡ AudioSource ���
//        audioSource = GetComponent<AudioSource>();
//        if (audioSource == null)
//            audioSource = gameObject.AddComponent<AudioSource>();
//    }

//    void Update()
//    {
//        // �޸ĵ㣺ֻ���������������� LeftControl �� Fire1
//        if (canShoot && Input.GetMouseButton(0) && Time.time >= nextFireTime)
//        {
//            Shoot();
//            nextFireTime = Time.time + fireRate;
//        }

//        // ��������ģ��������ص�
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
//            print ("���ɻ�����Ч");
//            GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation);
//            flash.transform.SetParent(muzzlePoint);  // ��ѡ������ǹ�ڷ���
//            Destroy(flash, 0.2f);  // �Զ����ٷ�ֹ����
//        }

//        GameManager.Instance?.AddShot();

//        //  ���ſ�����Ч
//        if (fireSound != null && audioSource != null)
//            audioSource.PlayOneShot(fireSound);

//        // �����������������ת��Ϊ�ֲ����������
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

//    public float recoilBackDistance = 0.15f;    // ÿǹ������˲��λ�ƴ�С
//    public float recoilSpring = 120f;           // ���ɸն�
//    public float recoilDamping = 25f;           // ����

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
//        // �޸ĵ㣺ֻ���������������� LeftControl �� Fire1
//        if (canShoot && Input.GetMouseButton(0) && Time.time >= nextFireTime)
//        {
//            Shoot();
//            nextFireTime = Time.time + fireRate;
//        }

//        // ��������ģ��������ص�
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

//        // �����������������ת��Ϊ�ֲ����������
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

//    public float recoilBackDistance = 0.15f;    // ÿǹ������˲��λ�ƴ�С
//    public float recoilSpring = 120f;           // ���ɸն�
//    public float recoilDamping = 25f;           // ����

//    private float nextFireTime;

//    private Vector3 originalLocalPosition;
//    private Vector3 currentPosition;
//    private Vector3 velocity;

//    // �������Ƿ�����ǹ
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

//        // ���������˶����㣬�ָ���ԭλ
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

//        // �����������Ϊ����������ת����ǹ���ؾֲ�����
//        Vector3 recoilDirection = -Camera.main.transform.forward; // ����ռ����
//        Vector3 localRecoil = transform.parent.InverseTransformDirection(recoilDirection);

//        velocity += localRecoil * recoilBackDistance * 50f;
//    }
//}
