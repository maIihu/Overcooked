
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenu, Game, Loading
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
