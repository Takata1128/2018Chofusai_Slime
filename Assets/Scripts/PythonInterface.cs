using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System;


public class PythonInterface : MonoBehaviour {

    // ポート指定 python 側と合わせる
    int Port = 22222;

    // 通信用
    static UdpClient udp;
    Thread thread;

    // 半径
    public static float radius_ori;

    void Start()
    {
        // udp 設定
        udp = new UdpClient(Port);
        udp.Client.ReceiveTimeout = 200;
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();

    }

    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        thread.Abort();
    }

    private static void ThreadMethod()
    {
        while (true)
        {
            // udp data 取得
            IPEndPoint remoteEP = null;
            byte[] data = udp.Receive(ref remoteEP);
            string text = Encoding.ASCII.GetString(data);

            float.TryParse(text, out radius_ori);

            Debug.Log(radius_ori);

        }
    }
}