using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;

    void Start()
    {
        // Set the slider range
        soundSlider.minValue = 0;
        soundSlider.maxValue = 100;

        // Set initial volume based on PlayerPrefs
        float savedVolume = PlayerPrefs.GetFloat("SavedMasterVolume", 100);
        SetVolume(savedVolume);
        RefreshSlider(savedVolume);

        // Update numberText with initial slider value
        SetNumbertext(savedVolume);

        // Attach the SetVolumeFromSlider method to the OnValueChanged event of the slider
        soundSlider.onValueChanged.AddListener(SetVolumeFromSlider);
    }

    public void SetVolume(float _value)
    {
        if (_value <= 0)
        {
            _value = 1f; // Set to a small non-zero value
        }

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);

        // Update the numberText with the new volume value
        SetNumbertext(_value);
    }

    public void SetVolumeFromSlider(float _value)
    {
        SetVolume(_value);
    }

    public void RefreshSlider(float _value)
    {
        soundSlider.value = _value;
    }

    public void SetNumbertext(float value)
    {
        numberText.text = value.ToString();
    }
}
