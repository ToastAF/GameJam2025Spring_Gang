using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class ProjectileGun : MonoBehaviour
{
    public List<BulletContainer> bulletContainers = new List<BulletContainer>(); //ALL Bullets
    public List<GameObject> activeBullets = new List<GameObject>(); //Bullets in magazine

    public List<GameObject> bulletUIElements = new List<GameObject>();
    public List<Sprite> activeSprites = new List<Sprite>();
    public Sprite emptyBulletUI;

    public GunAnimation gunAniScript;

    int bulletCounter = 0;

    public AudioSource gunSound, reloadSound;

    // Bullet force
    public float shootForce, upwardForce;

    // Gun stats
    public float reloadTime, timeBetweenShots;
    public int magazineSize;

    int bulletsLeft;

    // Recoil
    public Rigidbody playerRb;

    // Bools
    public bool shooting, readyToShoot, reloading;

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
        PopulateMagazine();

        readyToShoot = true;

        Debug.Log(bulletContainers.Count);

        gunSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        MyInput();

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

        if (bulletContainers.Count == 0) return; // Ensure list is not empty

        gunSound.Play(); //Play BaNG

        readyToShoot = false;

        // Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Center of the screen
        RaycastHit hit;

        // Determine target point
        Vector3 targetPoint = Physics.Raycast(ray, out hit) ? hit.point : ray.GetPoint(75); //Bruger vi den her ?????????????

        GameObject selectedBullet = activeBullets[bulletCounter];  // Select next bullet from the list

        GameObject currentBullet = Instantiate(selectedBullet, attackPoint.position, Quaternion.identity);  // Instantiate the random bullet

        currentBullet.transform.LookAt(targetPoint);

        Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();  // Add force to bullet
        if (bulletRb != null)
        {
            bulletRb.AddForce(fpsCam.transform.forward * shootForce, ForceMode.Impulse);
            bulletRb.AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
        }

        gunAniScript.shoot();

        if (muzzleFlash != null)        // Instantiate muzzle flash if it exists
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        //activeBullets.RemoveAt(0);
        activeSprites.RemoveAt(0);
        UpdateBulletUI();
        StartCoroutine(MuzzleFlash());

        bulletCounter++;
        Debug.Log("Current bullet counter: " + bulletCounter);
    }

    public void PopulateMagazine() //Det er her vi putter random bullets ind i magasinet
    {
        activeBullets.Clear();
        activeSprites.Clear();

        for (int i = 0; i < magazineSize; i++)
        {
            int temp = Random.Range(0, bulletContainers.Count);
            activeBullets.Add(bulletContainers[temp].bullet);

            //Populate UI
            activeSprites.Add(bulletContainers[temp].UIBullet);

            bulletsLeft = magazineSize;
            UpdateBulletUI();
        }
    }

    public void UpdateBulletUI()
    {
        int spritesLeft = activeSprites.Count;
        for(int i = 0; i < spritesLeft; i++) //Populate bullets into UI
        {
            bulletUIElements[i].GetComponent<Image>().sprite = activeSprites[i];
        }

        for (int i = spritesLeft; i < 6; i++) //Populate rest of UI with emptyBulletUI element
        {
            bulletUIElements[i].GetComponent<Image>().sprite = emptyBulletUI;
        }
    }

    private void Reload()
    {
        reloading = true;
        bulletCounter = 0; //Reset bullet counter
        Debug.Log("Start reloading!");
        //Now we call the reload animation
        gunAniScript.reload();
        reloadSound.Play();
        
        //Old Reloading
        //StartCoroutine(ReloadCD(reloadTime));
    }


    /*IEnumerator ReloadCD(float time)
    {
        reloading = true;
        Debug.Log("Reloading!");
        yield return new WaitForSeconds(time);
        bulletsLeft = magazineSize;
        Debug.Log("Done reloading!");
        PopulateMagazine();
        reloading = false;
    }*/

    IEnumerator ShootCD(float time)
    {
        readyToShoot = false;
        //Debug.Log("I shot!");

        yield return new WaitForSeconds(time);
        //Debug.Log("I can shoot again!");
        readyToShoot = true;

    }

    IEnumerator MuzzleFlash()
    {
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }
}
