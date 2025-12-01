using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{

    [SerializeField] private GameObject btnPause;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private AudioSource ambientMusic;

    [SerializeField] private GameObject menuOptions;
    [SerializeField] private GameObject btnOptions;
    [SerializeField] private GameObject btnReturnOptions;
    
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        btnPause.SetActive(false);
        menuPause.SetActive(true);

        if (ambientMusic != null)
        {
            ambientMusic.Pause();
        }
        EventSystem.current.SetSelectedGameObject(null);
       EventSystem.current.SetSelectedGameObject(menuPause.transform.GetChild(0).gameObject);
    }

    public void Continue ()
    {
        isPaused = false;
        Time.timeScale = 1f;
        btnPause.SetActive(true);
        menuPause.SetActive(false);
        if (ambientMusic != null)
        {
            ambientMusic.UnPause();
        }
    }

    public void ExitGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (ambientMusic != null)
        {
            ambientMusic.Stop();
        }

        SceneManager.LoadScene("MenuStart");
       
    }

    public void RestartLevel()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (ambientMusic != null)
        {
            ambientMusic.Stop();
        }
        PlayerPrefs.DeleteKey("checkpointX");
        PlayerPrefs.DeleteKey("checkpointY");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenOptions()
    {

        
        if (ambientMusic != null)
        {
            ambientMusic.UnPause();
        }
        ambientMusic.Stop();
        btnPause.SetActive(false);
        menuPause.SetActive(false);
        menuOptions.SetActive(true);

        SelectButton(btnReturnOptions);
    }

    private void SelectButton(GameObject button)
    {

        if (EventSystem.current == null) return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);

    }
}
