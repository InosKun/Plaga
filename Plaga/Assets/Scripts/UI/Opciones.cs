using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum BotonesOpts { Opts_Volumen,
Opts_Controles,
Opts_Volver,
Opts_TotalBotones }

public class Opciones : MonoBehaviour
{

    //Make sure to attach these Buttons in the Inspector
     [SerializeField]
     string[] nombreBoton = {
        "Audio",
        "Controles",
        "Volver"};

      Button[] boton;

    void Start () {
        boton = new Button [(int) BotonesOpts.Opts_TotalBotones];

        for (int i = (int)BotonesOpts.Opts_Volumen; 
        i < (int)BotonesOpts.Opts_TotalBotones;
        i++)
        boton[i] = GameObject.Find(nombreBoton[i]).GetComponent<Button>();


        boton[(int)BotonesOpts.Opts_Volumen].onClick.AddListener(volumenClicked);
        boton[(int)BotonesOpts.Opts_Controles].onClick.AddListener(controlesClicked); 
        boton[(int)BotonesOpts.Opts_Volver].onClick.AddListener(volverClicked);
        }


    void volverClicked()
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


    void volumenClicked() {
//Output this to console when the Button is clicked
       Debug.Log("Boton Volumen presionado");
       SceneManager.LoadScene("Volumen");
    }  

     void controlesClicked() {
      //Output this to console when the Button is clicked
      Debug.Log("Boton controles presionado");
      SceneManager.LoadScene("Controles");
    }  

     void Update() {
      //Regla del escape
      if (Input.GetKey("escape"))
        volverClicked();
      //Regla del enter
      if (Input.GetKey(KeyCode.Return))
        volumenClicked();
     }

}


