using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameMode : MonoBehaviour
{

    public GameObject taskPopup;

    public Button startButton, farmButton, jungleButton, safariButton;

    public SceneLoader loader;

    // Start is called before the first frame update
    void Start()
    {
        taskPopup.SetActive(false);

        farmButton.onClick.AddListener(() =>
        {
            taskPopup.SetActive(true);
            startButton.onClick.AddListener(loader.LoadFarmAnimalGame);
        });
        jungleButton.onClick.AddListener(() =>
        {
            taskPopup.SetActive(true);
            startButton.onClick.AddListener(loader.LoadJungleAnimalGame);
        });
        safariButton.onClick.AddListener(() =>
        {
            taskPopup.SetActive(true);
            startButton.onClick.AddListener(loader.LoadSafariAnimalGame);
        });
    }

    public void OpenTaskPopup()
    {
        taskPopup.SetActive(true);
    }


}
