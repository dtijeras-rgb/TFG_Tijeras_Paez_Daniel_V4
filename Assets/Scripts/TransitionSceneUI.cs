using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSceneUI : MonoBehaviour
{
    public CanvasGroup dissolvedCanvasGroup;
    public float durationDissolve;
    public float durationDissolveExit;
    public static TransitionSceneUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        dissolvedCanvasGroup.alpha = 1f;
        dissolvedCanvasGroup.interactable = true;
        dissolvedCanvasGroup.blocksRaycasts = true;
        DissolveEnter();
    }

    private void DissolveEnter()
    {
        
        LeanTween.alphaCanvas(dissolvedCanvasGroup, 0f, durationDissolve).setOnComplete(() =>
        {
            dissolvedCanvasGroup.interactable = false;
            dissolvedCanvasGroup.blocksRaycasts = false;
        });
    }

    public void DissolveExit(int indexScene)
    {
        dissolvedCanvasGroup.interactable = true;
        dissolvedCanvasGroup.blocksRaycasts = true;
        LeanTween.alphaCanvas(dissolvedCanvasGroup, 1f, durationDissolveExit).setOnComplete(() =>
        {
            SceneManager.LoadScene(indexScene);
        });
    }
}
