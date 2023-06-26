using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float MaxHP;
    public float CurrentHP;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Transform _canvas;

    void Start()
    {
        CurrentHP = MaxHP;
    }

    public async void ReduceHP(float hp)
    {
        if (CurrentHP == 0) return;
        var label = Instantiate(_label);
        label.transform.SetParent(_canvas);
        label.transform.position = new Vector3(0, 2.5f);
        label.transform.rotation = Quaternion.LookRotation(label.transform.position - Camera.main.transform.position);
        label.gameObject.SetActive(true);
        label.text = $"{hp}";

        CurrentHP -= hp;
        CurrentHP = Mathf.Max(CurrentHP, 0);
        await Task.Delay(2000);
        if (label)
        {
            Destroy(label.gameObject);
        }

    }


}
