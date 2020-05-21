using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPointAtMouse : MonoBehaviour
{
    [SerializeField] public Camera cam;

    private Vector3 mousePoint3D;
  
    private void Update()
    {
       PointAtMouse();
    }

    // Gets the mouse position, pushes it back into the world position with 'z'
    private void PointAtMouse()
    {
        mousePoint3D = Camera.main.ScreenToWorldPoint(Input.mousePosition + 
                                                      Vector3.back * Camera.main.transform.position.z);
        
        transform.LookAt(mousePoint3D, Vector3.back);
    }
}
