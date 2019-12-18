using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    public float speed = 1f;
    public GameManager gameManager;

    private float _Power = 0f;

    public void Shoot(float power)
    {
        Debug.Log("Shooting at " + power + " power.");
        _Power = power;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Get a value for how close to the center the collision happened
        ContactPoint contact = collision.GetContact(0);
        float hitDistance = Vector2.Distance(contact.point, collision.transform.position);

        if (hitDistance > 1f)
        {
            // The box collider was hit, but it was in a corner, where there is no target.
            Debug.Log("Collision, but missed.");
        }
        else
        {
            Debug.Log(name + " hit at " + hitDistance);
            
            // Stop arrow flight
            _Power = 0f;

            // Convert inverse normalized magnitude to a score 0-100
            int scoreEarned = Mathf.RoundToInt(Mathf.InverseLerp(1f, 0f, hitDistance) * 100);
            // If you're very close, just call it 100. Scoring 98 for something that looks perfect is frustrating
            if (scoreEarned > 97) scoreEarned = 100;
            // Log score
            gameManager.AddToScore(scoreEarned);
            // Child to target
            transform.parent = collision.transform.parent;
        }
    }

    void Update()
    {
        if (_Power > 0f)
        {
            transform.position += transform.forward * speed * _Power;
        }
    }
}
