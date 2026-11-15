using UnityEngine;
using UnityEngine.InputSystem;

public class PabloCamera : MonoBehaviour
{
    private float turnSpeed = 0.2f;
    public Transform target; // the location for the camera to follow 
    private float orbitAngle; // 

    private void LateUpdate()
    {
        // if no target do nothing 
        if (target == null)
        {
            return;
        }

        // get mouse input to rotate
        Mouse mouse = Mouse.current;
        if (mouse != null)
        {
            float mouseInput = mouse.delta.ReadValue().x;
            orbitAngle += mouseInput * turnSpeed;
        }

        // Move the Camera up above the target
        Vector3 aimPoint = target.position + Vector3.up; 

        // tilts the camera down and applies the rotation
        transform.eulerAngles = new Vector3(35f, orbitAngle, 0f);

        // move the camera away from the target based on rotation
        transform.position = aimPoint - transform.forward * 7f;
    }
}