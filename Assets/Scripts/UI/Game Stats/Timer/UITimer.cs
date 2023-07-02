using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITimer : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private float _timeElapsed;
    private bool _isStopped;
    private const float UI_UPDATE_INTERVAL = 1f;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void StartTimer()
    {
        _isStopped = false;
        InvokeRepeating(nameof(UpdateText), 0f, UI_UPDATE_INTERVAL);
    }

    public void StopTimer()
    {
        _isStopped = true;
        CancelInvoke(nameof(UpdateText));
    }

    public void ResetTimer()
    {
        _timeElapsed = 0;
    }

    private void UpdateText()
    {
        int minutes = Mathf.FloorToInt(_timeElapsed / 60);
        int seconds = Mathf.FloorToInt(_timeElapsed % 60);
        _text.text = minutes.ToString() + ":" + seconds.ToString("D2");
    }

    private void Update()
    {
        if (!_isStopped)
        {
            _timeElapsed += Time.deltaTime;
        }
    }
}
