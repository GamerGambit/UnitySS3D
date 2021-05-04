using System;
using System.Collections.Generic;
using System.Globalization;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Assets.UI
{
	public class FAButton : VisualElement
	{
        private readonly Label faLabel = new Label();
        private readonly Button button = new Button();

        public new class UxmlFactory : UxmlFactory<FAButton, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription faCode = new UxmlStringAttributeDescription { name = "fa-code", defaultValue = "f641" };
            UxmlStringAttributeDescription btnText = new UxmlStringAttributeDescription { name = "button-text", defaultValue = "Button" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var ate = ve as FAButton;
                ate.faCode = faCode.GetValueFromBag(bag, cc);
                ate.buttonText = btnText.GetValueFromBag(bag, cc);
            }
        }

        public string faCode {
            get => faLabel.text;
            set
            {
                if (int.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var codepoint))
                {
                    faLabel.text = char.ConvertFromUtf32(codepoint);
                }
                else
                {
                    faLabel.text = value;
                }
            }
        }

        public string buttonText
        {
            get => button.text;
            set => button.text = value;
        }

        public event Action<EventBase> Click;

        public FAButton()
        {
            button.style.paddingLeft = new StyleLength(new Length(20, LengthUnit.Pixel));
            button.style.paddingRight = new StyleLength(new Length(5, LengthUnit.Pixel));
            Add(button);

            faLabel.AddToClassList("fa");
            faLabel.focusable = false;
            faLabel.pickingMode = PickingMode.Ignore;
            faLabel.style.position = new StyleEnum<Position>(Position.Absolute);
            faLabel.style.left = new StyleLength(0.0f);
            faLabel.style.top = new StyleLength(0.0f);
            faLabel.style.paddingLeft = new StyleLength(new Length(5, LengthUnit.Pixel));
            faLabel.style.paddingRight = new StyleLength(new Length(0, LengthUnit.Pixel));
            faLabel.style.paddingTop = new StyleLength(new Length(3, LengthUnit.Pixel));
            faLabel.style.paddingBottom = new StyleLength(new Length(0, LengthUnit.Pixel));
            Add(faLabel);

            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
		}

        void OnGeometryChange(GeometryChangedEvent evt)
        {
            button.RegisterCallback<ClickEvent>(ev =>
            {
                ev.target = this;
                Click?.Invoke(ev);
            });

            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }
    }
}