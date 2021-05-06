using UnityEngine;

namespace Assets.Scripts.Player
{
	public class MouseLook : MonoBehaviour
    {
        [SerializeField] float sensitivityX = 8.0f;
        [SerializeField] float sensitivityY = 0.5f;
        [SerializeField] Transform playerCamera;
        [SerializeField] float xClamp = 85.0f;

        float xRot = 0.0f;

        // Update is called once per frame
        void Update()
        {
            var mouseDelta = InputManager.Instance.GetMouseDelta();
            var mouseX = mouseDelta.x * sensitivityX;
            var mouseY = mouseDelta.y * sensitivityY;

            transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -xClamp, xClamp);

            var targetRotation = transform.eulerAngles;
            targetRotation.x = xRot;
            playerCamera.eulerAngles = targetRotation;
        }
    }
}
