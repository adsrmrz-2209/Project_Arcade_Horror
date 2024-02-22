using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponStats : MonoBehaviour
{
    [Header("Weapon Type")]
    [Space(3)]
    public WeaponType weaponType;

    [Header("Weapon Stats")]
    [Space(3)]
    public float gunDamage;
    public float fireRate;
    public float reloadSpeed;
    public int currentAmmo;
    public int AmmoDisplay;
    

    [Header("Enemy Take Damage Values")]
    [Space(4)]
    public int limbDamage;

    [Header("Ammo Canvas")]
    [Space(4)]
    [SerializeField] private TextMeshProUGUI ammoTxt;
    [SerializeField] Image[] bulletImage;

    private void Start()
    {
        WeaponStatistics();
        currentAmmo = AmmoDisplay;
    }

    private void Update()
    {
        currentAmmo = Mathf.Clamp(currentAmmo, 0, AmmoDisplay);
        ammoTxt.text = $"Ammo: {currentAmmo}/{AmmoDisplay}";

        for (int i = 0; i < bulletImage.Length; i++)
        {
            if (i < currentAmmo)
            {
                bulletImage[i].enabled = true;
            }
            else bulletImage[i].enabled = false;
        }
    }

    private void WeaponStatValue(float damage, int limbDmg, float fireRt, int ammoDisplay, float rldSpd)
    {
        gunDamage = damage;
        limbDamage= limbDmg;
        fireRate = fireRt;
        AmmoDisplay = ammoDisplay;
        reloadSpeed = rldSpd;
    }

    public void WeaponStatistics()
    {
        switch (weaponType)
        {
            case WeaponType.Pistol:
                WeaponStatValue(10, 1, 0.25f, 7, 3f);
                
                break;

            case WeaponType.Smg:
                WeaponStatValue(14, 1, 0.25f, 21, 3f);
                break;

            case WeaponType.AssaultRifle:
                WeaponStatValue(24, 2, 0.25f, 30, 3f);
                break;

            case WeaponType.Shotgun:
                WeaponStatValue(30, 2, 0.25f, 6, 3f);
                break;
        }
    }
}

public enum WeaponType
{
    Pistol,
    Smg,
    AssaultRifle,
    Shotgun
}
