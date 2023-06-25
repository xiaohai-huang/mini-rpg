using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Xiaohai.Input;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _objectNameText;
    // Start is called before the first frame update
    [HideInInspector] public Interactable HoverringObject;
    [SerializeField] private InputReader _inputReader;
    public Vector2 PointerPosition;

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
        PointerPosition = _inputReader.InputActions.GamePlay.PointerPosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(PointerPosition);
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
