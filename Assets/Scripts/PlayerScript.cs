using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    RaycastHit hit;
    Ray rayOrigin;
    public Camera cam;

    [SerializeField] Image crossHair;

    //public bool isHitting; //for debug only
    [SerializeField] private GameObject hitPointObject;


    WeaponStats weaponStats;
    PlayerHealth health;

    EnemyMiniHealth enemyMiniHealth;
    public EnemyHealth enemyHealth;

   

    void Start()
    {
       
        weaponStats = GetComponent<WeaponStats>();
        cam = GetComponentInChildren<Camera>();
        health = GetComponent<PlayerHealth>();
    }


    void Update()
    {
        RaycastOnCrosshair();
        CrosshairMovement();
        DamageEnemy();
        ReloadWeapon();
        /*only for debugging
        EnemyDetected(); //do not put this on update
        sisHitting = EnemyDetected();
        */
        if (Input.GetKeyDown(KeyCode.D))
        {
            health.TakeDamage();
        }
    }

    private Vector3 CrosshairMovement()
    {
        return crossHair.transform.position = Input.mousePosition;
    }

    private bool EnemyDetected()
    {
        enemyMiniHealth = hit.collider.GetComponent<EnemyMiniHealth>();

        if (enemyMiniHealth != null)
        {
            //get the EnemyHealth component from the parent
            enemyHealth = enemyMiniHealth.GetComponentInParent<EnemyHealth>();
            return true;
        }
        else return false;

        //return enemyMiniHealth != null ? true : false;
    }

    void RaycastOnCrosshair()
    {
        Vector3 crosshairPos = new Vector3(crossHair.transform.position.x, crossHair.transform.position.y, 0);

        //set the raycast position in the crosshair image
        rayOrigin = cam.ScreenPointToRay(crosshairPos);

        //cast the ray from the crosshair image
        Physics.Raycast(rayOrigin.origin, rayOrigin.direction, out hit, Mathf.Infinity);

        //check if the ray is accurately hitting the spot (for debugging purposes only)
        hitPointObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
    }

    void DamageEnemy()
    {
        if (Input.GetMouseButtonDown(0) && weaponStats.currentAmmo > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            weaponStats.currentAmmo--;
            weaponStats.WeaponStatistics();
            if (EnemyDetected())
            {
                switch (hit.collider.tag)
                {
                    case "head":
                        enemyMiniHealth.TakeDamage(weaponStats.limbDamage, hit);
                        break;

                    case "body":
                        enemyMiniHealth.TakeDamage(0, hit);
                        enemyHealth.TakeDamage(weaponStats.gunDamage);
                        break;

                    case "limbs":
                        enemyMiniHealth.TakeDamage(weaponStats.limbDamage, hit);
                        break;
                }
            }
        }
    }

    void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponStats.currentAmmo = weaponStats.AmmoDisplay;
        }
    }

    
}




