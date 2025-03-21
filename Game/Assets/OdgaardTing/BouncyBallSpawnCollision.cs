using UnityEngine;

public class BouncyBallSpawnCollision : MonoBehaviour
{
    public GameObject explosionPrefab; // Assign the Explosion Prefab in the Inspector
    public GameObject alwaysVisibleWhite;
    public GameObject destroyParent;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Ensure the enemy has the "Enemy" tag
        {
            // Spawn the explosion at the same position as the ball
            BouncyBallExplosion bbe = Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<BouncyBallExplosion>();
            GameObject alwaysVisible = Instantiate(alwaysVisibleWhite, transform.position, Quaternion.identity);

            bbe.alwaysVisible = alwaysVisible;
            
            Debug.Log("COLLISION WITH ENEMY DETECTED");

            // Destroy the ball immediately
            Destroy(destroyParent);
        }
    }
}