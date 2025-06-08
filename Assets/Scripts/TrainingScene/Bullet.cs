using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;  // �ӵ����ʱ�䣬��ֹ������������
    public int damage = 1;       // �˺�ֵ����Ŀ�������

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
        // ��ײ���ģʽ����Ϊ������̬����ֹ���ٴ�͸
        if (rb != null)
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void OnCollisionEnter(Collision collision)
    {
        // ���Ի�ȡĿ��ű�
        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            target.OnHit(damage); // ����Ŀ���ܻ�����
            Destroy(gameObject);  // ����Ŀ�������ӵ�
        }
        else
        {
            // ײ����������Ҳ�����ӵ�
            Destroy(gameObject);
        }
    }
}
