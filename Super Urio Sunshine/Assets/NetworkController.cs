using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using System.Collections.Generic;

//
public class NetworkController : MonoBehaviour {

    //通信用プロパティ
    const int stateByteSize = 32768;
    const int maxConnections = 64;
    const int refleshRate = 30;
    const float interval = 1f / refleshRate * 0.9f;
    const int port = 6721;
    Socket client;
    Thread timerThread;

    //データ格納用プロパティ
    SubTransform[] sts = new SubTransform[stateByteSize / 128];
    //監視対象,1番目に操作キャラ
    List<Rigidbody> watchees = new List<Rigidbody>();

	// Use this for initialization
	void Start () {
        //データ初期化
        watchees.Add(GameObject.Find("MainChar").GetComponent<Rigidbody>());
        for(int i = 0; i < sts.Length; i++) { sts[i] = new SubTransform(); }
        //通信初期化・開始
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client.Connect("localhost",port);
        timerThread = new Thread(send);
        timerThread.Start();
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        //foreach(Rigidbody)
    }

    void OnDisable() {
        timerThread.Abort();
    }


    void send() {
        Thread.Sleep((int)(interval * 5000));
        while (true) {
            Thread.Sleep((int)(interval * 1000));
            byte[] buffer = new byte[stateByteSize];
            for(int i = 0; i < sts.Length; i++) {
                sts[i].GetByte().CopyTo(buffer, i * 128);
            }
            client.Send(buffer);
        }
    }
}
