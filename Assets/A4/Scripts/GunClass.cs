using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunClass : MonoBehaviour
{

    public abstract int GetAmmoCount();

    public abstract int GetAmmoLeft();

    public abstract float GetVerticalRecoil();

    public abstract float GetHorizontalRecoil();

    public abstract string GetWeaponName();

    public abstract void DecrementAmmoCount();

    public abstract bool GetFullAutoCapability();

    public abstract float GetFireInterval();

    public abstract float GetReloadTime();

    public abstract void ReloadWeapon();

}
