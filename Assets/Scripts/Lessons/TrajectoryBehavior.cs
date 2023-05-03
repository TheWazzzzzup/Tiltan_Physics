using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryBehavior : MonoBehaviour
{


    [SerializeField] float xThrowIntensity;
    [SerializeField] float yGravityIntensity;

    Transform objectTransform;

    uint timeLocOnScale;

    private void Start()
    {
        objectTransform = GetComponent<Transform>();

        if (objectTransform == null) Debug.LogWarning("Cant Find Transform");
    }

    private void FixedUpdate()
    {
        timeLocOnScale ++;
        if (objectTransform != null)
        {
            Vector3 dest = ZeroDragTranslate(xThrowIntensity,yGravityIntensity);
            objectTransform.transform.Translate(dest);
        }
    }

    Vector3 ZeroDragTranslate(float xSpeed, float ySpeed)
    {
        float x, y, z;

        x = xSpeed * timeLocOnScale;
        z = 0;

        // y 
        y = (-yGravityIntensity / Mathf.Pow(2 * x, 2)) * Mathf.Pow(x, 2);

        Vector3 NewForce = new Vector3(x, y, z);

        return NewForce;
    }
}
