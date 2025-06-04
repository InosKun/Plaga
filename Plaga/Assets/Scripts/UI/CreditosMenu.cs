using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum BotonCreditos {
Cred_Volver,
Cred_TotalBotones }

public class CreditosMenu : MonoBehaviour
{

    [SerializeField]
     string[] nombreBoton = {
        "Volver"
        };

      Button[] boton;
    // Start is called before the first frame update
    void Start()
    {
        boton = new Button [(int) BotonCreditos.Cred_TotalBotones];

        for (int i = (int)BotonCreditos.Cred_Volver; 
        i < (int)BotonCreditos.Cred_TotalBotones;
        i++)
        boton[i] = GameObject.Find(nombreBoton[i]).GetComponent<Button>();

        boton[(int)BotonCreditos.Cred_Volver].onClick.AddListener(volverrClicked);
        
    
        
    }

    // Update is called once per frame
    void volverrClicked()
    {
        if (PlayerPrefs.GetInt("FromPauseMenu", 0) == 1)  // Check if we came from the pause menu
        {
            PlayerPrefs.SetInt("FromPauseMenu", 0);  // Reset the value
            PlayerPrefs.Save();
            SceneManager.LoadScene("first person");  // Return to the first person scene
        }
        else
        {
            SceneManager.LoadScene("MenuPpal");  // Default behavior
        }
    }

}

