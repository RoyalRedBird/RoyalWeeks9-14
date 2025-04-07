using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : GunClass
{

    int ammoCapacity = 12;
    int ammoLeft = 12;

    float horizontalRecoilMin = -0.5f;
    float horizontalRecoilMax = 0.5f;

    float verticalRecoilMin = 0.5f;
    float verticalRecoilMax = 1.0f;

    string weaponName = "Handgun";

    float fireInterval = 0.2f;

    bool isFullAuto = false;

    float reloadTime = 1.1f;

    public override void DecrementAmmoCount()
    {
        ammoLeft--;
    }

    public override int GetAmmoCount()
    {
        return ammoCapacity;
    }

    public override int GetAmmoLeft()
    {
        return ammoLeft;
    }

    public override float GetHorizontalRecoil()
    {
        return Random.Range(horizontalRecoilMin, horizontalRecoilMax);
    }

    public override float GetVerticalRecoil()
    {
        return Random.Range(verticalRecoilMin, verticalRecoilMax);
    }  

    public override string GetWeaponName()
    {
        return weaponName;
    }

    public override bool GetFullAutoCapability()
    {

        return isFullAuto;

    }

    public override float GetFireInterval()
    {
        return fireInterval;
    }

    public override float GetReloadTime()
    {
        return reloadTime;
    }

    public override void ReloadWeapon()
    {
        ammoLeft = ammoCapacity;
    }

}
