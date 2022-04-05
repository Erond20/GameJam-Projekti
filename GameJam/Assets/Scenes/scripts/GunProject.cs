using UnityEngine;
using TMPro;
public class GunProject : MonoBehaviour
{

    //bullet
    public GameObject bullet;
    //bulletforce
    public float shootforce, upwardForce;
    //GunStats
    public float timeBetweenShooting, spread, reloadTime, TimeBetweenShoots;
    public int magazineSize, bulletsPerTrap;
    public bool allowButtonHold;

    //int
    int bulletLeft, BulletShot;

    //bool
    bool shooting, readyToShoot, reloading;

    //reference

    public Camera fpsCam;
    public Transform attackPoint;

    public GameObject MuzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;
    //bug fixing;
    public bool AllowInvoke = true;


    private void Awake()
    {
        //make sure magazine size
        bulletLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletLeft / bulletsPerTrap + "/" + magazineSize / bulletsPerTrap);
    }
    private void MyInput()
    {
        //check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize && !reloading) reload();

        if (readyToShoot && shooting && !reloading && bulletLeft <= 0) reload();
        //shooting
        if(readyToShoot && shooting && !reloading && bulletLeft >0 )
        {
            //set bullet shot to 0
            BulletShot = 0;

            Shoot();
        }
    }
     
    private void Shoot()
    {
        readyToShoot = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        //check if raycast hit something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
            else
            targetPoint= ray.GetPoint(75);
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread; // just add spread to last di



        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized *  shootforce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up *  upwardForce, ForceMode.Impulse);

        if (MuzzleFlash != null)
            Instantiate(MuzzleFlash, attackPoint.position, Quaternion.identity);

        bulletLeft--;
        BulletShot++;
        
       if (AllowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            AllowInvoke = false;

            if (BulletShot < bulletsPerTrap && bulletLeft > 0)
                Invoke("shoot", TimeBetweenShoots);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
        AllowInvoke = true;
    }

   private void reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);

    }
    private void ReloadFinished()
    {
        bulletLeft = magazineSize;
        reloading = false;
    }
    
}
