using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;  // 子弹存活时间，防止场景残留过多
    public int damage = 1;       // 伤害值，给目标调用用

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
        // 碰撞检测模式设置为连续动态，防止高速穿透
        if (rb != null)
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 尝试获取目标脚本
        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            target.OnHit(damage); // 调用目标受击方法
            Destroy(gameObject);  // 击中目标销毁子弹
        }
        else
        {
            // 撞到其他物体也销毁子弹
            Destroy(gameObject);
        }
    }
}
