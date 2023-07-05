using UnityEngine;

public class AbilityDirectionIndicator : MonoBehaviour
{
    [SerializeField] private AbilityButton _button;
    [SerializeField] private Transform _indicator;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _head;

    [SerializeField] private Transform _target;
    [SerializeField] private float _length;

    void Awake()
    {
        _indicator.gameObject.SetActive(false);

        // update the length of the body and position of the head
        var newScale = _body.transform.localScale;
        newScale.y = _length;
        _body.transform.localScale = newScale;

        var newPosition = _body.transform.position;
        newPosition.z = _length / 2;
        _body.transform.position = newPosition;

        var newHeadPosition = _head.transform.position;
        newHeadPosition.z = _length;
        _head.transform.position = newHeadPosition;
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
    }

    private void OnReleased(bool arg0)
    {
        _indicator.gameObject.SetActive(false);
    }

    private void OnBeginInteraction()
    {
        _indicator.rotation = _target.transform.rotation;
        _indicator.gameObject.SetActive(true);
    }
}
