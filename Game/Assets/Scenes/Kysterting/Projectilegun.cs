using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class ProjectileGun : MonoBehaviour
{
    // Bullet list (random selection)
    public List<GameObject> bullets = new List<GameObject>();

    // Bullet force
    public float shootForce, upwardForce;

    // Gun stats
    public float reloadTime, timeBetweenShots;
    public int magazineSize;

    int bulletsLeft;

    // Recoil
    public Rigidbody playerRb;

    // Bools
    bool shooting, readyToShoot, reloading;

    // References
    public Camera fpsCam;
    public Transform attackPoint;

    // Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;

    private void Awake()
    {
        // Make sure the magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        // Update ammo display if it exists
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void MyInput()
    {
        shooting = Input.GetKeyDown(KeyCode.Mouse0); //Shooting from MOUSE0 (left click)

        // Reload when pressing 'R' if not already reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        // Auto-reload when out of ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        // Shooting logic
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        StartCoroutine(ShootCD(timeBetweenShots));

        if (bullets.Count == 0) return; // Ensure list is not empty

        readyToShoot = false;

        // Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Center of the screen
        RaycastHit hit;

        // Determine target point
        Vector3 targetPoint = Physics.Raycast(ray, out hit) ? hit.point : ray.GetPoint(75);

        GameObject selectedBullet = bullets[Random.Range(0, bullets.Count)];  // Select a random bullet from the list

        GameObject currentBullet = Instantiate(selectedBullet, attackPoint.position, Quaternion.identity);  // Instantiate the random bullet

        Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();  // Add force to bullet
        if (bulletRb != null)
        {
            bulletRb.AddForce(fpsCam.transform.forward * shootForce, ForceMode.Impulse);
            bulletRb.AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
        }

        if (muzzleFlash != null)        // Instantiate muzzle flash if it exists
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
    }


    private void Reload()
    {
        StartCoroutine(ReloadCD(reloadTime));
    }


    IEnumerator ReloadCD(float time)
    {
        reloading = true;
        Debug.Log("Reloading!");
        yield return new WaitForSeconds(time);
        bulletsLeft = magazineSize;
        Debug.Log("Gun loaded!");
        reloading = false;
    }

    IEnumerator ShootCD(float time)
    {
        readyToShoot = false;

        yield return new WaitForSeconds(time);
        Debug.Log("I can shoot again!");
        readyToShoot = true;

    }
}
