using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectRotation : MonoBehaviour
{
    float RotationSpeed = 4f;

    private void OnMouseDrag()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * RotationSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * RotationSpeed;

        transform.Rotate(Vector3.down, XaxisRotation);
        transform.Rotate(Vector3.right, YaxisRotation);
    }
}
