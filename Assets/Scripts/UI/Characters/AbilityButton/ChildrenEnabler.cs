using System.Collections.Generic;
using UnityEngine;

public class ChildrenEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _children;
    public float Seconds = 0.5f;

    void Awake()
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            children.Add(child);
        }
        _children = children.ToArray();
    }

    void Start()
    {
        Invoke(nameof(EnableChildren), Seconds);
    }

    void EnableChildren()
    {
        for (int i = 0; i < _children.Length; i++)
        {
            _children[i].SetActive(true);
        }
    }
}
