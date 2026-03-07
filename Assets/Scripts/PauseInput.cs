using UnityEngine;

public class PauseInput : MonoBehaviour
{
    public GameObject menu;
    // Update is called once per frame
    void Update()
    {
    
    }

    public void Pause()
    {
        if(Time.timeScale != 0){
            menu.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            return;
        }
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
