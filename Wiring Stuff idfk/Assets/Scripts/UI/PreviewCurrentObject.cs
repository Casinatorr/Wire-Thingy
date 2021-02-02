using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCurrentObject : MonoBehaviour
{
    public Placing placing;
    private GameObject currentModel;
    public TextMesh text;

    private void Start()
    {
        placing.onObjectSelectionChange += OnSelectionChange;
    }

    private void OnSelectionChange()
    {
        if (currentModel != null)
            Destroy(currentModel);
        currentModel = Instantiate(Placing.placeableObjects[placing.selectedObject].model, transform);
        currentModel.transform.localPosition = new Vector3(0, 0, 5);
        currentModel.transform.localEulerAngles = new Vector3(-20, -20, 0);
        text.text = currentModel.name;
    }
}
