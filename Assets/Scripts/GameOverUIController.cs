using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    // reference untuk button
    public Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        // setup event saat button di klik
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        // kembali ke main menu
        SceneManager.LoadScene("MainMenu");
    }
}
