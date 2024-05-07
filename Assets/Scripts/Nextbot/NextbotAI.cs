using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBotController : MonoBehaviour {
    public Transform player; // reference to player
    public float moveSpeed = 5f; // movement speed of nextbot
    public float followDistance = 10f; // distance that nextbot starts following player
    public float collisionDistance = 1f; // distance to trigger collision with player

    private bool playerAlive = true; // to track player life status

    private void Update() {
        if (playerAlive) {
            // checks if player is within follow distance
            if (IsPlayerWithinFollowDistance()) {
                FollowPlayer();
            }
        }
    }

    private bool IsPlayerWithinFollowDistance() {
        return Vector3.Distance(transform.position, player.position) <= followDistance;
    }

    private void FollowPlayer() {
        if (player != null) {
            // calculates direction to move towards the player
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // moves nextbot towards the player
            transform.position += direction * moveSpeed * Time.deltaTime;

            // locks nextbot rotation along X and Z axes
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, eulerAngles.y, 0f);

            // checks collision with player
            if (Vector3.Distance(transform.position, player.position) <= collisionDistance) {
                PlayerDies();
            }
        }
    }

    private void PlayerDies() {
        playerAlive = false;
        Debug.Log("You have Died");
        /*
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null) {
            playerRigidbody.velocity = Vector3.zero; // Stop player's movement
            playerRigidbody.constraints = RigidbodyConstraints.FreezeAll; // Freeze player's movement
        }
        */

        // disables player controller script
        CharacterController playerController = player.GetComponent<CharacterController>();
        if (playerController != null) {
            playerController.enabled = false;
        }
    }
}
