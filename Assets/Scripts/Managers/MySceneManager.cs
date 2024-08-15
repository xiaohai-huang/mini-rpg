using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public void LoadSceneAdditive(string sceneName, object callback)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        var cb = ReactUnity.Helpers.Callback.From(callback);

        operation.completed += (_) =>
        {
            cb.Call();
        };
    }
}
