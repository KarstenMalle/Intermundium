using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class RotatorNZoomer : MonoBehaviour
{
    public float ScrollSpeed;
    public float RotationSpeed;
    public Camera InspectCamera;

    private Vector3 DefaultScale;
    private Vector3 DefaultPosition;
    private Quaternion DefaultRotation;
    private float zoomVal;
    private Coroutine ZoomCo;
    private Coroutine DefaultPos;

    void OnMouseDrag()
    {
        float rotateX = Input.GetAxis("Mouse X") * RotationSpeed;
        float rotateY = Input.GetAxis("Mouse Y") * RotationSpeed;

        Vector3 right = Vector3.Cross(InspectCamera.transform.up, transform.position - InspectCamera.transform.position);
        Vector3 up = Vector3.Cross(transform.position - InspectCamera.transform.position, right);

        transform.rotation = Quaternion.AngleAxis(-rotateX, up) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(rotateY, right) * transform.rotation;
    }

    private void Start()
    {
        DefaultRotation = transform.localRotation;
        //DefaultPosition = transform.position;
        DefaultScale = transform.localScale;

  
        if (ZoomCo != null)
        {
            StopCoroutine(ZoomCo);
        }

        if (DefaultPos != null)
        {
            StopCoroutine(RotToOriginalPos());
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            DefaultPos = StartCoroutine(RotToOriginalPos());
        }

        ZoomCo = StartCoroutine(ZoomRoutine());
    }


    IEnumerator RotToOriginalPos()
    {
        float elapsedTime = 0;
        float waitTime = 0.4f;

        while(elapsedTime > waitTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, DefaultScale, elapsedTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, DefaultRotation, elapsedTime); 

            elapsedTime += Time.deltaTime;

            yield return null;
        }

    }

    IEnumerator ZoomRoutine()
    {
        Debug.Log("ZoomRoutine running");
        zoomVal += Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        
        if (zoomVal < 100)
        {
            zoomVal = 100.0f;
        }
        else if (zoomVal > 200)
        {
            zoomVal = 200;
        }
        transform.localScale = new Vector3 (zoomVal, zoomVal, zoomVal);

        yield return null;
    }
}
