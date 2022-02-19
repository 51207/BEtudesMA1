using UnityEngine;
using System;
//using M2MqttUnity;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Collections.ObjectModel;
//using System.Text.Json;
using System.IO;
using Newtonsoft.Json;

public class MQTTxyz : MonoBehaviour
{
   

    void Start()
    {
        mqttmethods();
    }

    void Update()
    {
       

    }

    public  void mqttmethods()
    {

        //Localhost
         MqttClient mymqttclient = new MqttClient("localhost");
        //MqttClient mymqttclient = new MqttClient("172.30.4.44");
        Console.WriteLine("========MQTT ======");

        mymqttclient.MqttMsgPublishReceived += clientreceptionMsg;
        //inscription au broker
        //  mymqttclient.Subscribe(new string[] { "tags" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        
        //creation du client
        string clientId = Guid.NewGuid().ToString();

        //   mymqttclient.Connect(clientId, "ali", "isib", true, keepAlivePeriod: 500);
        mymqttclient.Connect(clientId);
        // mymqttclient.Subscribe(new string[] { "tag26886/" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        mymqttclient.Subscribe(new string[] { "tags" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

       
        //messsage qui affiche les deux tags
        Console.WriteLine("Subscriber:tags");



    }
     void deserilize_position( Collection<JsonPozyxTags.Example> todo)
    {
        foreach (var item in todo)
        {

            Tag.TaginX1 = item.data.coordinates.x;
            Tag.TaginY1 = item.data.coordinates.y;
            Tag.TaginZ1 = item.data.coordinates.z;
        }
    }
     void deserilize_valueGiroscope( string value, Collection<JsonPozyxTags.Example> todo)
    {

        foreach (var item in todo)
        {

            Double[] Giroscopevalue = JsonConvert.DeserializeObject<Double[]>(item.data.tagData.sensors[0].value.ToString());
            //JsonSerializer.Deserialize<Double[]>(item.data.tagData.sensors[0].value.ToString());

            Tag.TagOrientX1 = Giroscopevalue[0];
            Tag.TagOrientY1 = Giroscopevalue[1];
            Tag.TagOrientZ1 = Giroscopevalue[2];
        }
    }
    static void clientreceptionMsg(object sender, MqttMsgPublishEventArgs e)
    {//  utf8Encoding = initialise une nouvelle instance de classe a
    // Debug.Log("Message re�ue :  " + "  " + System.Text.Encoding.Default.GetString(e.Message));

       
        string path = @"C:\Users\aliou\OneDrive\Bureau\fichier_bureau_etudes.txt";
        if (File.Exists(path))
        {//si le fichier existe

            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                string s = System.Text.Encoding.Default.GetString(e.Message);
                
                //j'�cris dans le fichier  
                sw.WriteLine(s);

            }
        }


        // Open the file to read from.
        using (StreamReader sr = File.OpenText(path))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {

                Collection<JsonPozyxTags.Example> todo = JsonConvert.DeserializeObject<Collection<JsonPozyxTags.Example>>(File.ReadAllText(path));
                //JsonSerializer.Deserialize<Collection<JsonPozyxTags.Example>>(File.ReadAllText(path));
                
               
                MQTTxyz p = new MQTTxyz();

                foreach (var item in todo)
                {



                    if (item.data.coordinates is null && item.data.tagData.sensors is null)
                    {

                    }
                    else
                    {



                        if (item.data.coordinates is null && !(item.data.tagData.sensors is null))
                        {
                          

                            Debug.Log(item.data.tagData.sensors[0].name);
                           //TagOrientX1 est une variable statique: pour l'appeler ,on fait class.lenomdelavariable
                            Debug.Log(Tag.TagOrientX1);
                            Debug.Log(Tag.TagOrientY1);
                            Debug.Log(Tag.TagOrientZ1);
                            Debug.Log("*****************************************");

                           //une deuxi�me deserialisation pour obtenir la valeur 
                            p.deserilize_valueGiroscope(item.data.tagData.sensors[0].value.ToString(), todo);

                        }


                        if (!(item.data.coordinates is null) && item.data.tagData.sensors is null)
                        {
                          //afficher les positions

                            Debug.Log("x =" + item.data.coordinates.x + "y=" + item.data.coordinates.y + "z=" + item.data.coordinates.z);
                            Debug.Log("*****************************************");
                            //deserialisation totale
                            p.deserilize_position( todo);
                        }


                        if (!(item.data.coordinates is null) && !(item.data.tagData.sensors is null))
                        {
                            
                           Debug.Log("x =" + item.data.coordinates.x + "y=" + item.data.coordinates.y + "z=" + item.data.coordinates.z);
                            Debug.Log(item.data.tagData.sensors[0].name);
                            //une deuxi�me deserialisation pour obtenir la valeur 
                            p.deserilize_valueGiroscope( item.data.tagData.sensors[0].value.ToString(), todo);
                            Debug.Log(Tag.TagOrientX1);
                            Debug.Log(Tag.TagOrientY1);
                            Debug.Log(Tag.TagOrientZ1);

                            Debug.Log("*****************************************");

                         //deserialisation totale
                            p.deserilize_position(todo);
                        }






                    }


                }
            }
        }

    }
}
