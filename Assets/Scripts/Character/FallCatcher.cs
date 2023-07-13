using UnityEngine;

public class FallCatcher : MonoBehaviour
{
    public void OnEnterZone(bool enter, GameObject go)
    {
        if (enter)
        {
            if (go.CompareTag("Player"))
            {
                var controller = go.GetComponent<CharacterController>();
                controller.enabled = false;
                go.transform.position = new Vector3(0, 2, 0);
                controller.enabled = true;
            }
        }
    }
}
