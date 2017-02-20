using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LuaInterface;
public class SendData
{
    public static uint msgIx = 0;
    private static byte headLen = 14;
    private byte cmd;
    private byte funCode;
    private uint ix;
    private int dataLen;
    private byte[] data;


    public SendData(byte cmd, byte funCode, byte[] data, LuaFunction callBack)
    {
        
        this.cmd = cmd;
        this.funCode = funCode;
        this.data = data;
        this.ix = msgIx++;
        this.dataLen = this.data.Length;
        NetworkManager.recvCallBackDict.Add(this.ix, callBack);
    }

    public byte[] GetSendByte()
    {
        byte[] send = new byte[data.Length + headLen];
        MemoryStream stream = new MemoryStream(headLen);
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(this.dataLen + headLen);   //数据总长
        writer.Write(cmd);              //cmd
        writer.Write(funCode);          //funCode
        writer.Write(ix);               //ix
        writer.Write(this.dataLen);      //数据长度
        writer.Flush();
        writer.Close();
        byte[] headData = stream.ToArray();
        Array.Copy(headData, 0, send, 0, headData.Length);
        Array.Copy(data, 0, send, 6, data.Length);
        return send;
    }
}
