using UnityEngine;

namespace Example
{
    public class CameraController : MonoBehaviour
    {
        public MyPlayer.PlayerController playerController;
        [Header("References")]
        public Transform orientation;
        public Transform player;
        public Transform playerObj;

        public float rotationSpeed;

        public Transform combatLookAt;

        public GameObject thirdPersonCam;
        public GameObject combatCam;
        public GameObject topDownCam;

        public CameraStyle currentStyle;
        public enum CameraStyle
        {
            Basic,
            Combat,
            Topdown
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            // rotate orientation
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;

            // roate player object
            if (currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

                if (inputDir != Vector3.zero)
                {
                    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
                    playerController.Move(orientation.forward * verticalInput + orientation.right * horizontalInput);
                    playerController.PlayAnimation(1);
                }
                else
                {
                    playerController.PlayAnimation(0);
                }
            }
        }
    }
}