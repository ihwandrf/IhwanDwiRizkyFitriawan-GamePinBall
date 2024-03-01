using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // kita masukan audio source bgm disini
    public AudioSource bgmAudioSource;

    // kita masukan game object sfx disini
    public GameObject sfxAudioSource;

    public GameObject sfxSwitchAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // jalankan BGM saat game dimulai
        PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // fungsi yang disiapkan untuk perintah menjalankan bgm dari script lain
    public void PlayBGM() 
    {
        bgmAudioSource.Play();
    }
    // fungsi yang disiapkan untuk perintah menjalankan sfx dari script lain
    public void PlaySFX(Vector3 spawnPosition) 
    {
        // berbeda dengan bgm, disini kita buat script untuk 
        // memunculkan prefabnya pada posisi sesuai dengan parameternya
        GameObject.Instantiate(sfxAudioSource, spawnPosition, Quaternion.identity);
    }

    public void PlaySFXSwitch(Vector3 spawnPosition)
    {
        GameObject.Instantiate(sfxSwitchAudioSource, spawnPosition, Quaternion.identity);
    }
}
