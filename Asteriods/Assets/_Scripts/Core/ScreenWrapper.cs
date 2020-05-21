using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    private Renderer[] renderers;

    private bool isWrappingX = false;
    private bool isWrappingY = false;

    private void Start() 
    {
        renderers = GetComponentsInChildren<Renderer>();     
    }

    private void FixedUpdate() 
    {
        ScreenWrap();
    }

    public void ScreenWrap()
    {
        bool isVisible = CheckRenderers();

        // If we are on screen, then we are not wrapping around
        if(isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if(isWrappingX && isWrappingY)
        {
            return;
        }

        // Gets the current position
        Vector3 newPosition = transform.position;

        // Sets the position of the ship on the opposite end of the screen
        if(newPosition.x > -1 || newPosition.x < 0)
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }

        if (newPosition.y > 1 || newPosition.y < 0)
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }

        // The updated position will now be the new position
        transform.position = newPosition;

    }

    // Checks if the renders are on screen
    private bool CheckRenderers()
    {
        foreach(Renderer renderer in renderers)
        {
            if(renderer.isVisible)
            {
                return true;
            }
        }
        return false;
    }
}
