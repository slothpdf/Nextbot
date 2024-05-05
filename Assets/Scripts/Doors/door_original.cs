using UnityEngine;
using System.Collections;

public class DoorRotator : MonoBehaviour
{
    public float rotationSpeed = 180f; // The speed at which the door rotates (degrees per second)
    public bool rotateClockwise = true; // True for clockwise rotation, false for counterclockwise

    private bool isOpen = false;
    private bool isRotating = false;
    private Quaternion initialRotation;

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
                    Quaternion endRotation = isOpen ? initialRotation : initialRotation * Quaternion.Euler(Vector3.up * (rotateClockwise ? 90f : -90f));
                    StartCoroutine(RotateCoroutine(startRotation, endRotation));
                }
            }
        }
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
