using UnityEngine;
using System;
//using M2MqttUnity;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;

public class MQTTxyz : MonoBehaviour
{
   

    void Start()
    {
        mqttmethods();
    }

    void Update()
    {
       

    }

      public void deserilize_position(Tag tag, Collection<JsonPozyxTags.Example> todo)
        {
            foreach (var item in todo)
            {

                Tag.TaginX1 = item.data.coordinates.x;
                Tag.TaginY1 = item.data.coordinates.y;
                Tag.TaginZ1 = item.data.coordinates.z;
            }
        }
            public void deserilize_valueGiroscope(Tag tag, string value, Collection<JsonPozyxTags.Example> todo)
        {

            foreach (var item in todo)
            {

                Double[] Giroscopevalue = JsonConvert.DeserializeObject<Double[]>(item.data.tagData.sensors[0].value.ToString());

                Tag.TagOrientX1 = Giroscopevalue[0];
                Tag.TagOrientY1 = Giroscopevalue[1];
                Tag.TagOrientZ1 = Giroscopevalue[2];
            }
        }

    public  void mqttmethods()
    {

        //Localhost
        MqttClient mymqttclient = new MqttClient("localhost");
       // MqttClient mymqttclient = new MqttClient("172.30.4.44");
        Console.WriteLine("========MQTT ======");

        mymqttclient.MqttMsgPublishReceived += clientreceptionMsg;
        //inscription au broker
        //  mymqttclient.Subscribe(new string[] { "tags" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        //
        //creation du client
        string clientId = Guid.NewGuid().ToString();

        //   mymqttclient.Connect(clientId, "ali", "isib", true, keepAlivePeriod: 500);
        mymqttclient.Connect(clientId);
        // mymqttclient.Subscribe(new string[] { "tag26886/" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        mymqttclient.Subscribe(new string[] { "tags" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

        //m
        //messsage qui affiche les deux tags
        Console.WriteLine("Subscriber:tags");


       


    }
    static void clientreceptionMsg(object sender, MqttMsgPublishEventArgs e)
    {
     //Debug.Log("Message re�ue :  " + "  " + System.Text.Encoding.Default.GetString(e.Message));

     

        string path = @"C:\Users\aliou\OneDrive\Bureau\fichier_bureau_etudes.txt";
            if (File.Exists(path))
            {//si le fichier existe

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    string s = System.Text.Encoding.Default.GetString(e.Message);

                    sw.WriteLine(s);

                }
            }


            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {

                //string text = System.Text.Encoding.Default.GetString(e.Message);
                Collection<JsonPozyxTags.Example> todo = JsonConvert.DeserializeObject<Collection<JsonPozyxTags.Example>>(File.ReadAllText(path));
                //	File.ReadAllText(path): lit tout le texte qui se trouve dans le fichier et le ferme
                Tag tag = new Tag();
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
                                //  Console.WriteLine(item.data.tagData.sensors[0].value);
                                
                                Console.WriteLine(item.data.tagData.sensors[0].name);
                                Console.WriteLine(item.data.tagData.sensors[0].value);
                                Console.WriteLine("*****************************************");
                                /* Double[] Giroscopevalue = JsonSerializer.Deserialize<Double[]>(item.data.tagData.sensors[0].value.ToString());
                                 tag.TagOrientX1 = Giroscopevalue[0];
                                 tag.TagOrientY1 = Giroscopevalue[1];
                                 tag.TagOrientZ1 = Giroscopevalue[2];*/
                                p.deserilize_valueGiroscope(tag, item.data.tagData.sensors[0].value.ToString(), todo);


                            }
                               

                                if (!(item.data.coordinates is null) && item.data.tagData.sensors is null)
                                {
                                //  Console.WriteLine(item.data.tagData.sensors[0].value);

                                Console.WriteLine("x =" + item.data.coordinates.x + "y=" + item.data.coordinates.y + "z=" + item.data.coordinates.z);
                                Console.WriteLine("*****************************************");
                                Tag.TaginX1 = item.data.coordinates.x;
                                Tag.TaginY1 = item.data.coordinates.y;
                                Tag.TaginZ1 = item.data.coordinates.z;
                                p.deserilize_position(tag, todo);
                            }
                               

                                if (!(item.data.coordinates is null) && !(item.data.tagData.sensors is null))
                                {
                                  
                                    Console.WriteLine("x =" + item.data.coordinates.x + "y=" + item.data.coordinates.y + "z=" + item.data.coordinates.z);
                                    Console.WriteLine(item.data.tagData.sensors[0].name);
                                   
                             
                                p.deserilize_valueGiroscope(tag, item.data.tagData.sensors[0].value.ToString(), todo);

                                Console.WriteLine("*****************************************");

                                
                                p.deserilize_position(tag, todo);
                            Tag.TaginX1 = item.data.coordinates.x;
                            Tag.TaginY1 = item.data.coordinates.y;
                            Tag.TaginZ1 = item.data.coordinates.z;
                        }
                               

                  



                        }

                      
                    }
                }
            }

    }
}
