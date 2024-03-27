using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

namespace _Joystick.Scripts
{
    public class CharacterMovementManager : MonoBehaviour
    {
        public VariableJoystick joystick;
        public CharacterController controller;
        public Canvas inputCanvas;
        public Animator playerAnimator;
        public bool isJoyStick;
        public float movementSpeed;
        public float rotationSpeed;

        public static CharacterMovementManager Instance { get; private set; }

        // Other variables and methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                //Debug.LogWarning("Duplicate GameController instance found. Destroying the duplicate.");
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            EnableJoyStickInput();
        }
        public void EnableJoyStickInput()
        {
            isJoyStick = true;
            inputCanvas.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (isJoyStick)
            {
                var movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);

                if (movementDirection.sqrMagnitude <= 0)
                {
                    playerAnimator.SetBool("run", false);
                    return;
                }
                playerAnimator.SetBool("run", true);
                controller.SimpleMove(movementDirection * movementSpeed);
                var targetDirection = Vector3.RotateTowards(controller.transform.forward, movementDirection, rotationSpeed * Time.deltaTime, 0.0f);
                controller.transform.rotation = Quaternion.LookRotation(targetDirection);

                //Debug.Log("Time inside area: " + timeInsideArea);

            }
            else playerAnimator.SetBool("run", false);
        }

        public void AttachShoe(GameObject shoe)
        {
            Vector3 characterPosition = controller.transform.position;

            // Set the position of the shoe to the position of the character controller
            shoe.transform.position = characterPosition;

            // Optionally, you can parent the shoe to the character controller if needed
            //shoe.transform.parent = controller.GetComponentInChildren<"A Pose Man In Dhoti">().transform;
            shoe.transform.parent = controller.transform.Find("A Pose Man In Dhoti").transform.Find("Wolf3D_Body");
            shoe.SetActive(true);
        }

    }
}
