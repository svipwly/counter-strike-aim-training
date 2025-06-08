using UnityEngine;
using UnityEngine.Audio;

public class ChallengeTarget : MonoBehaviour
{
    private GameManager gameManager;
    private TargetSpawner spawner;

    public AudioClip breakSound;              // ������Ч
    public AudioMixerGroup hitMixerGroup;     // Inspector������Hit����

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

        // ���Զ���AudioSource����������Ч����Hit����
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

        // �Ʒ���Ŀ��������
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

//    // ���ӵ�������������������˺�ֵ
//    public void OnHit(int damage)
//    {
//        gameManager.AddScore(damage);  // ���ӷ���
//        spawner.TargetDestroyed();     // ֪ͨ��������Ŀ�걻������
//        Destroy(gameObject);           // ����Ŀ��
//    }
//}
