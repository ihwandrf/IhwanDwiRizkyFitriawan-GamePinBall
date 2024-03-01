using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    public Collider bola; // referensi ke bola
    public KeyCode input; // tombol input untuk aktivasi launch
    public float force; // besar gaya yang diberikan saat launch

    // untuk set berapa lama waktu yang harus dicapai hingga maksimal force
    public float maxTimeHold;
    // untuk set berapa besar maksimal force yang bisa didapat (ini menggantikan force)
    public float maxForce;

    // state pada launcher
    private bool isHold;

    private Renderer render;
    private Renderer ogRender;

    public Color altColor;
    private Color ogColor;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        // di set false state nya saat baru mulai
        isHold = false;
        render = GetComponent<Renderer>();
        ogRender = GetComponent<Renderer>();
        ogColor = render.material.color;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // hanya dapat membaca input saat bersentuhan dengan bola saja
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider == bola)
        {
            ReadInput(bola);
        }
    }

    // baca input
    private void ReadInput(Collider collider)
    {
        /*if (Input.GetKey(input))
        {
            // dorong bola ke atas dengan menggunakan gaya dorong dngn besaran tertentu
            collider.GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
        }*/

        // di tambah adjustment agar tidak dijalankan saat sedang di hold
        if (Input.GetKey(input) && !isHold)
        {
            // disini langsung jalankan coroutine untuk mengkalkulasinya
            StartCoroutine(StartHold(collider));
        }
    }

    // coroutine untuk fitur hold launcher
    private IEnumerator StartHold(Collider collider)
    {
        // di set true dulu
        isHold = true;
        animator.SetBool("isHold", true);

        while (Input.GetKey(input))
        {
            // isi perhitungan force disini
            float force = 0.0f;
            float timeHold = 0.0f;

            while (Input.GetKey(input))
            {
                // hitung force menggunakan lerp
                force = Mathf.Lerp(0, maxForce, timeHold / maxTimeHold);

                // tunggu step berikutnya dan naikan timer 
                // agar mendapat nilai force yang lebih besar dari sebelumnya
                yield return new WaitForEndOfFrame();
                render.material.color = altColor;
                timeHold += Time.deltaTime;
                
            }
        }

        // kalau tombol dilepas, maka proses hold selesai
        collider.GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
        isHold = false;
        animator.SetBool("isHold", false);

        render.material.color = ogColor;
    }
}
