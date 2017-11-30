using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider MusicVolume;
    public Slider SoundVolume;
    public Text ControlsButtonText;

    void Start()
    {
        SetControlsButtonText();
        MusicVolume.value = GameState.MusicVolume;
        SoundVolume.value = GameState.SoundVolume;
    }

    public void ChangeControls()
    {
        SetControlsButtonText();
        Control.IsAlternative = !Control.IsAlternative;
    }

    private void SetControlsButtonText()
    {
        ControlsButtonText.text = Control.IsAlternative ? "Управление: Альт." : "Управление: Станд.";        
    }

    public void ChangeMusicVolume()
    {
        GameState.MusicVolume = MusicVolume.value;
    }

    public void ChangeSoundVolume()
    {
        GameState.SoundVolume = SoundVolume.value;
    }
}
