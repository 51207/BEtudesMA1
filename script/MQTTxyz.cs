using UnityEngine;
using System;
//using M2MqttUnity;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

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

                tag.TaginX1 = item.data.coordinates.x;
                tag.TaginY1 = item.data.coordinates.y;
                tag.TaginZ1 = item.data.coordinates.z;
            }
        }
            public void deserilize_valueGiroscope(Tag tag, string value, Collection<JsonPozyxTags.Example> todo)
        {

            foreach (var item in todo)
            {

                Double[] Giroscopevalue = JsonSerializer.Deserialize<Double[]>(item.data.tagData.sensors[0].value.ToString());

                tag.TagOrientX1 = Giroscopevalue[0];
                tag.TagOrientY1 = Giroscopevalue[1];
                tag.TagOrientZ1 = Giroscopevalue[2];
            }
        }

    public  void mqttmethods()
    {

        //Localhost
        // MqttClient mymqttclient = new MqttClient("localhost");
        MqttClient mymqttclient = new MqttClient("172.30.4.44");
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


        /*//MqttClient mymqttclient = new MqttClient("localhost");
        MqttClient mymqttclient = new MqttClient("172.30.4.44");
        Debug.Log("========welcom ASTROROAD99 ======");

        mymqttclient.MqttMsgPublishReceived += clientreceptionMsg;
        //inscription au broker
        mymqttclient.Subscribe(new string[] { "tag26886/" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
       // mymqttclient.Subscribe(new string[] { "tags" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

        //creation du client
        string clientId = Guid.NewGuid().ToString();
        
        mymqttclient.Connect(clientId, "ali", "isib", true, keepAlivePeriod: 5);

        //messsage qui affiche les deux tags
        Debug.Log("Subscriber:tag1/");
       */


    }
    static void clientreceptionMsg(object sender, MqttMsgPublishEventArgs e)
    {//  utf8Encoding = initialise une nouvelle instance de classe a
     //Debug.Log("Message re�ue :  " + "  " + System.Text.Encoding.Default.GetString(e.Message));

        //string source = "TESTMQTT";
      /*  FileStream filemanip = new FileStream(source, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter writertext = new StreamWriter(filemanip);
        var msg = UTF8Encoding.UTF8.GetString(e.Message);
        var rr = msg;
        writertext.WriteLine(rr);
        writertext.Close();*/

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
                    Collection<JsonPozyxTags.Example> todo = JsonSerializer.Deserialize<Collection<JsonPozyxTags.Example>>(File.ReadAllText(path));
                    //	File.ReadAllText(path): lit tout le texte qui se trouve dans le fichier et le ferme
                    Tag tag = new Tag();
                    Program p = new Program();

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
                               /* tag.TaginX1 = item.data.coordinates.x;
                                tag.TaginY1 = item.data.coordinates.y;
                                tag.TaginZ1 = item.data.coordinates.z;*/
                                p.deserilize_position(tag, todo);
                            }
                               

                                if (!(item.data.coordinates is null) && !(item.data.tagData.sensors is null))
                                {
                                    //  Console.WriteLine(item.data.tagData.sensors[0].value);
                                    Console.WriteLine("x =" + item.data.coordinates.x + "y=" + item.data.coordinates.y + "z=" + item.data.coordinates.z);
                                    Console.WriteLine(item.data.tagData.sensors[0].name);
                                    Console.WriteLine(JsonSerializer.Deserialize<Double[]>(item.data.tagData.sensors[0].value.ToString()));
                                // Double[] Giroscopevalue = JsonSerializer.Deserialize<Double[]>(item.data.tagData.sensors[0].value.ToString());
                                // tag.TagOrientX1 = Giroscopevalue[0];
                                //tag.TagOrientY1 = Giroscopevalue[1];
                                //tag.TagOrientZ1 = Giroscopevalue[2];
                                p.deserilize_valueGiroscope(tag, item.data.tagData.sensors[0].value.ToString(), todo);

                                Console.WriteLine("*****************************************");

                                /*  tag.TaginX1 = item.data.coordinates.x;
                                  tag.TaginY1 = item.data.coordinates.y;
                                  tag.TaginZ1 = item.data.coordinates.z;*/
                                p.deserilize_position(tag, todo);
                            }
                               

                  



                        }

                      
                    }
                }
            }

    }
}
