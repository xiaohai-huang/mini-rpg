using UnityEngine;

public class AbilityDirectionIndicator : MonoBehaviour
{
    [SerializeField] private AbilityButton _button;
    [SerializeField] private Transform _indicator;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _head;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _cancelColor;
    private Material _bodyMaterial;
    private Material _headMaterial;

    [SerializeField] private Transform _target;
    [SerializeField] private float _length;

    void Awake()
    {
        _indicator.gameObject.SetActive(false);

        // update the length of the body and position of the head
        var newScale = _body.transform.localScale;
        newScale.y = _length;
        _body.transform.localScale = newScale;

        var newPosition = _body.transform.localPosition;
        newPosition.z = _length / 2;
        _body.transform.localPosition = newPosition;

        var newHeadPosition = _head.transform.localPosition;
        newHeadPosition.z = _length;
        _head.transform.localPosition = newHeadPosition;

        _bodyMaterial = _body.GetComponent<MeshRenderer>().material;
        _headMaterial = _head.GetComponent<MeshRenderer>().material;
    }

    void OnEnable()
    {
        _button.OnBeginInteraction += OnBeginInteraction;
        _button.OnMoving += OnMoving;
        _button.OnReleased += OnReleased;
    }

    void OnDisable()
    {
        _button.OnBeginInteraction -= OnBeginInteraction;
        _button.OnMoving -= OnMoving;
        _button.OnReleased -= OnReleased;
    }

    private void OnMoving()
    {
        var dir = new Vector3(_button.Direction.x, 0, _button.Direction.y);
        _indicator.rotation = Quaternion.LookRotation(dir, Vector3.up);
        // turn red if the player is trying to cancel the action
        if (_button.Cancelling)
        {
            _bodyMaterial.color = _cancelColor;
            _headMaterial.color = _cancelColor;
        }
        else
        {
            _bodyMaterial.color = _activeColor;
            _headMaterial.color = _activeColor;
        }
    }

    private void OnReleased(bool arg0)
    {
        _indicator.gameObject.SetActive(false);
        _bodyMaterial.color = _activeColor;
        _headMaterial.color = _activeColor;
    }

    private void OnBeginInteraction()
    {
        _indicator.rotation = _target.transform.rotation;
        _indicator.gameObject.SetActive(true);
    }
}
