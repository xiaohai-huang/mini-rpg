using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _objectNameText;
    // Start is called before the first frame update
    [HideInInspector] public Interactable HoverringObject;
   
    // Update is called once per frame
    void Update()
    {
        HoverringObject = GetHoverObject();
        UpdateObjectNameText(HoverringObject);

        if (Input.GetMouseButtonDown(0))
        {
            if (HoverringObject)
            {
                HoverringObject.Interact();
            }
        }
    }

    Interactable GetHoverObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // make sure the user did not click on the UI element
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            return hitInfo.transform.GetComponent<Interactable>();
        }
        else
        {
            return null;
        }
    }

    void UpdateObjectNameText(Interactable obj)
    {
        if (obj)
        {
            _objectNameText.text = obj.name;
        }
        else
        {
            _objectNameText.text = "empty";
        }
    }
}
