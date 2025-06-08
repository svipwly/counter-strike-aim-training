using UnityEngine;
using UnityEngine.Audio;

public class Target : MonoBehaviour
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

    public void OnHit(int damage, bool isPlayerShot = true)
    {
        int scoreToAdd = 0; // Ĭ��0��
        if (isPlayerShot)
        {
            // ֻ����һ��вŻ�ȡ����
            MovingTarget moving = GetComponent<MovingTarget>();
            if (moving != null)
                scoreToAdd = moving.GetCurrentScore();
            else
                scoreToAdd = damage;
        }
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

        if (isPlayerShot)
        {
            gameManager?.AddScore(scoreToAdd); // ֻ����һ��вżӷ�
        }
        // �Զ���ʧ��isPlayerShot==false��ʱ��ȫ���ӷ�

        spawner?.TargetDestroyed();
        Destroy(gameObject);
    }
}


//using UnityEngine;
//using UnityEngine.Audio;

//public class Target : MonoBehaviour
//{
//    private GameManager gameManager;
//    private TargetSpawner spawner;

//    public AudioClip breakSound;              // ������Ч
//    public AudioMixerGroup hitMixerGroup;     // Inspector������Hit����

//    void Start()
//    {
//        gameManager = FindObjectOfType<GameManager>();
//        spawner = FindObjectOfType<TargetSpawner>();
//    }

//    public void OnHit(int damage, bool isPlayerShot = true)
//    {

//        int scoreToAdd = damage;
//        MovingTarget moving = GetComponent<MovingTarget>();
//        if (moving != null)
//            scoreToAdd = moving.GetCurrentScore();

//        // ���Զ���AudioSource����������Ч����Hit����
//        if (breakSound != null && hitMixerGroup != null)
//        {

//            GameObject go = new GameObject("HitAudio");
//            go.transform.position = transform.position;
//            AudioSource source = go.AddComponent<AudioSource>();
//            source.clip = breakSound;
//            source.outputAudioMixerGroup = hitMixerGroup;
//            source.Play();
//            Destroy(go, breakSound.length + 0.1f);

//        }

//        if (isPlayerShot)
//        {
//            gameManager?.AddScore(scoreToAdd); // ��һ��вżӷֺ�����
//        }
//        else
//        {
//            // �Զ���ʧ����ѡ�񲻼ӷ֣�Ҳ��������
//            // ������Զ���ʧҲ�ܼӻ����֣�������׼ȷ�ʣ�����������
//            gameManager?.AddScoreWithoutHit(scoreToAdd);
//        }

//        spawner?.TargetDestroyed();
//        Destroy(gameObject);


//        //// �Ʒ���Ŀ��������
//        //gameManager?.AddScore(damage);
//        //spawner?.TargetDestroyed();

//        //// ��������Ŀ��
//        //Destroy(gameObject);
//    }
//}




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
