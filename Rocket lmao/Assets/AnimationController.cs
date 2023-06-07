using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Renderer[] bodyPartRenderers;
    private Renderer batRenderer;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Transform bodyTransform = transform.Find("Player/Body");
        bodyPartRenderers = bodyTransform.GetComponentsInChildren<Renderer>();
        batRenderer = transform.Find("Bat")?.GetComponent<Renderer>();
      
    }

    public void upSwing()
    {
        Debug.Log("Tried playing the animation");

        foreach (Renderer renderer in bodyPartRenderers)
        {
            renderer.enabled = false;
        }
        batRenderer.enabled = false;
        animator.Play("upSwingAnimation");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            upSwing();
        }
    }
}
