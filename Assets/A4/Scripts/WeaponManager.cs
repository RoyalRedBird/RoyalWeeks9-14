using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class WeaponManager : MonoBehaviour
{

    public UnityEvent onShotFired; //This event fires whenever a shot is fired.

    public Vector2 aimpointPosition; //The position of the aimpoint.
    [SerializeField] Vector2 mousePosition; //The position of the mouse.

    public GameObject aimPoint; //The Aimpoint itself.
    public Slider reloadTimer; //The slider for the Reload Timer.

    [SerializeField] float pointDistanceFromMouse; //The distance between the mouse and the aim point, used to calculate how much the aimpoint should gravitate towards the cursor.
    [SerializeField] float lagModifier = 1; //Controls how much the aimpoint lags behind the mouse cursor, more noticable at higher values. (20+)

    [SerializeField] GunClass[] availableWeapons; //An array of available GunClass objects.

    [SerializeField] int weaponSelectIndex = 0; //The indexer for the available weapons.

    [SerializeField] float timeSinceLastFire = 0; //The amount of time since the last time the weapon has been fired.

    Coroutine activeReloadCoroutine; //The coroutine that starts the reload process goes here.
    Coroutine reloadingCoroutine; //The reloading coroutine goes here.

    public bool weaponActive = true; //Is the weapon active and able to fire?

    public TextMeshProUGUI weaponSelectText; //The UI text for the weapon being used.
    public TextMeshProUGUI ammoCounterText; //The UI text for the ammo counter.
    public TextMeshProUGUI fireModeText; //The UI text for the fire mode indicator.

    float currentWeaponReloadTime; //The reload time for the active weapon.
    float reloadTimeLeft = 0; //The time left until the reload is complete.

    // Start is called before the first frame update
    void Start()
    {

        aimpointPosition = aimPoint.transform.position; //Grabs the position of the aim point.
        
    }

    // Update is called once per frame
    void Update()
    {

        currentWeaponReloadTime = availableWeapons[weaponSelectIndex].GetReloadTime(); //Grabs the reload time of the currently selected weapon.

        timeSinceLastFire += Time.deltaTime; //Tracking the time from the last time the gun fired.

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get the mouse position.

        pointDistanceFromMouse = Vector2.Distance(mousePosition, aimpointPosition); //Gets the distance between the aim point and the mouse pos.

        //The aim point is moved towards the mouse cursor using the MoveTowards function. It uses the distance from the mouse divided by the lag modifier to determine how hard to push the aim point to the mouse.
        //The further the mouse is from the aim point, the more the aim point will try and keep up.
        Vector2 calculatedPointPos = Vector2.MoveTowards(aimpointPosition, mousePosition, (pointDistanceFromMouse/lagModifier));
       
        aimpointPosition = calculatedPointPos; //Apply transfrom to aim point placeholder.

        if (Input.GetKeyDown(KeyCode.R) && weaponActive) //If R is pressed and the weapon is active...
        {

            activeReloadCoroutine = StartCoroutine(StartReload()); //Start the coroutine that starts the reload. Also bind this coroutine to activeReloadCoroutine.

        }

        if (Input.GetKeyDown(KeyCode.E)) //If E is pressed...
        {

            CycleWeaponUp(); //Cycle to the next available weapon.

        }

        if (Input.GetKeyDown(KeyCode.Q)) //If Q is pressed...
        {

            CycleWeaponDown(); //Cycle to the previous weapon.

        }

        if (availableWeapons[weaponSelectIndex].GetFullAutoCapability()) //If the weapon is full auto, use GetMouseButton.
        {

            if (Input.GetMouseButton(0) && timeSinceLastFire >= availableWeapons[weaponSelectIndex].GetFireInterval()) //If MB1 is held down and the time since the last shot is greater than the fire interval of the current weapon...
            {

                if (weaponActive && availableWeapons[weaponSelectIndex].GetAmmoLeft() > 0) //And if the weapon is active and has ammo in it...
                {

                    FireWeapon(); //Fire the weapon.

                }

            }

        }
        else //If the weapon is semi auto, use GetMouseButtonDown.
        {

            if (Input.GetMouseButtonDown(0) && timeSinceLastFire >= availableWeapons[weaponSelectIndex].GetFireInterval()) //If MB1 is pressed and the time since the last shot is greater than the fire interval of the current weapon...
            {

                if(weaponActive && availableWeapons[weaponSelectIndex].GetAmmoLeft() > 0) //And if the weapon is active and has ammo in it...
                {

                    FireWeapon(); //Fire the weapon.

                }               

            }

        }

        aimPoint.transform.position = aimpointPosition; //Apply the transforms to the aim point itself. (Note: Recoil is added to the aim point when FireWeapon() is called.

        weaponSelectText.text = "Current Weapon: " + availableWeapons[weaponSelectIndex].GetWeaponName(); //Update the current weapon text.
        ammoCounterText.text = "Ammo: " + availableWeapons[weaponSelectIndex].GetAmmoLeft() + "/" + availableWeapons[weaponSelectIndex].GetAmmoCount(); //Update the ammo counter text.
        fireModeText.text = "Full Auto?: " + availableWeapons[weaponSelectIndex].GetFullAutoCapability(); //Update the fire selector text.

    }

    //This function fires the weapon, decreasing ammo left, applying ammo and invoking the onShotFired event.
    void FireWeapon()
    {

        timeSinceLastFire = 0; //Reset the time since the last round was fired.

        onShotFired.Invoke(); //Invoke the onShotFired event, the targets should be subscribed to this event.

        Vector2 shootPos = aimpointPosition; //Get the aimpoint position again.

        shootPos.y += availableWeapons[weaponSelectIndex].GetVerticalRecoil(); //Apply vertical recoil.

        shootPos.x += availableWeapons[weaponSelectIndex].GetHorizontalRecoil(); //Apply horizontal recoil.

        aimpointPosition = shootPos; //Apply transform to aim point placeholder again.

        availableWeapons[weaponSelectIndex].DecrementAmmoCount(); //Decrement the ammo on the gun's script.     

    }

    //This enumerator starts and handles the reload process.
    IEnumerator StartReload()
    {

        weaponActive = false; //Sets the weapon to inactive.
        reloadTimeLeft = currentWeaponReloadTime; //Sets the reloadTimeRemaining to the reload time of the current weapon.
        reloadTimer.gameObject.SetActive(true); //Reveals the reload timer bar.
        reloadTimer.maxValue = currentWeaponReloadTime; //Sets the max value of the timer bar to the reload time of the weapon.
        yield return reloadingCoroutine = StartCoroutine(ReloadSequence()); //Starts the reload timer coroutine.
        reloadTimer.gameObject.SetActive(false); //Once the reload timer coroutine is over, hide the timer bar.
        availableWeapons[weaponSelectIndex].ReloadWeapon(); //Reload the weapon.
        weaponActive = true; //And set the weapon as being active again.

    }

    IEnumerator ReloadSequence() //This coroutine handles the reload timer for the weapon.
    {

        while(reloadTimeLeft >= 0) //While the time left to reload is greater than zero.
        {

            reloadTimeLeft -= Time.deltaTime; //Decrement the remaining time by delta time.
            reloadTimer.value = reloadTimeLeft; //Set the value of the reload timer bar to the remaining reload time.

            yield return null;

        }

    }

    void CycleWeaponUp() //This function cycles the weapon indexer up. To select the next available weapon.
    {

        int newIndex = weaponSelectIndex + 1; //The current weapon index plus one is saved to newIndex.

        if(newIndex > availableWeapons.Length - 1) //If incrementing the indexer would make it out of bounds...
        {

            newIndex = 0; //Loop back around to zero.

        }

        weaponSelectIndex = newIndex; //Set the weapon index to the value of new index.

        if(activeReloadCoroutine!= null) //If the current/previous weapon was being reloaded...
        {

            StopCoroutine(activeReloadCoroutine); //End both reload coroutines.
            StopCoroutine(reloadingCoroutine);
            reloadTimer.gameObject.SetActive(false); //Hide the timer bar.
            weaponActive = true; //Set the weapon as active.

        }

    }

    void CycleWeaponDown() //This function cycles the weapon indexer down. To select the previous weapon.
    {

        int newIndex = weaponSelectIndex - 1; //The current weapon index minus one is saved to newIndex.

        if (newIndex < 0) //If decrementing the indexer would make it out of bounds...
        {

            newIndex = availableWeapons.Length - 1; //Loop back around to the end of the array.

        }

        weaponSelectIndex = newIndex; //Set the weapon index to the value of new index.

        if (activeReloadCoroutine != null) //If the current/previous weapon was being reloaded...
        {

            StopCoroutine(activeReloadCoroutine); //End both reload coroutines.
            StopCoroutine(reloadingCoroutine);
            reloadTimer.gameObject.SetActive(false); //Hide the timer bar.
            weaponActive = true; //Set the weapon as active.

        }

    }

    //INT. BRENNAN
    //DEAD DEATH
    //KILLME KILLME KILLME KILLME KILLME KILLME

}
