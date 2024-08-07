using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDefeatCount : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _blueTeamDefeatCountText;

    [SerializeField]
    private TextMeshProUGUI _redTeamDefeatCountText;
    private int _blueTeamDefeatCount;
    private int _redTeamDefeatCount;

    void Awake()
    {
        _blueTeamDefeatCountText.text = _blueTeamDefeatCount.ToString();
        _redTeamDefeatCountText.text = _redTeamDefeatCount.ToString();
    }

    public void IncreaseBlueTeamDefeatCount()
    {
        _blueTeamDefeatCount++;
        _blueTeamDefeatCountText.text = _blueTeamDefeatCount.ToString();
    }

    public void IncreaseRedTeamDefeatCount()
    {
        _redTeamDefeatCount++;
        _redTeamDefeatCountText.text = _redTeamDefeatCount.ToString();
    }
}
