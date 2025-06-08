using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public float lifeTime = 5f;
    public float moveSpeed = 3f;
    public float minScale = 0.3f;
    public float maxScore = 100f;
    public float minScore = 50f;

    private float timer = 0f;
    private Vector3 moveDirection;

    void Start()
    {
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // ��������ƶ�����
        Vector3 randomize = new Vector3(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.2f, 0.2f));
        moveDirection = (moveDirection + randomize).normalized;

        // �ƶ�
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ��С
        float t = Mathf.Clamp01(timer / lifeTime);
        float scale = Mathf.Lerp(1f, minScale, t);
        transform.localScale = new Vector3(scale, scale, scale);

        // ʱ�䵽���Զ���ʧ��֪ͨspawner��������
        if (timer >= lifeTime)
        {
            Target target = GetComponent<Target>();
            if (target)
                target.OnHit(0, false); // ����false��ʾ������һ��У�Target��ᴦ����ȫ���ӷ�
            else
                Destroy(gameObject);
        }
    }

    public int GetCurrentScore()
    {
        float t = Mathf.Clamp01(timer / lifeTime);
        float score = Mathf.Lerp(maxScore, minScore, t);
        return Mathf.RoundToInt(score);
    }
}
//using UnityEngine;

//public class MovingTarget : MonoBehaviour
//{
//    public float lifeTime = 5f;
//    public float moveSpeed = 3f;
//    public float minScale = 0.3f;
//    public float maxScore = 100f;
//    public float minScore = 50f;

//    private float timer = 0f;
//    private Vector3 moveDirection;

//    void Start()
//    {
//        moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
//    }

//    void Update()
//    {
//        timer += Time.deltaTime;

//        // ��������ƶ�����
//        Vector3 randomize = new Vector3(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.2f, 0.2f));
//        moveDirection = (moveDirection + randomize).normalized;

//        // �ƶ�
//        transform.position += moveDirection * moveSpeed * Time.deltaTime;

//        // ��С
//        float t = Mathf.Clamp01(timer / lifeTime);
//        float scale = Mathf.Lerp(1f, minScale, t);
//        transform.localScale = new Vector3(scale, scale, scale);

//        // ʱ�䵽���Զ���ʧ��֪ͨspawner��������
//        if (timer >= lifeTime)
//        {
//            Target target = GetComponent<Target>();
//            if (target)
//                target.OnHit(0, false); // ����false��ʾ������һ���
//            else
//                Destroy(gameObject);
//        }
//    }

//    public int GetCurrentScore()
//    {
//        float t = Mathf.Clamp01(timer / lifeTime);
//        float score = Mathf.Lerp(maxScore, minScore, t);
//        return Mathf.RoundToInt(score);
//    }
//}