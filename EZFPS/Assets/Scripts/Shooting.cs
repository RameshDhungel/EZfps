using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera mainCam;
    public ParticleSystem muzzleFlash;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            muzzleFlash.Play(true);
        }
    }
    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
        }
        
    }
}
