using System.Collections;
using UnityEngine;

public class DoorSlider : MonoBehaviour
{
    public float openSpeed = 2f; // The speed at which the door opens
    public Vector3 targetPosition; // Final position of the door when opened

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private bool isOpening = false;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        // Check if the player clicks on the door
        if (Input.GetMouseButtonDown(0) && isOpening == false)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    StartCoroutine(OpenDoor());
                }
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        isOpening = true;

        float t = 0f;
        Vector3 initialDoorPosition = transform.position;

        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.position = Vector3.Lerp(initialDoorPosition, targetPosition, t);
            yield return null;
        }

        isOpening = false;
    }
}
