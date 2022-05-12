using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    public Vector3 positionOffset;
    public Vector3 lookAtOffset;

    [SerializeField] float camPositionSmooth = 0.01f;
    [SerializeField] bool doLookAt = true;

    public Camera followCam;

    void Start()
    {
        transform.parent = null;
        followCam = this.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (targetTransform != null)
        {
            Vector3 offset = targetTransform.TransformDirection(positionOffset);
            transform.position = Vector3.Lerp(transform.position, targetTransform.position + offset, camPositionSmooth);

            if (doLookAt)
            {
                //this.transform.LookAt(targetTransform.position + targetTransform.TransformDirection(lookAtOffset));
                Vector3 newLookAt = targetTransform.position + targetTransform.TransformDirection(lookAtOffset);
                Vector3 lookDir = (newLookAt - transform.position).normalized;
                Quaternion rot = Quaternion.LookRotation(lookDir);
                transform.rotation = rot;// Quaternion.Slerp(transform.rotation, rot, 0.02f);
            }
        }
    }
}
