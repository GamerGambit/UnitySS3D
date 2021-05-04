using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.UI
{
	public class SuitStorageUnit : VisualElement
    {
        /// <summary>
        /// Component which spawned this UI
        /// </summary>
        public Component InstanceComponent;

        private bool locked = false;
        private bool open = false;

        private FAButton lockUnlock;
        private FAButton openClose;
        private FAButton disinfect;
        private VisualElement storage;
        private Button helmetSlot;
        private Button suitSlot;
        private Button maskSlot;
        private Button storageSlot;

        public new class UxmlFactory : UxmlFactory<SuitStorageUnit, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
            }
        }

        public SuitStorageUnit()
        {
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        void OnGeometryChange(GeometryChangedEvent evt)
        {
            lockUnlock = this.Q<FAButton>("Lock");
            openClose = this.Q<FAButton>("Open");
            disinfect = this.Q<FAButton>("Disinfect");
            storage = this.Q("Storage");
            helmetSlot = this.Q<Button>("HelmetSlot");
            suitSlot = this.Q<Button>("SuitSlot");
            maskSlot = this.Q<Button>("MaskSlot");
            storageSlot = this.Q<Button>("StorageSlot");

            lockUnlock.Click += (e) =>
            {
                locked = !locked;

                if (locked)
                {
                    openClose.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
                    disinfect.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);

                    lockUnlock.faCode = "f3c1";
                    lockUnlock.buttonText = "Unlock";
                }
                else
                {
                    openClose.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                    disinfect.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

                    lockUnlock.faCode = "f023";
                    lockUnlock.buttonText = "Lock";
                }
            };

            openClose.Click += (e) =>
            {
                open = !open;

                if (open)
                {
                    storage.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                    lockUnlock.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
                    disinfect.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);

                    openClose.faCode = "f2f5";
                    openClose.buttonText = "Close";
                }
                else
				{
                    storage.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
                    lockUnlock.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                    disinfect.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

                    openClose.faCode = "f2f6";
                    openClose.buttonText = "Open";
                }
            };

            UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }
    }
}
