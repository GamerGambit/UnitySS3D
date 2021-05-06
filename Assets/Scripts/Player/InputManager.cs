using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
	public class InputManager : MonoBehaviour
    {
        private static InputManager _instance;
        public static InputManager Instance { get => _instance; }

        private PlayerControls controls;

        public PlayerInput playerInput;

        // Start is called before the first frame update
        private void Awake()
        {
            if (_instance != null && _instance != this)
			{
                Destroy(gameObject);
                return;
			}

            _instance = this;
            controls = new PlayerControls();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            if (controls != null)
            {
                controls.Disable();
            }
        }

        public Vector2 GetPlayerMovement()
        {
            if (playerInput.currentActionMap.name != "GroundMovement")
                return Vector2.zero;

            return controls.GroundMovement.HorizontalMovement.ReadValue<Vector2>();
        }

        public Vector2 GetMouseDelta()
        {
            if (playerInput.currentActionMap.name != "GroundMovement")
                return Vector2.zero;

            return controls.GroundMovement.Look.ReadValue<Vector2>();
        }

        public bool JumpedThisFrame()
        {
            if (playerInput.currentActionMap.name != "GroundMovement")
                return false;

            return controls.GroundMovement.Jump.triggered;
        }

        public bool UsePressed()
        {
            if (playerInput.currentActionMap.name != "GroundMovement")
                return false;

            return controls.GroundMovement.Use.triggered;
        }
    }
}
