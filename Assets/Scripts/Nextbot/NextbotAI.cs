using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NextbotAI : MonoBehaviour {
    public Transform player;
    public float followSpeed = 5.0f; // chasing player speed of nextbot
    public float wanderspeed = 3.0f; // wandering speed of nextbot
    public float followDistance = 10.0f; // distance that nextbot starts following player
    public float collisionDistance = 1.0f; // distance to trigger collision with player
    public float obstacleRange = 5.0f; // distance from obstacles where nextbot will turn 
    public bool playerAlive = true;
    public float volume = 1.0f;

    private NavMeshAgent agent;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        // checks if player is within follow distance
        if (IsPlayerWithinFollowDistance()) {
            FollowPlayer();
            transform.LookAt(player);
        } else {
            // if player is not within follow range (WanderingAI script)
            transform.Translate(0, 0, wanderspeed * Time.deltaTime);

            // create a ray from the wandering game object, pointed in the 
            // same direction as that game object
            Ray ray = new Ray(transform.position, transform.forward);

            // data container w/ hit info 
            RaycastHit hit;

            // perform a raycast w/ a circular volume around the ray 
            if (Physics.SphereCast(ray, 0.75f, out hit)) {
                // get a reference to the game object that was hit
                // by the sphere cast
                GameObject hitObject = hit.transform.gameObject;

                if (hit.distance < obstacleRange) {
                    // if the ray had hit something, turn around by a random angle
                    // only perform if the object is within the obstacleRange
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    // checks if player is within follow distance
    private bool IsPlayerWithinFollowDistance() {
        return Vector3.Distance(transform.position, player.position) <= followDistance;
    }

    // tells nextbot ai to follow player / checks collision with player
    private void FollowPlayer() {
        if (player != null) {
            // calculates direction to move towards the player
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // moves nextbot towards the player
            transform.position += direction * followSpeed * Time.deltaTime;
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit)) {
                if (hit.distance < obstacleRange) {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }

            // checks collision with player & if collide sends player to game over scene
            if (Vector3.Distance(transform.position, player.position) <= collisionDistance) {
                playerAlive = false;
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}