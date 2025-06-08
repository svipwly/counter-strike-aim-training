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

        // 1. AudioSource.volume始终为1
        menuBGMSource.volume = 1.0f;

        // 2. 读取用户音量（或默认值），优先设置Mixer参数（如BGMVolume）
        float userVolume = PlayerPrefs.GetFloat("BGMVolume", 1f); // 0~1，默认最大
        float volumeDb = Mathf.Log10(Mathf.Clamp(userVolume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MenuBGMVolume", volumeDb);

        // 3. Mixer参数设置好后再播放音乐
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
//    public AudioMixer audioMixer; // 拖Mixer进来

//    void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject); // 保持切场景不销毁
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