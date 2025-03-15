using UnityEngine;

public class ExplosiveRadio : MonoBehaviour
{
    // Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;
    public AudioSource radioAudio; // Radio playing before explosion
    public AudioSource explosionSound; // Sound for explosion

    // Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    // Damage
    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;

    // Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicsMaterial physics_mat;

    private void Start()
    {
        Setup();

        // Start playing the radio audio
        if (radioAudio) radioAudio.Play();

        // Start countdown for explosion
        Invoke("DelayedExplosion", maxLifetime);
    }

    private void Update()
    {
        // Count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void Explode()
    {
        // Prevent multiple explosions
        CancelInvoke("DelayedExplosion");
        DelayedExplosion();
    }

    private void DelayedExplosion()
    {
        // Stop radio sound
        if (radioAudio) radioAudio.Stop();

        // Play explosion sound
        if (explosionSound) explosionSound.Play();

        // Instantiate explosion effect
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        // Check for enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        foreach (Collider enemy in enemies)
        {
            // Apply damage if enemy has an EnemyHealth component
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(explosionDamage);
            }

            // Apply explosion force (if enemy has a Rigidbody)
            Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 explosionDirection = enemy.transform.position - transform.position;
                float distance = explosionDirection.magnitude;
                float forceMultiplier = 1f - (distance / explosionRange); // More force when closer

                enemyRb.AddExplosionForce(explosionForce * forceMultiplier, transform.position, explosionRange);
            }
        }

        // Destroy object after short delay
        Invoke("DelayDestroy", 0.05f);
    }

    private void DelayDestroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ignore other bullets
        if (collision.collider.CompareTag("Bullet")) return;

        // Count up collisions
        collisions++;

        // Explode if bullet hits an enemy directly and explodeOnTouch is enabled
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode();
    }

    private void Setup()
    {
        // Create a new Physics Material
        physics_mat = new PhysicsMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicsMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicsMaterialCombine.Maximum;

        // Assign material to collider
        GetComponent<Collider>().material = physics_mat;

        // Set gravity
        rb.useGravity = useGravity;
    }

    /// Just to visualize the explosion range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
