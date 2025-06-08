using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioSource menuBGMSource;
    public AudioClip menuBGMClip;
    public AudioMixer audioMixer;

    void Awake()
    {
        if (menuBGMSource == null)
            menuBGMSource = GetComponent<AudioSource>();

        menuBGMSource.clip = menuBGMClip;
        menuBGMSource.loop = true;
        menuBGMSource.playOnAwake = false;
        menuBGMSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("MenuBGM")[0];

        // 1. AudioSource.volumeʼ��Ϊ1
        menuBGMSource.volume = 1.0f;

        // 2. ��ȡ�û���������Ĭ��ֵ������������Mixer��������BGMVolume��
        float userVolume = PlayerPrefs.GetFloat("BGMVolume", 1f); // 0~1��Ĭ�����
        float volumeDb = Mathf.Log10(Mathf.Clamp(userVolume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MenuBGMVolume", volumeDb);

        // 3. Mixer�������úú��ٲ�������
        menuBGMSource.Play();
    }
}

//using UnityEngine;
//using UnityEngine.Audio;

//public class MusicManager : MonoBehaviour
//{
//    public static MusicManager Instance;
//    public AudioSource menuBGMSource;
//    public AudioClip menuBGMClip;
//    public AudioMixer audioMixer; // ��Mixer����

//    void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject); // �����г���������
//        }
//        else
//        {
//            Destroy(gameObject);
//            return;
//        }
//        if (menuBGMSource == null)
//            menuBGMSource = GetComponent<AudioSource>();

//        menuBGMSource.clip = menuBGMClip;
//        menuBGMSource.loop = true;
//        menuBGMSource.playOnAwake = false;
//        menuBGMSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("MenuBGM")[0];
//        menuBGMSource.Play();
//    }
//}