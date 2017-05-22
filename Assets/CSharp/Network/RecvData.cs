using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class RecvData
{
    public byte cmd;
    public byte funCode;
    public uint ix;
    public byte[] data;

    public static RecvData ParseRecv(ByteBuffer buffer)
    {
        RecvData recvData = new RecvData();
        recvData.cmd = buffer.ReadByte();
        recvData.funCode = buffer.ReadByte();
        recvData.ix = buffer.ReadUInt();
        recvData.data = buffer.ReadBytes();

        return recvData;
    }
}
