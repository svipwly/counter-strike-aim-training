using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;         // Ŀ����Ԥ����
    public Vector3 spawnArea = new Vector3(30f, 1f, 30f);  // ��������X��Z��Χ��Y������Ϊ�̶��߶�ʹ��
    public float spawnMinHeight = 2f;       // ������ɸ߶�
    public float spawnMaxHeight = 8f;      // ������ɸ߶�
    public int maxTargets = 5;             // ͬʱ�������Ŀ����

    private int currentTargetCount = 0;

    void Start()
    {
        // һ��ʼ����maxTargets��Ŀ��
        for (int i = 0; i < maxTargets; i++)
        {
            SpawnTarget();
        }
    }

    public void SpawnTarget()
    {
        if (currentTargetCount >= maxTargets)
            return;

        float x = Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f);
        float y = Random.Range(spawnMinHeight, spawnMaxHeight);
        float z = Random.Range(-spawnArea.z / 2f, spawnArea.z / 2f);

        Vector3 spawnPosition = new Vector3(x, y, z);

        Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
        currentTargetCount++;
    }

    // ��Ŀ������ʱ���ã�����ά������
    public void TargetDestroyed()
    {
        currentTargetCount--;
        SpawnTarget();
    }
}
