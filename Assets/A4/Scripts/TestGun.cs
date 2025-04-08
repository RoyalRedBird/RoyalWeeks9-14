using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : GunClass
{

    //NOTE: For comments regarding the overridden methods, see the GunClass script.

    int ammoCapacity = 12; //The max ammo capacity.
    int ammoLeft = 12; //Remaining ammo.

    float horizontalRecoilMin = -0.5f; //The minimum value possible for horizontal recoil.
    float horizontalRecoilMax = 0.5f; //The maximum value possible for horizontal recoil.

    float verticalRecoilMin = 0.5f; //The minimum value possible for vertical recoil.
    float verticalRecoilMax = 1.0f; //The maximum value possible for vertical recoil.

    string weaponName = "Handgun"; //The name of the weapon.

    float fireInterval = 0.2f; //The firing interval, for fire rate.

    bool isFullAuto = false; //Is this weapon full auto?

    float reloadTime = 1.1f; //How long this gun takes to reload.

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
