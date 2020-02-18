using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera mainCam;
    public ParticleSystem muzzleFlash;
    public TextMeshProUGUI ammoUI;

    public float currentAmmo;
    public float totalAmmo = 60;
    public float magSize = 6;

    public bool magEmpty = false;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentAmmo = magSize;
        UpdateTotalAmmo(magSize);
        UpdateAmmoUI(currentAmmo, totalAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !magEmpty)
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

        currentAmmo = currentAmmo - 1f;
        UpdateAmmoUI(currentAmmo, totalAmmo);
        if (currentAmmo == 0)
        {
            Reload();
            //magEmpty = true;
        }
    }
    public void Reload()
    {
        if (totalAmmo> 0)
        {
            if (currentAmmo == 0 && (totalAmmo - magSize) >= 0)
            {
                currentAmmo = magSize;
                UpdateTotalAmmo(magSize);
            }
            else if (currentAmmo == 0 && (totalAmmo - magSize) < 0)
            {
                currentAmmo += totalAmmo;
                UpdateTotalAmmo(totalAmmo);
            }
            else if (currentAmmo > 0 && (totalAmmo >= (magSize - currentAmmo)))
            {
                currentAmmo += (magSize - currentAmmo);
                UpdateTotalAmmo(magSize - currentAmmo);
            }
            else
            {
                currentAmmo += totalAmmo;
                UpdateTotalAmmo(totalAmmo);
            }

            magEmpty = false;
        }
    }
    public void UpdateTotalAmmo(float usedAmmo)
    {
        totalAmmo -= usedAmmo;
    }
    public void UpdateAmmoUI(float currentAmmo, float totalAmmo)
    {
        ammoUI.text = currentAmmo + " / " + totalAmmo;
    }
}
