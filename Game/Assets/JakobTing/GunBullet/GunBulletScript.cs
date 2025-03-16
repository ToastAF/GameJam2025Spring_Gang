using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GunBulletScript : MonoBehaviour
{
    float x, y, z;
    public float maxSpinValue;
    Rigidbody rb;

    public List<BulletContainer> bulletContainers = new List<BulletContainer>();
    public ProjectileGun gunScript;

    GameObject selectedBullet;
    public Transform attackPoint;

    AudioSource gunSound;

    void Start()
    {
        x = Random.Range(0, maxSpinValue*2);
        y = Random.Range(0, maxSpinValue);
        z = Random.Range(0, maxSpinValue);

        rb = GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(x, y, z), ForceMode.Impulse);

        gunScript = GameObject.FindGameObjectWithTag("Gun").GetComponent<ProjectileGun>();
        bulletContainers = gunScript.bulletContainers;

        StartCoroutine(WaitForShoot());

        gunSound = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        selectedBullet = bulletContainers[Random.Range(0,bulletContainers.Count)].bullet;

        GameObject currentBullet = Instantiate(selectedBullet, attackPoint.position, Quaternion.identity);

        Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();  // Add force to bullet
        if (bulletRb != null)
        {
            bulletRb.AddForce(transform.forward * 100, ForceMode.Impulse);
            bulletRb.AddForce(transform.up * 10, ForceMode.Impulse);
        }
        gunSound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(50);
        }
    }

    IEnumerator WaitForShoot()
    {
        yield return new WaitForSeconds(Random.Range(1,4));
        Shoot();
        StartCoroutine(KillSelf());
    }

    IEnumerator KillSelf()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
