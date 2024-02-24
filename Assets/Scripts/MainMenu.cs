using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Элементы настроек")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider volumeSlider;

    private int resolution;
    private float volume;
    private int curWidth;
    private int curHeight;

    void Start()
    {   
        // получаем список всех поддерживаемых разрешений
        Resolution[] resolutions = Screen.resolutions;
        foreach (var res in resolutions)
            if (res.refreshRateRatio.ToString() == Screen.currentResolution.refreshRateRatio.ToString() && res.width >= 800)
                resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.width + "x" + res.height));
        LoadSettings();
    }

    // функция, привязанная к кнопке играть
    public void OnPlayButtonClick() {
        SaveSettings();
        SceneManager.LoadScene("Game");
    }

    // функция, привязанная к кнопке выход
    public void OnExitClick() {
        SaveSettings();
        Application.Quit();
    }

    // функция, сохраняющая выставленные настройки в PlayerPrefs
    private void SaveSettings() {
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }
    
    // функция, подгружающая настройки из PlayerPrefs
    private void LoadSettings() {
        resolution = PlayerPrefs.GetInt("Resolution", resolutionDropdown.options.Count - 1);
        resolutionDropdown.value = resolution;

        volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        volumeSlider.value = volume;
    }

    // применяем здесь только разрешение, громкость звука будет браться из PlayerPrefs 
    // и применяться к каждому источнику отдельно через дополнительный скрипт.

    // данная функция будет вызвана, когда произойдет выбор в выпадающем списке
    public void ApplySettings() {
        string selectedResolution = resolutionDropdown.options[resolutionDropdown.value].text;
        curWidth = int.Parse(selectedResolution.Split('x')[0]);
        curHeight = int.Parse(selectedResolution.Split('x')[1]);
        
        Screen.SetResolution(curWidth, curHeight, FullScreenMode.ExclusiveFullScreen);
        SaveSettings();
    }
}
