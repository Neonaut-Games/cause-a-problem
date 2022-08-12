using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance;

    private void Awake()
    {
        if (Instance != null) return;
        
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    
}
