using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] Vector2 aimpointPosition;
    [SerializeField] Vector2 mousePosition;

    public GameObject aimPoint; //The Aimpoint itself.
    public Slider reloadTimer;

    [SerializeField] float pointDistanceFromMouse; //The distance between the mouse and the aim point, used to calculate how much the aimpoint should gravitate towards the cursor.
    [SerializeField] float lagModifier = 1; //Controls how much the aimpoint lags behind the mouse cursor, more noticable at higher values. (20+)

    [SerializeField] GunClass[] availableWeapons; //An array of available GunClass objects.

    int weaponSelectIndex = 0; //The indexer for the available weapons.

    [SerializeField] float timeSinceLastFire = 0;

    public bool weaponActive = true;

    float currentWeaponReloadTime;
    float reloadTimeLeft = 0;

    // Start is called before the first frame update
    void Start()
    {

        aimpointPosition = aimPoint.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        currentWeaponReloadTime = availableWeapons[weaponSelectIndex].GetReloadTime();

        timeSinceLastFire += Time.deltaTime;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        pointDistanceFromMouse = Vector2.Distance(mousePosition, aimpointPosition);

        Vector2 calculatedPointPos = Vector2.MoveTowards(aimpointPosition, mousePosition, (pointDistanceFromMouse/lagModifier));

        aimpointPosition = calculatedPointPos;

        if(Input.GetKeyDown(KeyCode.R) && weaponActive)
        {

            StartCoroutine(StartReload());

        }

        if (availableWeapons[weaponSelectIndex].GetFullAutoCapability())
        {

            if (Input.GetMouseButton(0) && timeSinceLastFire >= availableWeapons[weaponSelectIndex].GetFireInterval())
            {

                if (weaponActive && availableWeapons[weaponSelectIndex].GetAmmoLeft() > 0)
                {

                    FireWeapon();
                    Debug.Log("DAKKA DAKKA DAKKA!");

                }

            }

        }
        else
        {

            if (Input.GetMouseButtonDown(0) && timeSinceLastFire >= availableWeapons[weaponSelectIndex].GetFireInterval())
            {

                if(weaponActive && availableWeapons[weaponSelectIndex].GetAmmoLeft() > 0)
                {

                    FireWeapon();
                    Debug.Log("BANG!");

                }               

            }

        }

        aimPoint.transform.position = aimpointPosition;

    }

    void FireWeapon()
    {

        timeSinceLastFire = 0;

        Vector2 shootPos = aimpointPosition;

        shootPos.y += availableWeapons[weaponSelectIndex].GetVerticalRecoil();

        shootPos.x += availableWeapons[weaponSelectIndex].GetHorizontalRecoil();

        Debug.Log(shootPos.x + ", " + shootPos.y);

        aimpointPosition = shootPos;

        availableWeapons[weaponSelectIndex].DecrementAmmoCount();

    }

    IEnumerator StartReload()
    {

        weaponActive = false;
        reloadTimeLeft = currentWeaponReloadTime;
        reloadTimer.gameObject.SetActive(true);
        reloadTimer.maxValue = currentWeaponReloadTime;
        yield return StartCoroutine(ReloadSequence());
        reloadTimer.gameObject.SetActive(false);
        availableWeapons[weaponSelectIndex].ReloadWeapon();
        weaponActive = true;

    }

    IEnumerator ReloadSequence()
    {

        while(reloadTimeLeft >= 0)
        {

            reloadTimeLeft -= Time.deltaTime;
            reloadTimer.value = reloadTimeLeft;

            yield return null;

        }

    }

}
