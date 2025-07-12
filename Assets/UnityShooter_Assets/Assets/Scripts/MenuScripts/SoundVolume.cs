using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    /// <summary>
    /// When player slides the Volume slide to the left, the volume lowers. When sliding to the right, the volume increases.
    /// </summary>
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    /// <summary>
    /// To Save the current volume so player does not to set volume every time game resets (while in play mode).
    /// </summary>
    public void SetVolume()
    {
         volumeSlider.value = AudioListener.volume;
    }
}
