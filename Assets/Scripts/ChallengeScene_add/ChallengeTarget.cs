using UnityEngine;
using UnityEngine.Audio;

public class ChallengeTarget : MonoBehaviour
{
    private GameManager gameManager;
    private TargetSpawner spawner;

    public AudioClip breakSound;              // 破裂音效
    public AudioMixerGroup hitMixerGroup;     // Inspector里拖入Hit分组

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<TargetSpawner>();
    }

    public void OnHit(int damage)
    {


        int scoreToAdd = damage;
        MovingTarget moving = GetComponent<MovingTarget>();
        if (moving != null)
            scoreToAdd = moving.GetCurrentScore();

        // 用自定义AudioSource播放命中音效，走Hit分组
        if (breakSound != null && hitMixerGroup != null)
        {
            GameObject go = new GameObject("HitAudio");
            go.transform.position = transform.position;
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = breakSound;
            source.outputAudioMixerGroup = hitMixerGroup;
            source.Play();
            Destroy(go, breakSound.length + 0.1f);
        }

        // 计分与目标再生成
        gameManager?.AddScore(scoreToAdd);
        spawner?.TargetDestroyed();
        Destroy(gameObject);
    }
}




//using UnityEngine;

//public class Target : MonoBehaviour
//{
//    private GameManager gameManager;
//    private TargetSpawner spawner;

//    void Start()
//    {
//        gameManager = FindObjectOfType<GameManager>();
//        spawner = FindObjectOfType<TargetSpawner>();
//    }

//    // 让子弹调用这个方法，传入伤害值
//    public void OnHit(int damage)
//    {
//        gameManager.AddScore(damage);  // 增加分数
//        spawner.TargetDestroyed();     // 通知生成器该目标被销毁了
//        Destroy(gameObject);           // 销毁目标
//    }
//}
