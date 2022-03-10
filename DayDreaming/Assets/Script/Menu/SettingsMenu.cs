using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Script pour le menu des paramètres
// @author Maxime Ginda
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer am;

    public TMPro.TMP_Dropdown resolutionDropDown;
    public Toggle togFS;

    private Resolution[] resolutions;
    private bool fs;

    public GameObject settingsWindow;
    public GameObject PauseMenu;


    public void Start()
    {
        togFS.isOn = Screen.fullScreen;

        resolutions = Screen.resolutions;
        
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResI = 0;

        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height){
                currentResI = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResI;
        resolutionDropDown.RefreshShownValue();
    }

    // Permet de modifier le volume du jeu
    public void SetVolume(float volume)
    {
        am.SetFloat("Volume", volume);   
    }

    // Permet de passer en plein écran ou non
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        fs = isFullScreen;

    }

    public void SetResolution(int valueOfDD){
         Screen.SetResolution(resolutions[valueOfDD].width,resolutions[valueOfDD].height,fs);
    }

    // Permet de fermer les settings du jeu 
    public void closeSettings()
    {

        if(PauseMenu != null)
            PauseMenu.SetActive(true);


        settingsWindow.SetActive(false);

    }

}
