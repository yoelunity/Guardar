using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSW : MonoBehaviour
{
    public string _nombre = "Yoel";
    public int _nivel = 20;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            string savePath = Application.persistentDataPath + "/Save.yikes";
            StreamWriter sw = new StreamWriter(savePath);
            sw.WriteLine(_nombre);
            sw.WriteLine(_nivel);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            string savepath =  Application.persistentDataPath + "/Save.yikes";
            StreamReader sr = new StreamReader(savepath);
            sr.ReadLine();
            sr.ReadLine();
            sr.Close();
            
        }
    }
}
