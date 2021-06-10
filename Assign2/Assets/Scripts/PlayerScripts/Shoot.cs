using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Shoot : MonoBehaviour
{
   // public EnemyHealth enemyHealth;

    [SerializeField] Transform FPSCam;
    [SerializeField] GameObject[] guns;
    [SerializeField] private int curGun = 0;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float impactForce = 5f;

    public int damagePerShot = 10;

    private float nextShootTime = 0;
    private int prevGun = 0;
    int shootableLayer;


    // Start is called before the first frame update
    void Start()
    {
        if (!FPSCam)
            FPSCam = GameObject.FindGameObjectWithTag("MainCamera").transform;

        shootableLayer = LayerMask.GetMask("Shootable");

    }

    // Update is called once per frame
    void Update()
    {
        if (prevGun != curGun)
            ChangeGun();
        prevGun = curGun;
        Debug.DrawRay(FPSCam.position, FPSCam.forward, Color.red);
        if (Input.GetButton("Fire1") && Time.time > nextShootTime)
        {
            ShootGun();
            nextShootTime = Time.time + (1 / guns[curGun].GetComponent<Gun>().shootRate);
        }
        if ((Input.GetAxis("Mouse ScrollWheel")) > 0f)
        {
            if (curGun != 2)
            {
                curGun++;
            }
            else
                curGun = 0;
        }
        if ((Input.GetAxis("Mouse ScrollWheel")) < 0f)
        {
            if (curGun != 0)
            {
                curGun--;
            }
            else
                curGun = 2;
        }
    }

    void ShootGun()
    {
        AudioSource s = guns[curGun].GetComponent<AudioSource>();
        guns[curGun].GetComponent<Gun>().muzzleFlash.Play();
        if (s != null)
            s.Play();
        if (Physics.Raycast(FPSCam.position, FPSCam.forward, out RaycastHit hitInfo, guns[curGun].GetComponent<Gun>().shootRange,shootableLayer))
        {
            Debug.Log(hitInfo.point);

            Rigidbody rb = hitInfo.rigidbody;
            if (rb != null)
            {
                rb.AddForce(-hitInfo.normal * impactForce);
            }

            EnemyHealth enemyHealth = hitInfo.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot);
            }
            GameObject g = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(g, 1f);
        }
    }

    void ChangeGun()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(false);
        }
        guns[curGun].SetActive(true);
    }
}
