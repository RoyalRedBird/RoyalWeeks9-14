using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : GunClass
{

    int ammoCapacity = 10;
    int ammoLeft = 10;

    float horizontalRecoilMin = -0.5f;
    float horizontalRecoilMax = 0.5f;

    float verticalRecoilMin = 0.5f;
    float verticalRecoilMax = 1.0f;

    string weaponName = "Test Gun";

    float fireInterval = 0.01f;

    bool isFullAuto = true;

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

}
