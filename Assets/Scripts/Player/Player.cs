using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
	public class Player : MonoBehaviour
	{
		[SerializeField] new Camera camera;

		private void Awake()
		{
			Cursor.lockState = CursorLockMode.Locked;

			InputManager.Instance.playerInput = GetComponent<PlayerInput>();
		}

		void Update()
		{
			var ray = camera.ViewportPointToRay(Vector3.one / 2.0f);
			if (InputManager.Instance.UsePressed() && Physics.Raycast(ray, out var hitInfo, 2.0f))
			{
				var interactable = hitInfo.collider.gameObject.GetComponent<IInteractable>();
				if (interactable != null)
				{
					interactable.Interact(gameObject);
				}
			}
		}
	}
}