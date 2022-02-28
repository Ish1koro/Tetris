using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Buttonのシーン移動
/// </summary>
public class ButtonSystem : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void BackTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }
    public void Option()
    {
        SceneManager.LoadScene("Option");
    }
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
