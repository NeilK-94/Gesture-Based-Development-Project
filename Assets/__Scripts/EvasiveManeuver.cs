using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public Vector2 startDelay;
    public float dodge;
    public Vector2 maneuverTime;
    public Vector2 maneuverDelay;
    public float targetManeuver;
    public float smoothing;
    public float tilt;
    public Boundary boundary;

    private float newManeuver;
    private float currentSpeed;
    private Rigidbody rb;

    void Start()
    {
        currentSpeed = rb.velocity.z;   //  Get the current speed along the z
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Evade());
    }

    void FixedUpdate()
    {
        newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3
            (   //  Keep the enemy iinside the screen
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    //  Set target value along x axis and move towards it over a period of tie
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startDelay.x, startDelay.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);    //  will return the opposite sign value to the x position
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverDelay.x, maneuverDelay.y));
        }
    }
}
