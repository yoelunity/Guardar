using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

public class PracticaFinal : MonoBehaviour
{
    [Serializable]
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
    public void Update()
    {
        //Al pulsar la tecla i creamos la conexion con la base de datos guardada en el path estblecido
        if (Input.GetKeyDown(KeyCode.I))
        { 
            string conn = $"URI=file:{Application.streamingAssetsPath}/SistemaDeGuardadoParchis.db";
            IDbConnection dbConnection = new SqliteConnection(conn);
            dbConnection.Open();
            //Generamos un comando y le damos una orden
            IDbCommand idbCommanddel = dbConnection.CreateCommand();
            
            string sqldel = $"delete from Parchis";
            idbCommanddel.CommandText = sqldel;
            //Ejectuamops el comando
            idbCommanddel.ExecuteReader();
            
            //Con este for, se dan los valores establecidos en el inspector a cada uno de los jugadores de la partida y a sus fichas
            for (int i = 0; i < jugadores.Count ; i++)
            {
                IDbCommand idbCommand = dbConnection.CreateCommand();
                string sql = $"Insert into Parchis values('{jugadores[i].nombre}','{jugadores[i].pos[0]}'," +
                             $"'{jugadores[i].pos[1]}','{jugadores[i].pos[2]}','{jugadores[i].pos[3]}'," +
                             $"'{jugadores[i].turno}')";
                idbCommand.CommandText = sql;
                idbCommand.ExecuteReader();
                idbCommand.Dispose();
            }
            //Se cierra la conexion con la base de datos
            dbConnection.Close();
            
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //Conectamos con la database
            string conn = $"URI=file:{Application.streamingAssetsPath}/SistemaDeGuardadoParchis.db";
            IDbConnection dbConnection = new SqliteConnection(conn);
            dbConnection.Open();
            
            IDbCommand idbCommandsel = dbConnection.CreateCommand();
            
            string sqlsel = $"select * from parchis";
            idbCommandsel.CommandText = sqlsel;

            IDataReader reader = idbCommandsel.ExecuteReader();
            //Vaciamos la lista de jugadores para comprobar que funciona
            jugadores.Clear();
            //recorremos la database danod los valores encontrados a las variables establecidas en unity
            while (reader.Read())
            {
                jugadores.Add(new Jugador()
                {
                    nombre = reader.GetString(0),
                    pos = new int[]
                    {
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetInt32(3),
                        reader.GetInt32(4)
                    },
                   turno = reader.GetInt32(5)
                });
            }
            //Cerramos la conexion con la database
            reader.Close();
            idbCommandsel.Dispose();
            dbConnection.Close();
            
        }
    }
}

