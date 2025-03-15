using UnityEngine;

public class BouncyBallExplosion : MonoBehaviour
{
    public float expansionSpeed = 5f; // How fast the explosion grows
    public float maxSize = 5f; // Max size before disappearing
    public float fadeSpeed = 2f; // Speed of fading out

    private Material material;
    private float alpha = 1f;

    public GameObject alwaysVisible;

    void Start()
    {
        // Get the material of the explosion sphere
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Expand the sphere over time
        transform.localScale += Vector3.one * expansionSpeed * Time.deltaTime;

        // Fade out the material (if using transparency)
        //alpha -= fadeSpeed * Time.deltaTime;
        //material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);

        // Destroy the explosion when it's fully expanded or faded out
        if (transform.localScale.x >= maxSize || alpha <= 0)
        {
            Destroy(alwaysVisible);
            Destroy(gameObject);
        }
    }
} 