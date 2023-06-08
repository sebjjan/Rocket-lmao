using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Renderer[] bodyPartRenderers;
    private Renderer batRenderer;
    private Collider2D batCollider;
    private TrailRenderer batTrailRenderer;

    private bool isCooldown = false;
    private float cooldownTime = 2f;
    private float cooldownTimer = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();

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
}
