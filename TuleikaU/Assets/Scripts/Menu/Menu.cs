using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas Main;
    public Canvas Settings;

    public AudioClip ClickSound;
    public AudioClip SelectSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        EnterMain();
    }

    public void StartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void EnterSettings()
    {
        Main.enabled = false;
        Settings.enabled = true;
    }

    public void EnterMain()
    {
        Main.enabled = true;
        Settings.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlaySelectSound()
    {
        audioSource.PlayOneShot(SelectSound);
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(ClickSound);
    }
}
