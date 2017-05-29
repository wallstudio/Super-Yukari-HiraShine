using UnityEngine;
using System.Collections;
using System.Text;
using System;

public class SubTransform {
    public string name;    //28byte
    public Vector3 pos;    //12byte
    public Vector3 rot;    //12byte
    public Vector3 vel;    //12byte
    public string msic;    //64byte
    public SubTransform() {
        name = "";
        pos = Vector3.zero;
        rot = Vector3.zero;
        vel = Vector3.zero;
        msic = "";
    }
    public SubTransform(byte[] bytes) {
        name = Encoding.ASCII.GetString(bytes, 0, 28);
        pos = getVector(bytes, 28);
        rot = getVector(bytes, 40);
        vel = getVector(bytes, 52);
        msic = Encoding.ASCII.GetString(bytes, 64, 64);
    }
    public SubTransform(Rigidbody rigidbody) {
        reflesh(rigidbody, rigidbody.name, "");
    }
    public SubTransform(Rigidbody rigidbody, string _name) {
        reflesh(rigidbody, _name, "");
    }
    public SubTransform(Rigidbody rigidbody, string _name, string _msic) {
        reflesh(rigidbody, _name, _msic);
    }
    public void reflesh(Rigidbody rigidbody, string _misic) {
        reflesh(rigidbody, name, _misic);
    }
    private void reflesh(Rigidbody rigidbody, string _name, string _msic) {
        name = _name;
        pos = rigidbody.transform.position;
        rot = rigidbody.transform.rotation.eulerAngles;
        vel = rigidbody.velocity;
        msic = _name;
    }
    public byte[] GetByte() {
        byte[] objectBuffer = new byte[128];
        Encoding.ASCII.GetBytes(name).CopyTo(objectBuffer, 0);
        GetBytes(pos).CopyTo(objectBuffer, 28);
        GetBytes(rot).CopyTo(objectBuffer, 40);
        GetBytes(vel).CopyTo(objectBuffer, 52);
        Encoding.ASCII.GetBytes(msic).CopyTo(objectBuffer, 64);
        return objectBuffer;
    }
    private byte[] GetBytes(Vector3 v3) {
        byte[] rtn = new byte[12];
        BitConverter.GetBytes(v3.x).CopyTo(rtn, 0);
        BitConverter.GetBytes(v3.y).CopyTo(rtn, 4);
        BitConverter.GetBytes(v3.z).CopyTo(rtn, 8);
        return rtn;
    }
    private Vector3 getVector(byte[] b, int startIndex) {
        float x = BitConverter.ToSingle(b, startIndex + 0);
        float y = BitConverter.ToSingle(b, startIndex + 4);
        float z = BitConverter.ToSingle(b, startIndex + 8);
        return new Vector3(x, y, z);
    }
}
