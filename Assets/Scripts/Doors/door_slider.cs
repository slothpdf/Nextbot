using UnityEngine;
using System.Collections;

public class SlidingObject : MonoBehaviour
{
    public float slideSpeed = 2f; // Speed of sliding
    public Vector3 openPosition; // Target position when the door is opened
    private Vector3 initialPosition; // Initial position of the door
    private Vector3 targetPosition; // Target position for sliding

    private bool isOpen = false; // Indicates if the door is open
    private bool isMoving = false; // Indicates if the door is currently moving

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition; // Initialize target position to initial position
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ToggleObject();
                }
            }
        }
    }

    private void ToggleObject()
    {
        if (isOpen)
        {
            StartCoroutine(MoveObject(transform.position, initialPosition));
        }
        else
        {
            StartCoroutine(MoveObject(initialPosition, openPosition));
        }
        isOpen = !isOpen;
    }

    private IEnumerator MoveObject(Vector3 startPosition, Vector3 targetPosition)
    {
        isMoving = true;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float slideTime = distance / slideSpeed; // Calculate slide time based on distance and speed
        float elapsedTime = 0f;

        while (elapsedTime < slideTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / slideTime); // Ensure t stays between 0 and 1
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition; // Ensure object reaches the target position
        isMoving = false;
    }
}
