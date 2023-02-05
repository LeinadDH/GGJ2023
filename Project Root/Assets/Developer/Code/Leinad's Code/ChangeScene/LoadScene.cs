using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string SceneName;

    public void SceneToLoad()
    {
        SceneManager.LoadScene(SceneName);
    }
}
