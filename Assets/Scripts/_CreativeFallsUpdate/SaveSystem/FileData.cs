using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FileData
{	
	public Bk[] blocks = new Bk[1000];
}

public struct Bk{
	public bool isSet;
	//Transform
	public Vector3 pos;
	public Vector3 rot;
	public Vector3 scl;

	//Location (To init GameObject onload map)
	public int groupId;
	public int globId;
	//if this obj button - link to act obj | string can have "", but int primary 0
	public string linkId;
	public Bk(Vector3 pos, Vector3 rot, Vector3 scl, int gri, int gli, string linkId){
		this.pos = pos;
		this.rot = rot;
		this.scl = scl;
		this.groupId = gri;
		this.globId = gli;
		this.linkId = linkId;
		this.isSet = true;
	}
}
