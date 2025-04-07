using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : GunClass
{

    int ammoCapacity = 30;
    int ammoLeft = 30;

    float horizontalRecoilMin = -0.7f;
    float horizontalRecoilMax = 0.7f;

    float verticalRecoilMin = 1.5f;
    float verticalRecoilMax = 2f;

    string weaponName = "Assault Rifle";

    float fireInterval = 0.05f;

    bool isFullAuto = true;

    float reloadTime = 2.5f;

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
