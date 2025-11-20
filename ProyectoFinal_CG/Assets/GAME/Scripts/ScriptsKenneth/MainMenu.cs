using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject instructionsPanel;

    public void OnStartButton()
    {

        SceneManager.LoadScene("ScenaDuque");
    }

    public void OnInstructionsButton()
    {
        if (instructionsPanel != null)
            instructionsPanel.SetActive(true);
    }


    public void OnBackFromInstructions()
    {
        if (instructionsPanel != null)
            instructionsPanel.SetActive(false);
    }
}
