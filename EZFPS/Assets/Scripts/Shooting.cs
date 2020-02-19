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
    public GameObject bulletImpactEffect;
    public TextMeshProUGUI ammoUI;

    public float currentAmmo;
    public float totalAmmo = 60;
    public float magSize = 6;

    public bool magEmpty = false;
    public bool reloading = false;


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
        if (Input.GetButtonDown("Fire1") && !magEmpty &&!reloading)
        {
            Shoot();
            muzzleFlash.Play(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
           StartCoroutine(Reload());
        }
      
    }
    public void Shoot()
    {

        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            GameObject bullet = Instantiate(bulletImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(bullet, 1f);
        }
        
        currentAmmo = currentAmmo - 1f;
        UpdateAmmoUI(currentAmmo, totalAmmo);
        if (currentAmmo == 0)
        {
            magEmpty = true;
           StartCoroutine(Reload());
           
        }
    }
    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(2f);
        //Checks to see if total ammo is 0
        if (totalAmmo> 0)
        {
            //if there is no ammo in the magazine and total ammo is more than mag size ammo
            if (currentAmmo == 0 && (totalAmmo - magSize) >= 0)
            {
                currentAmmo = magSize;
                UpdateTotalAmmo(magSize);
            }
            //if there is no ammo in the magazine and total ammo is not more than mag size ammo
            else if (currentAmmo == 0 && (totalAmmo - magSize) < 0)
            {
                currentAmmo += totalAmmo;
                UpdateTotalAmmo(totalAmmo);
            }
            // if there is ammo in the mag but they reloaded and there is enough total ammo left to get full magazine of ammo
            else if (currentAmmo > 0 && (totalAmmo >= (magSize - currentAmmo)))
            {
                UpdateTotalAmmo(magSize - currentAmmo);
                currentAmmo += (magSize - currentAmmo);
            }
            // if there is ammo in the mag but they reloaded and there is not enough total ammo left to get full magazine of ammo
            else
            {
                currentAmmo += totalAmmo;
                UpdateTotalAmmo(totalAmmo);
            }
            //updates ammo UI
            UpdateAmmoUI(currentAmmo, totalAmmo);
            magEmpty = false;
            reloading = false;
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
