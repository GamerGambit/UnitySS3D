using UnityEngine;

namespace Assets.Scripts.Player
{
	public class Movement : MonoBehaviour
	{
		[SerializeField] CharacterController controller;
		[SerializeField] float speed = 11.0f;
		[SerializeField] float gravity = -9.81f;
		[SerializeField] LayerMask groundMask;
		[SerializeField] float jumpHeight = 3.5f;

		Vector3 verticalVelocity = Vector3.zero;
		bool isGrounded;

		private void Update()
		{
			isGrounded = controller.isGrounded;

			if (isGrounded)
			{
				verticalVelocity.y = 0;
			}

			var horizontalInput = InputManager.Instance.GetPlayerMovement();
			Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
			controller.Move(horizontalVelocity * Time.deltaTime);

			if (InputManager.Instance.JumpedThisFrame() && isGrounded)
			{
				verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
			}

			verticalVelocity.y += gravity * Time.deltaTime;
			controller.Move(verticalVelocity * Time.deltaTime);
		}
	}
}
