using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SwitchController : MonoBehaviour
{

    // untuk mengakses score manager
    public ScoreManager scoreManager;

    public float score;

    private enum SwitchState
    {
        Off,
        On,
        Blink
    }

    // menyimpan variabel bola sebagai referensi untuk pengecekan
    public Collider bola;

    // menyimpan variabel material nyala dan mati untuk merubah warna
    public Material offMaterial;
    public Material onMaterial;


    // menyimpan state switch apakah nyala atau mati
    private bool isOn;
    // komponen renderer pada object yang akan diubah
    private Renderer renderer;

    private SwitchState state;

    // tambahkan audio manager untuk mengakses fungsi pada audio managernya
    public AudioManager audioManager;

    // tambahkan vfx manager untuk mengakses fungsi pada audio managernya
    public VFXManager VFXManager;



    // Start is called before the first frame update
    void Start()
    {
        // ambil renderernya
        renderer = GetComponent<Renderer>();

        /*// set switch nya mati baik di state, maupun materialnya
        isOn = false;
        renderer.material = offMaterial;*/

        // set switch nya mati baik di state, maupun materialnya
        Set(false);

        StartCoroutine(BlinkTimerStart(5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // memastikan yang menabrak adalah bola
        if (other == bola)
        {
            /* // kita lakukan debug
             Debug.Log("Kena Bola");

             *//*// kita matikan atau nyalakan switch sesuai dengan kebalikan state switch tersebut
             // mati --> nyala || nyala --> mati
             Set(!isOn);*//*

             // jalankan coroutine blink
             StartCoroutine(Blink(2));*/

            Toggle();

            audioManager.PlaySFXSwitch(other.transform.position);

            VFXManager.PlayVFX(other.transform.position);

        }
    }

    private void Set(bool active)
    {
        if (active == true)
        {
            state = SwitchState.On;
            renderer.material = onMaterial;
            StopAllCoroutines();
        }
        else
        {
            state = SwitchState.Off;
            renderer.material = offMaterial;
            StartCoroutine(BlinkTimerStart(5));
        }
    }

    private void Toggle()
    {
        if (state == SwitchState.On)
        {
            Set(false);
        }
        else
        {
            Set(true);
        }
        //tambah score saat menyalakan atau mematikan switch
        scoreManager.AddScore(score);
    }

    private IEnumerator Blink(int times)
    {
        state = SwitchState.Blink;

        /*// parameter dikali 2 agar pengulangan tiap nyala dan mati
        int blinkTimes = times * 2;*/

        // ulang perubahan nyala mati sebanyak parameter
        for (int i = 0; i < times; i++)
            {
            /*Set(!isOn);
            yield return new WaitForSeconds(0.5f);*/

            renderer.material = onMaterial;
            yield return new WaitForSeconds(0.5f);
            renderer.material = offMaterial;
            yield return new WaitForSeconds(0.5f);
        }
        state = SwitchState.Off;

        StartCoroutine(BlinkTimerStart(5));
    }

    private IEnumerator BlinkTimerStart(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(Blink(2));
    }

}
