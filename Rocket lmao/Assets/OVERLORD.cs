using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVERLORD : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        ScaleObjectsToScreen();
    }

    private void ScaleObjectsToScreen()
    {
        float screenHeight = mainCamera.orthographicSize * 2f;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        Vector3 scale = transform.localScale;
        scale.x = screenWidth;
        scale.y = screenHeight;
        transform.localScale = scale;
    }
}
