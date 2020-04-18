using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegStepper : MonoBehaviour
{
    // The position and rotation we want to stay in range of
    [SerializeField] Transform homeTransform;
    // Stay within this distance of home
    [SerializeField] float wantStepAtDistance;
    // How long a step takes to complete
    [SerializeField] float moveDuration;

    // Is the leg moving?
    public bool Moving;
    public float stepOvershootFraction;
    public void TryMove()
    {
        // If we are already moving, don't start another move
        if (Moving) return;

        float distFromHome = Vector3.Distance(transform.position, homeTransform.position);

        // If we are too far off in position or rotation
        if (distFromHome > wantStepAtDistance)
        {
            // Start the step coroutine
            StartCoroutine(MoveToHome());
        }
    }

    // Coroutines must return an IEnumerator
    IEnumerator MoveToHome()
    {
        // Indicate we're moving (used later)
        Moving = true;

        // Store the initial conditions
        Quaternion startRot = transform.rotation;
        Vector3 startPoint = transform.position;

        Quaternion endRot = homeTransform.rotation;

        // Directional vector from the foot to the home position
        Vector3 towardHome = (homeTransform.position - transform.position);
        // Total distnace to overshoot by   
        float overshootDistance = wantStepAtDistance * stepOvershootFraction;
        Vector3 overshootVector = towardHome * overshootDistance;
        // Since we don't ground the point in this simplified implementation,
        // we restrict the overshoot vector to be level with the ground
        // by projecting it on the world XZ plane.
        overshootVector = Vector3.ProjectOnPlane(overshootVector, Vector3.up);


        // Apply the overshoot
        Vector3 endPoint = homeTransform.position + overshootVector;

        // We want to pass through the center point
        Vector3 centerPoint = (startPoint + endPoint) / 2;
        // But also lift off, so we move it up by half the step distance (arbitrarily)
        centerPoint += homeTransform.up * Vector3.Distance(startPoint, endPoint) * 2;


        // Time since step started
        float timeElapsed = 0;

        // Here we use a do-while loop so the normalized time goes past 1.0 on the last iteration,
        // placing us at the end position before ending.
        do
        {
            // Add time since last frame to the time elapsed
            timeElapsed += Time.deltaTime;

            float normalizedTime = timeElapsed / moveDuration;

            // Quadratic bezier curve
            transform.position =
              Vector3.Lerp(
                Vector3.Lerp(startPoint, centerPoint, normalizedTime),
                Vector3.Lerp(centerPoint, endPoint, normalizedTime),
                normalizedTime
              );

            // Interpolate position and rotation
            transform.rotation = Quaternion.Slerp(startRot, endRot, normalizedTime);

            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveDuration);

        // Done moving
        Moving = false;
    }
}
