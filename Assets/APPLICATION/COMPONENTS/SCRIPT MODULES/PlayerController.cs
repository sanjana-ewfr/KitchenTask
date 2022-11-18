using UnityEngine;

namespace MyPlayer
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        private float playerSpeed = 1.5f;
        private float jumpHeight = 1.0f;
        private float gravityValue = -9.81f;
        public Animator playerAnimator;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            playerAnimator = GetComponent<Animator>();
        }

        public void Move(Vector3 moveDirection)
        {
            controller.Move(moveDirection * Time.deltaTime * playerSpeed);

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (moveDirection != Vector3.zero)
            {
                PlayAnimation(1);
            }
            else
            {
                PlayAnimation(0);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }

        public void PlayAnimation(int value)
        {
            if (value == 1)
            {
                playerAnimator.SetBool("iswalk", true);
                playerAnimator.SetBool("isidle", false);
            }
            else
            {
                playerAnimator.SetBool("iswalk", false);
                playerAnimator.SetBool("isidle", true);
            }
           
        }
    }
}