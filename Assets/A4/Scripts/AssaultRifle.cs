using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : GunClass
{

    //NOTE: For comments regarding the overridden methods, see the GunClass script.

    int ammoCapacity = 30; //The max ammo capacity.
    int ammoLeft = 30; //Remaining ammo.

    float horizontalRecoilMin = -0.7f; //The minimum value possible for horizontal recoil.
    float horizontalRecoilMax = 0.7f; //The maximum value possible for horizontal recoil.

    float verticalRecoilMin = 1.5f; //The minimum value possible for vertical recoil.
    float verticalRecoilMax = 2f; //The maximum value possible for vertical recoil.

    string weaponName = "Assault Rifle"; //The name of the weapon.

    float fireInterval = 0.05f; //The firing interval, for fire rate.

    bool isFullAuto = true; //Is this weapon full auto?

    float reloadTime = 2.5f; //How long this gun takes to reload.

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
