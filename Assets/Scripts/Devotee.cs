using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devotee : MonoBehaviour
{
    private float movementSpeed = 4f; // Adjust movement speed as needed
    public CharacterController controller;
    public Canvas inputCanvas;
    public Animator playerAnimator;
    public float rotationSpeed;
    
    public void MoveTo(Vector3 targetPosition, System.Action onArrival = null)
    {
        StartCoroutine(MoveCoroutine(targetPosition, onArrival));
    }


    private void Update()
    {
          //var movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);

           // if (movementDirection.sqrMagnitude <= 0)
           // {
           //     playerAnimator.SetBool("run", false);
           //     return;
           // }
          //  playerAnimator.SetBool("run", true);
           // controller.SimpleMove(movementDirection * movementSpeed);
            //var targetDirection = Vector3.RotateTowards(controller.transform.forward, movementDirection, rotationSpeed * Time.deltaTime, 0.0f);
            //controller.transform.rotation = Quaternion.LookRotation(targetDirection);
    }
    private IEnumerator MoveCoroutine(Vector3 targetPosition, System.Action onArrival = null)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            //var movementDirection = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

            //var targetDirection = Vector3.RotateTowards(controller.transform.forward, movementDirection, rotationSpeed * Time.deltaTime, 0.0f);
            //controller.transform.rotation = Quaternion.LookRotation(targetDirection);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            playerAnimator.SetBool("walk", true);
            yield return null;
        }
        playerAnimator.SetBool("walk", false);
        // Ensure exact position at target
        transform.position = targetPosition;

        // Invoke callback if provided
        onArrival?.Invoke();
    }
}
