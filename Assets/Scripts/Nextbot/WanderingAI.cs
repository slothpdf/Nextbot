using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {
    // speed of wandering game object
    // and how far to look for game obstacles
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    // state of game object
    private bool isAlive;

    private void Start() {
        // set game object to alive
        isAlive = true;
    }


    // Update is called once per frame
    void Update() {
        // move forward ONLY if alive
        if (isAlive) {
            transform.Translate(0, 0, speed * Time.deltaTime);

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

    // public method to set to alive by outside scripts
    public void SetAlive(bool alive) {
        isAlive = alive;
    }
}