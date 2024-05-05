using System.Collections;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public float openSpeed = 2f; // The speed at which the door opens
    public Vector3 targetPosition; // Final position of the door when opened
    public Quaternion targetRotation; // Final rotation of the door when opened

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isOpening = false;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
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
                    isOpening = true;
                    StartCoroutine(OpenDoor());
                }
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        float t = 0f;
        Quaternion initialDoorRotation = transform.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(initialDoorRotation, targetRotation, t);
            yield return null;
        }
        isOpening = false;
    }
}
