using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Renderer[] bodyPartRenderers;
    private Renderer batRenderer;
    private Collider2D batCollider;
    private TrailRenderer batTrailRenderer;
    private BoxCollider2D hitboxCollider;


    public float forceMagnitude = 5f; // The magnitude of the force to be applied

    private bool isCooldown = false;
    private float cooldownTime = 2f;
    private float cooldownTimer = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();

        hitboxCollider = GetComponent<BoxCollider2D>();
        hitboxCollider.enabled = false; // Disable the hitbox initially


        Transform bodyTransform = transform.parent.parent.Find("Player/Body");
        bodyPartRenderers = bodyTransform.GetComponentsInChildren<Renderer>();

        Transform batTransform = transform.parent.parent.Find("Bat");
        if (batTransform != null)
        {
            batRenderer = batTransform.GetComponent<Renderer>();
            batCollider = batTransform.GetComponent<Collider2D>();
            Transform trailTransform = batTransform.Find("Trail");
            if (trailTransform != null)
            {
                batTrailRenderer = trailTransform.GetComponent<TrailRenderer>();
            }
        }

        if (batRenderer == null)
        {
            UnityEngine.Debug.LogError("Bat Renderer component not found!");
        }

        if (batCollider == null)
        {
            UnityEngine.Debug.LogError("Bat Collider component not found!");
        }

        if (batTrailRenderer == null)
        {
            UnityEngine.Debug.LogError("Bat Trail Renderer component not found!");
        }
    }

    public void upSwing()
    {
        if (!isCooldown)
        {
            if (animator != null)
            {
                if (batRenderer != null)
                {
                    batRenderer.enabled = false;
                }

                if (batCollider != null)
                {
                    batCollider.enabled = false;
                }

                if (batTrailRenderer != null)
                {
                    batTrailRenderer.enabled = false;
                }

                foreach (Renderer renderer in bodyPartRenderers)
                {
                    renderer.enabled = false;
                }

                animator.Play("UpSwing");

                // Start the cooldown timer
                isCooldown = true;
                cooldownTimer = 0f;

                StartCoroutine(WaitForAnimation());
            }
            else
            {
                UnityEngine.Debug.LogError("Animator component not found!");
            }
        }

        // Enable the hitbox
        hitboxCollider.enabled = true;

        // Start a coroutine to disable the hitbox after a certain duration (e.g., 1 second)
        StartCoroutine(DisableHitboxAfterDuration(1f));

    }

    private IEnumerator DisableHitboxAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Disable the hitbox
        hitboxCollider.enabled = false;
    }


    private IEnumerator WaitForAnimation()
    {
        // Wait for the animation to finish
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length - 0.3f;
        yield return new WaitForSeconds(animationDuration);

        // Enable the renderers and collider immediately without any additional delay
        if (batRenderer != null)
        {
            batRenderer.enabled = true;
        }

        if (batCollider != null)
        {
            batCollider.enabled = true;
        }

        if (batTrailRenderer != null)
        {
            batTrailRenderer.enabled = true;
        }

        foreach (Renderer renderer in bodyPartRenderers)
        {
            renderer.enabled = true;
        }

        // Reset the cooldown
        isCooldown = false;
    }

    private void Update()
    {
        if (isCooldown)
        {
            // Update the cooldown timer
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                // Cooldown period has ended
                isCooldown = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            upSwing();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ball"))
        {
            UnityEngine.Debug.Log("Ball found");
            Rigidbody2D ballRigidbody = other.GetComponent<Rigidbody2D>();
            if (ballRigidbody != null)
            {
                // Calculate the force direction opposite to the ball's entry course
                Vector2 forceDirection = (other.transform.position - transform.position).normalized;

                // Apply the force diagonally upwards
                Vector2 force = forceMagnitude * forceDirection;
                ballRigidbody.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
