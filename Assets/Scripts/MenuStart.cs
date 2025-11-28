using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    [SerializeField] private GameObject menuStart;
    [SerializeField] private GameObject menuOptions;
    [SerializeField] private GameObject menuCredits;

    [SerializeField] private GameObject btnPlay;
    [SerializeField] private GameObject btnOptions;
    [SerializeField] private GameObject btnCredits;

    [SerializeField] private GameObject  btnReturnOptions;
    [SerializeField] private GameObject btnReturnCredits;

    private void Start()
    {
        StartMenuButton();
        SelectButton(btnPlay);
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteKey("checkpointX");
        PlayerPrefs.DeleteKey("checkpointY");
        TransitionSceneUI.instance.DissolveExit(SceneManager.GetActiveScene().buildIndex + 1);
      
    }

    public void ExitGame()
    {
                Application.Quit();
    }

    public void StartMenuButton()
    {
         menuCredits.SetActive(false);
        menuOptions.SetActive(false);
        menuStart.SetActive(true);
    }
    public void OpenOptions()
    {
        menuStart.SetActive(false);
        menuOptions.SetActive(true);
        menuCredits.SetActive(false);

        SelectButton(btnReturnOptions);
    }

    public void OpenCredits()
    {
        menuStart.SetActive(false);
        menuOptions.SetActive(false);
        menuCredits.SetActive(true);
        SelectButton(btnReturnCredits);
    }

    private void SelectButton(GameObject button)
    {

        if(EventSystem.current == null) return;

       EventSystem.current.SetSelectedGameObject(null);
       EventSystem.current.SetSelectedGameObject(button);
        
    }
}
