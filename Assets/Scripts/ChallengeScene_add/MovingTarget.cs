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

        // 随机调整移动方向
        Vector3 randomize = new Vector3(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.2f, 0.2f));
        moveDirection = (moveDirection + randomize).normalized;

        // 移动
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 缩小
        float t = Mathf.Clamp01(timer / lifeTime);
        float scale = Mathf.Lerp(1f, minScale, t);
        transform.localScale = new Vector3(scale, scale, scale);

        // 时间到，自动消失并通知spawner生成新球
        if (timer >= lifeTime)
        {
            Target target = GetComponent<Target>();
            if (target)
                target.OnHit(0, false); // 参数false表示不是玩家击中，Target里会处理完全不加分
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

//        // 随机调整移动方向
//        Vector3 randomize = new Vector3(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.2f, 0.2f));
//        moveDirection = (moveDirection + randomize).normalized;

//        // 移动
//        transform.position += moveDirection * moveSpeed * Time.deltaTime;

//        // 缩小
//        float t = Mathf.Clamp01(timer / lifeTime);
//        float scale = Mathf.Lerp(1f, minScale, t);
//        transform.localScale = new Vector3(scale, scale, scale);

//        // 时间到，自动消失并通知spawner生成新球
//        if (timer >= lifeTime)
//        {
//            Target target = GetComponent<Target>();
//            if (target)
//                target.OnHit(0, false); // 参数false表示不是玩家击中
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