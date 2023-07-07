using UnityEngine;

public class AbilityDirectionIndicator : MonoBehaviour
{
    [SerializeField] private AbilityButton _button;
    [SerializeField] private Transform _arrowIndicator;
    [SerializeField] private Transform _rangeIndicator;
    [SerializeField] private Transform _arrowBody;
    [SerializeField] private Transform _arrowHead;
    [SerializeField] private Transform _circle;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _cancelColor;
    private Material _arrowBodyMaterial;
    private Material _arrowHeadMaterial;
    private Material _rangeIndicatorMaterial;

    [SerializeField] private Transform _target;
    [SerializeField] private float _length;

    void Awake()
    {
        _arrowIndicator.gameObject.SetActive(false);

        // update the length of the body and position of the head
        var newScale = _arrowBody.transform.localScale;
        newScale.y = _length;
        _arrowBody.transform.localScale = newScale;

        var newPosition = _arrowBody.transform.localPosition;
        newPosition.z = _length / 2;
        _arrowBody.transform.localPosition = newPosition;

        var newHeadPosition = _arrowHead.transform.localPosition;
        newHeadPosition.z = _length;
        _arrowHead.transform.localPosition = newHeadPosition;

        // update the size of the range indicator
        _circle.transform.localScale = new Vector3(_length * 2, _length * 2, 1);


        _arrowBodyMaterial = _arrowBody.GetComponent<MeshRenderer>().material;
        _arrowHeadMaterial = _arrowHead.GetComponent<MeshRenderer>().material;
        _rangeIndicatorMaterial = _circle.GetComponent<MeshRenderer>().material;
    }

    void OnEnable()
    {
        _button.OnBeginInteraction += OnBeginInteraction;
        _button.OnMoving += OnMoving;
        _button.OnReleased += OnReleased;
        _button.OnCancellingChanged += OnCancellingChanged;
    }

    void OnDisable()
    {
        _button.OnBeginInteraction -= OnBeginInteraction;
        _button.OnMoving -= OnMoving;
        _button.OnReleased -= OnReleased;
        _button.OnCancellingChanged -= OnCancellingChanged;
    }

    private void OnMoving()
    {
        var dir = new Vector3(_button.Direction.x, 0, _button.Direction.y);
        _arrowIndicator.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }

    private void OnReleased(bool arg0)
    {
        _arrowIndicator.gameObject.SetActive(false);
        _rangeIndicator.gameObject.SetActive(false);
    }

    private void OnBeginInteraction()
    {
        _arrowIndicator.rotation = _target.transform.rotation;
        _arrowIndicator.gameObject.SetActive(true);
        _rangeIndicator.gameObject.SetActive(true);
    }

    private void OnCancellingChanged(bool cancelling)
    {
        // turn red if the player is trying to cancel the action
        if (cancelling)
        {
            _arrowBodyMaterial.color = _cancelColor;
            _arrowHeadMaterial.color = _cancelColor;
            _rangeIndicatorMaterial.SetColor("_Color", _cancelColor);
        }
        else
        {
            _arrowBodyMaterial.color = _activeColor;
            _arrowHeadMaterial.color = _activeColor;
            _rangeIndicatorMaterial.SetColor("_Color", _activeColor);
        }
    }
}
