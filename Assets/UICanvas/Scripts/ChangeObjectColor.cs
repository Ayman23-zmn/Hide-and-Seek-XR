using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeObjectColor : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private ToggleGroup toggleGroup;
    
    private ToggleDeselect[] toggleDeselects;
    private Material objectMaterial;

    private void Start()
    {
        objectMaterial = objectPrefab.GetComponent<MeshRenderer>().sharedMaterial;
        toggleDeselects = toggleGroup.GetComponentsInChildren<ToggleDeselect>();

        foreach (var toggle in toggleDeselects)
        {
            toggle.onValueChanged.AddListener(delegate { OnToggleValueChanged(toggle); });
        }
    }

    private void OnToggleValueChanged(Toggle changedToggle)
    {
        if (changedToggle.isOn)
        {
            objectMaterial.color = changedToggle.colors.normalColor;
        }
    }

}
