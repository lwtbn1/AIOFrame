using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public class NetworkManager : MonoBehaviour
{

    private SocketClient socket;
    static readonly object m_lockObject = new object();
    static Queue<RecvData> recvQueue = new Queue<RecvData>();
    static Queue<SendData> sendQueue = new Queue<SendData>();
    public static Dictionary<uint, LuaFunction> recvCallBackDict = new Dictionary<uint, LuaFunction>();
    public static Dictionary<int, LuaFunction> foreverCallBackDict = new Dictionary<int, LuaFunction>();
    SocketClient SocketClient
    {
        get
        {
            if (socket == null)
                socket = new SocketClient();
            return socket;
        }
    }

    void Awake()
    {
        Init();
    }

    void Init()
    {
        SocketClient.OnRegister();
    }

    /// <summary>
    /// 执行Lua方法
    /// </summary>
    public object[] CallMethod(string func, params object[] args)
    {
        return Util.CallMethod("Network", func, args);
    }

    ///------------------------------------------------------------------------------------
    public static void AddEvent(RecvData data)
    {
        lock (m_lockObject)
        {
            recvQueue.Enqueue(data);
        }
    }

    /// <summary>
    /// 交给Command，这里不想关心发给谁。
    /// </summary>
    void Update()
    {
        if (recvQueue.Count > 0)
        {
            while (recvQueue.Count > 0)
            {
                RecvData data = recvQueue.Dequeue();
                //将消息发送给对应的逻辑
                if (recvCallBackDict.ContainsKey(data.ix))
                {
                    recvCallBackDict[data.ix].Call(data);
                    recvCallBackDict.Remove(data.ix);
                }
                int key = (int)data.cmd << 8 + data.funCode;
                if (foreverCallBackDict.ContainsKey(key))
                    foreverCallBackDict[key].Call(data);
            }
        }

        if (sendQueue.Count > 0)     //对于发送数据每一帧只处理一个消息
        {
            SendData data = sendQueue.Dequeue();
            socket.SendMessage(data.GetSendByte());
        }
    }

    /// <summary>
    /// 发送链接请求
    /// </summary>
    public void SendConnect(string ip, int port)
    {
        SocketClient.SendConnect(ip, port);
    }

    /// <summary>
    /// 发送SOCKET消息
    /// </summary>

    public void SendMessage(byte cmd, byte funCode, byte[] data, LuaFunction callBack)
    {
        lock (m_lockObject)
        {
            SendData sendData = new SendData(cmd, funCode, data, callBack);
            sendQueue.Enqueue(sendData);
        }
        
    }
    public void RegistForeverCallBack(int cmd, int funCode, LuaFunction callBack)
    {
        lock (m_lockObject)
        {
            int key = cmd << 8 + funCode;
            foreverCallBackDict.Add(key, callBack);
        }
    }
    /// <summary>
    /// 析构函数
    /// </summary>
    new void OnDestroy()
    {
        SocketClient.OnRemove();
        Debug.Log("~NetworkManager was destroy");
    }
}
