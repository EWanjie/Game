using UnityEngine;
using UnityEngine.SceneManagement;


public class GoMainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void MenuGame()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

}
