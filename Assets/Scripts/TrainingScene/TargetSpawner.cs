using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;         // 目标球预制体
    public Vector3 spawnArea = new Vector3(30f, 1f, 30f);  // 生成区域X和Z范围，Y不再作为固定高度使用
    public float spawnMinHeight = 2f;       // 最低生成高度
    public float spawnMaxHeight = 8f;      // 最高生成高度
    public int maxTargets = 5;             // 同时场上最大目标数

    private int currentTargetCount = 0;

    void Start()
    {
        // 一开始生成maxTargets个目标
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

    // 被目标销毁时调用，用于维护数量
    public void TargetDestroyed()
    {
        currentTargetCount--;
        SpawnTarget();
    }
}
