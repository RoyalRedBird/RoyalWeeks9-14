using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunClass : MonoBehaviour
{

    //This is an abstract class for which every gun inherits from.
    //This way the WeaponManager can cycle through multiple different weapons with ease.

    public abstract int GetAmmoCount(); //Returns the ammo capacity of the weapon.

    public abstract int GetAmmoLeft(); //Returns the ammo the weapon has left.

    public abstract float GetVerticalRecoil(); //Returns a random number between a minimum and maximum vertical recoil range. (Implemented on the weapon side.)

    public abstract float GetHorizontalRecoil(); //Returns a random number between a minimum and maximum horizontal recoil range. (Implemented on the weapon side.)

    public abstract string GetWeaponName(); //Returns the name of the weapon.

    public abstract void DecrementAmmoCount(); //Decrements the ammo left by 1.

    public abstract bool GetFullAutoCapability(); //Returns if the weapon can shoot fully automatic or not.

    public abstract float GetFireInterval(); //Returns the time the weapon manager must wait before the gun can fire another round.

    public abstract float GetReloadTime(); //Returns the time needed to reload the gun.

    public abstract void ReloadWeapon(); //Sets the ammo left to the ammo capacity of the weapon.

}
