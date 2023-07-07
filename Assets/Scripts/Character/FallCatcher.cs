using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCatcher : MonoBehaviour
{
    public void OnEnterZone(bool enter, GameObject go)
    {
        if (enter)
        {
            if (go.CompareTag("Player"))
            {
                go.SetActive(false);
                go.transform.position = new Vector3(0, 2, 0);
                go.SetActive(true);

            }
        }
    }
}
