using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer; // 拖Mixer进来
    public Slider masterSlider;
    public Slider menuBGMSlider;
    public Slider shootSlider;
    public Slider hitSlider;

    void Start()
    {
        float defaultMaster = 0.5f;
        float defaultMenuBGM = 0.1f;
        float defaultShoot = 0.5f;
        float defaultHit = 0.5f;

        float master = PlayerPrefs.GetFloat("MasterVolume", defaultMaster);
        float menuBGM = PlayerPrefs.GetFloat("MenuBGMVolume", defaultMenuBGM);
        float shoot = PlayerPrefs.GetFloat("ShootVolume", defaultShoot);
        float hit = PlayerPrefs.GetFloat("HitVolume", defaultHit);

        masterSlider.value = master;
        menuBGMSlider.value = menuBGM;
        shootSlider.value = shoot;
        hitSlider.value = hit;

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(master) * 20f);
        audioMixer.SetFloat("MenuBGMVolume", Mathf.Log10(menuBGM) * 20f);
        audioMixer.SetFloat("ShootVolume", Mathf.Log10(shoot) * 20f);
        audioMixer.SetFloat("HitVolume", Mathf.Log10(hit) * 20f);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        menuBGMSlider.onValueChanged.AddListener(SetMenuBGMVolume);
        shootSlider.onValueChanged.AddListener(SetShootVolume);
        hitSlider.onValueChanged.AddListener(SetHitVolume);
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
    public void SetMenuBGMVolume(float value)
    {
        audioMixer.SetFloat("MenuBGMVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
        PlayerPrefs.SetFloat("MenuBGMVolume", value);
    }
    public void SetShootVolume(float value)
    {
        audioMixer.SetFloat("ShootVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
        PlayerPrefs.SetFloat("ShootVolume", value);
    }
    public void SetHitVolume(float value)
    {
        audioMixer.SetFloat("HitVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
        PlayerPrefs.SetFloat("HitVolume", value);
    }


    //void Start()
    //{
    //    // 默认音量（1=100%，0.5=50%，0.1=10%）
    //    float defaultMaster = 0.5f;
    //    float defaultMenuBGM = 0.1f;
    //    float defaultShoot = 0.5f;
    //    float defaultHit = 0.5f;

    //    // 设置Slider的初始值
    //    masterSlider.value = defaultMaster;
    //    menuBGMSlider.value = defaultMenuBGM;
    //    shootSlider.value = defaultShoot;
    //    hitSlider.value = defaultHit;

    //    // 设置Mixer参数
    //    audioMixer.SetFloat("MasterVolume", Mathf.Log10(defaultMaster) * 20f);
    //    audioMixer.SetFloat("MenuBGMVolume", Mathf.Log10(defaultMenuBGM) * 20f);
    //    audioMixer.SetFloat("ShootVolume", Mathf.Log10(defaultShoot) * 20f);
    //    audioMixer.SetFloat("HitVolume", Mathf.Log10(defaultHit) * 20f);

    //    // 监听Slider变化
    //    masterSlider.onValueChanged.AddListener(SetMasterVolume);
    //    menuBGMSlider.onValueChanged.AddListener(SetMenuBGMVolume);
    //    shootSlider.onValueChanged.AddListener(SetShootVolume);
    //    hitSlider.onValueChanged.AddListener(SetHitVolume);
    //}

    //void Start()
    //{
    //    // 初始化slider的初始值（假设dB范围-80到0）
    //    float value;
    //    audioMixer.GetFloat("MasterVolume", out value);
    //    masterSlider.value = Mathf.Pow(10, value / 20f);
    //    audioMixer.GetFloat("MenuBGMVolume", out value);
    //    menuBGMSlider.value = Mathf.Pow(10, value / 20f);
    //    audioMixer.GetFloat("ShootVolume", out value);
    //    shootSlider.value = Mathf.Pow(10, value / 20f);
    //    audioMixer.GetFloat("HitVolume", out value);
    //    hitSlider.value = Mathf.Pow(10, value / 20f);

    //    masterSlider.onValueChanged.AddListener(SetMasterVolume);
    //    menuBGMSlider.onValueChanged.AddListener(SetMenuBGMVolume);
    //    shootSlider.onValueChanged.AddListener(SetShootVolume);
    //    hitSlider.onValueChanged.AddListener(SetHitVolume);
    //}

    //public void SetMasterVolume(float value)
    //{
    //    audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
    //}

    //public void SetMenuBGMVolume(float value)
    //{
    //    audioMixer.SetFloat("MenuBGMVolume", Mathf.Log10(value) * 20f);
    //}

    //public void SetShootVolume(float value)
    //{
    //    audioMixer.SetFloat("ShootVolume", Mathf.Log10(value) * 20f);
    //}

    //public void SetHitVolume(float value)
    //{
    //    audioMixer.SetFloat("HitVolume", Mathf.Log10(value) * 20f);
    //}
}