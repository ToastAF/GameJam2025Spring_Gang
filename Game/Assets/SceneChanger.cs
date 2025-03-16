using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Button changeSceneButton;
    public string sceneName;

    void Start()
    {
        // Add listener to the button
        if (changeSceneButton != null)
        {
            changeSceneButton.onClick.AddListener(ChangeScene);
        }
    }

    void ChangeScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
