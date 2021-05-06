using Assets.Scripts.UI;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
	public class Computer : MonoBehaviour, IInteractable
    {
        public void Interact(GameObject activator)
        {
            var go = new GameObject();
            var ui = go.AddComponent<UIDocument>();
            ui.panelSettings = AssetDatabase.LoadAssetAtPath<PanelSettings>("Assets/UI/PanelSettings.asset");
            ui.visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/SuitStorageUnit.uxml");

            var tgw = ui.rootVisualElement.Q<TGWindow>();
            tgw.UIObject = go;

            var ssu = ui.rootVisualElement.Q<SuitStorageUnit>();
            ssu.InstanceComponent = this;
        }
    }
}
