using UnityEngine;
using System.Collections;

public class AmbulanceDoorRotator : MonoBehaviour
{
    public float rotationSpeed = 180f; // The speed at which the door rotates (degrees per second)
    public bool rotateClockwise = true; // True for clockwise rotation, false for counterclockwise
    public Axis rotationAxis = Axis.Y; // The axis around which the door rotates

    private bool isOpen = false;
    private bool isRotating = false;
    private Quaternion initialRotation;

    public enum Axis { X, Y, Z }

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isRotating = true;
                    Quaternion startRotation = transform.rotation;
                    Quaternion endRotation = isOpen ? initialRotation : GetEndRotation(startRotation);
                    StartCoroutine(RotateCoroutine(startRotation, endRotation));
                }
            }
        }
    }

    private Quaternion GetEndRotation(Quaternion startRotation)
    {
        float angle = rotateClockwise ? 90f : -90f;
        Quaternion rotation = Quaternion.identity;

        switch (rotationAxis)
        {
            case Axis.X:
                rotation = Quaternion.Euler(angle, 0f, 0f);
                break;
            case Axis.Y:
                rotation = Quaternion.Euler(0f, angle, 0f);
                break;
            case Axis.Z:
                rotation = Quaternion.Euler(0f, 0f, angle);
                break;
        }

        return startRotation * rotation;
    }

    private IEnumerator RotateCoroutine(Quaternion startRotation, Quaternion endRotation)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * (rotationSpeed / 90f); // Normalize speed relative to 90 degrees
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
        isOpen = !isOpen;
        isRotating = false;
    }
}
