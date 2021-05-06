using Assets.Scripts.Player;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
	public class TGWindow : VisualElement
	{
        private readonly CursorLockMode oldLockMode;
        private readonly bool oldMouseVisibility;
        private readonly InputActionMap oldActionMap;

        private readonly VisualElement titleBar = new VisualElement();
        private readonly Label titleLabel = new Label();
        private readonly Button closeButton = new Button();
        private readonly VisualElement main = new VisualElement();

        public new class UxmlFactory : UxmlFactory<TGWindow, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription title = new UxmlStringAttributeDescription { name = "title", defaultValue = "TG Window" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var ate = ve as TGWindow;
                ate.titleLabel.text = title.GetValueFromBag(bag, cc);
            }
        }

        /// <summary>
        /// GameObject which contains the UIDocument component
        /// </summary>
        public GameObject UIObject;

        public string title
        {
            get => titleLabel.text;
            set => titleLabel.text = value;
        }

        public TGWindow() : base()
        {
            oldLockMode = UnityEngine.Cursor.lockState;
            oldMouseVisibility = UnityEngine.Cursor.visible;
            oldActionMap = InputManager.Instance.playerInput.currentActionMap;

            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            InputManager.Instance.playerInput.SwitchCurrentActionMap("Menu");

            contentContainer = this;

            titleBar.AddToClassList("titlebar");
            titleBar.name = "titlebar";
            Add(titleBar);

            titleLabel.AddToClassList("title");
            titleLabel.name = "window-title";
            titleBar.Add(titleLabel);

            closeButton.text = char.ConvertFromUtf32(0xf00d);
            closeButton.AddToClassList("fa");
            closeButton.style.paddingBottom = new StyleLength(new Length(4, LengthUnit.Pixel));
            closeButton.style.paddingTop = new StyleLength(new Length(4, LengthUnit.Pixel));
            closeButton.style.paddingLeft = new StyleLength(new Length(4, LengthUnit.Pixel));
            closeButton.style.paddingRight = new StyleLength(new Length(4, LengthUnit.Pixel));
            closeButton.style.fontSize = new StyleLength(new Length(24, LengthUnit.Pixel));
            titleBar.Add(closeButton);

            main.AddToClassList("main");
            main.name = "main";
            Add(main);

            contentContainer = main;

            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
		}

		public override VisualElement contentContainer { get; }

		void OnGeometryChange(GeometryChangedEvent evt)
        {
            closeButton.RegisterCallback<ClickEvent>(ev =>
            {
                if (UIObject != null)
                {
                    UnityEngine.Cursor.lockState = oldLockMode;
                    UnityEngine.Cursor.visible = oldMouseVisibility;
                    InputManager.Instance.playerInput.currentActionMap = oldActionMap;
                    Object.Destroy(UIObject);
				}
            });

            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }
    }
}