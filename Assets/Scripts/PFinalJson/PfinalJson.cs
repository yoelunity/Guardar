using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class PfinalJson : MonoBehaviour
{
    private byte[] _key = {0x01};
    private byte[] _initializationVector = {0x01};

    //Encriptamos el json con una key y una inicializacion
    byte[] Encrypt(string json) {
        //Generamos el mager
        AesManaged managed = new AesManaged();
        //generamos el encriptado con el manager y la key
        ICryptoTransform encryptor = managed.CreateEncryptor(_key, _initializationVector);

        MemoryStream mStream = new MemoryStream();
        CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write);
        StreamWriter sWriter = new StreamWriter(cStream);

        sWriter.WriteLine(json);

        mStream.Close();
        cStream.Close();
        sWriter.Close();

        return mStream.ToArray();
    }
    
    //Desencriptado
    string Decrypt(string encryptedJson) {
        AesManaged managed = new AesManaged();

        ICryptoTransform encryptor = managed.CreateDecryptor(_key, _initializationVector);

        MemoryStream mStream = new MemoryStream(encryptedJson.ToCharArray());
        CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Read);
        StreamReader sreader = new StreamReader(cStream);

        string json = sreader.ReadToEnd();

        mStream.Close();
        cStream.Close();
        sreader.Close();

        return json;
    }
    
    
    
    
    
    
    
    public struct Jugador
    {
        //nombre de cada jugador
        public string nombre;
        //posicion individual de cada ficha, sindo 0 la ficha en estado muerta
        public int[] pos;
        //Si la booleana esta en true, el jugador en cuestion tiene el turno
        public int turno;
    }
    
    public List<Jugador> jugadores;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            string json = Encrypt(JsonHelper.ToJson(jugadores.ToArray())).ToString();
            string savePath = Application.persistentDataPath + "/Savejson.yikes";
            StreamWriter sw = new StreamWriter(savePath);
            sw.WriteLine(json);
            sw.Close();
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            string savepath =  Application.persistentDataPath + "/Savejson.yikes";
            StreamReader sr = new StreamReader(savepath);
            jugadores = Decrypt(JsonHelper.FromJson<Jugador>(sr.ReadLine()).);
            sr.Close();
            
        }
    }
    
    //Clase que permite convertir arrays a tipo json
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }
        
        public static string ToJson<T>(T[] array, bool prettyPrint = false)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }
        
        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
