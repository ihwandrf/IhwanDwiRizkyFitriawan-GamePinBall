using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public Transform target;
    // buat dummy target untuk set variabel target ke dummy target saat FocusAtTarget
    public Transform dummyTarget;
    private Vector3 defaultPosition;

    // waktu yang dibutuhkan untuk kembali
    public float returnTime;

    // kecepatan kamera dalam mengikuti target
    public float followSpeed;

    // kita set length nya disini karena kan dipakai di fungsi Update
    public float length;


    // kita pakai state hasTarget yang diambil dari nilai target != null
    public bool hasTarget { get { return target != null; } }

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        /*// kita beri debug dulu dengan GetKey karena script ini masih tahap awal
        if (Input.GetKey(KeyCode.Space))
        {
            // tiap pencet spasi, langsung jalanin Coroutine Move Position untuk testing saja
            StartCoroutine(MovePosition(target.position, 5));
        }*/

        /* if (Input.GetKey(KeyCode.Space))
         {
             FocusAtTarget(dummyTarget, 5);
         }

         // tombol R untuk membuatnya kembali ke posisi default
         if (Input.GetKey(KeyCode.R))
         {
             GoBackToDefault();
         }*/

        // fungsi update kita ubah total menjadi fungsi untuk kamera mengikuti object
        // secara terus menerus sampai target kamera dikosongkam kembali
        if (hasTarget)
        {
            Vector3 targetToCameraDirection = transform.rotation * -Vector3.forward;
            Vector3 targetPosition = target.position + (targetToCameraDirection * length);

            // disini kamera dipindahkan menggunakan lerp biasa yang terjadi tiap update
            // Lerp yang dijalankan disini secara otomatis menjadi smoothing karena menggunakan
            // transform.position secara langsung
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    // fungsi FocusAtTarget diubah menjadi FollowTarget dengan sistem yang berbeda
    public void FollowTarget(Transform targetTransform, float targetLength)
    {
        // saat mulai follow, pastikan coroutine gerakan kamera ke posisi default berhenti
        StopAllCoroutines();

        // disini kita hanya set saja target dan length nya, nanti fungsi update akan otomatis
        // bekerja karena hasTarget akan menjadi true
        target = targetTransform;
        length = targetLength;
    }


    /*// belum dipakai
    public void FocusAtTarget(Transform targetTransform, float length)
    {
        // ubah target
        target = targetTransform;

        // disini perlu ditambahkan kalkulasi posisi kamera dari target
        Vector3 targetToCameraDirection = transform.rotation * -Vector3.forward;
        Vector3 targetPosition = target.position + (targetToCameraDirection * length);

        // dan fungsi untuk menggerakan kameranya
        StartCoroutine(MovePosition(targetPosition, 5));
    }*/

    // belum dipakai
    public void GoBackToDefault()
    {
        // sama seperti FollowTarget, pastikan coroutine berhenti
        StopAllCoroutines();

        // kita set targetnya ke null agar hasTarget menjadi false
        target = null;

        /*// disini perlu ditambahkan fungsi untukmengggerakan ke posisi default
        StartCoroutine(MovePosition(defaultPosition, 5));*/

        //gerakan ke posisi default dalam waktu return time
        StartCoroutine(MovePosition(defaultPosition, returnTime));
    }

    private IEnumerator MovePosition(Vector3 target, float time)
    {
        float timer = 0;
        Vector3 start = transform.position;

        while (timer < time)
        {
            // pindahkan posisi camera secara bertahap menggunakan Lerp
            // Lerp ini kita tambahkan smoothing menggunakan SmoothStep
            transform.position = Vector3.Lerp(start, target, Mathf.SmoothStep(0.0f, 1.0f, timer / time));

            // di lakukan berulang2 tiap frame selama parameter time
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // kalau proses pergerakan sudah selesai, kamera langsung dipaksa pindah
        // ke posisi target tepatnya agar tidak bermasalah nantinya
        transform.position = target;
    }
}
