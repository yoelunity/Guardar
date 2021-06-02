using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataWPlayerP : MonoBehaviour
{
    public int nivel;
    public string nombre;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("Nombre", "Yoel");
        PlayerPrefs.SetInt("Nivel",20);
        nivel = PlayerPrefs.GetInt("Nivel");
        nombre = PlayerPrefs.GetString("Nombre");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
