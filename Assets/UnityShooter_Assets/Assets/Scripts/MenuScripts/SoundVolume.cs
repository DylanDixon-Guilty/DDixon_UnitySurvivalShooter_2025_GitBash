using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    //When player slides the Volume slide to the left, the volume lowers. When sliding to the right, the volume increases.
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void SetVolume()
    {
         volumeSlider.value = AudioListener.volume;
    }
}
