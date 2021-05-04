using Assets.UI;

using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

public class Computer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ViewportPointToRay(Vector3.one / 2.0f);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);

        if (Physics.Raycast(ray, out var hitInfo, 2f) && Input.GetKeyDown("e"))
		{
            if (true)//if (hitInfo.collider.gameObject == this)
            {
                Debug.Log("HIT");
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
}
