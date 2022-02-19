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
     Debug.Log("Message reçue :  " + "  " + System.Text.Encoding.Default.GetString(e.Message));

        //string source = "TESTMQTT";
      /*  FileStream filemanip = new FileStream(source, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter writertext = new StreamWriter(filemanip);
        var msg = UTF8Encoding.UTF8.GetString(e.Message);
        var rr = msg;
        writertext.WriteLine(rr);
        writertext.Close();*/

    }
}
