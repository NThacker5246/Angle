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

	//Colorful if this - block
	public bool isBlock;
	public Color32 col;

	//light if this - light
	public bool isLight;
	public float intensity;
	public float range;
	public float spotAngle;

	//trigger setup
	public bool isT;
	public Vector3 thisPosition;
	public Vector3 newPosition;
	public int speed;
	public int[] ids;
	public string id;


	public Bk(Vector3 pos, Vector3 rot, Vector3 scl, int gri, int gli, string linkId, Color32 col, float intensity, float range, float spotAngle, Vector3 thisPosition, Vector3 newPosition, int speed, int[] ids, string id){
		this.pos = pos;
		this.rot = rot;
		this.scl = scl;
		this.groupId = gri;
		this.globId = gli;
		this.linkId = linkId;
		this.isSet = true;
		if(this.groupId == 0 && this.globId == 0){
			this.isBlock = true;
			this.col = col;
		} else {
			this.col = new Color32(0, 0, 0, 0);
			this.isBlock = false;
		}

		if(this.groupId == 3) {
			this.isLight = true;
			this.intensity = intensity;
			this.range = range;
			this.spotAngle = spotAngle;
		} else {
			this.isLight = false;
			this.intensity = 0;
			this.range = 0;
			this.spotAngle = 0;
		}

		if(this.groupId == 4){
			this.isT = true;
			this.thisPosition = thisPosition;
			this.newPosition = newPosition;
			this.speed = speed;
			this.id = id;
			this.ids = ids;
		} else {
			this.isT = false;
			this.thisPosition = thisPosition;
			this.newPosition = newPosition;
			this.speed = speed;
			this.id = id;
			this.ids = ids;
		}
	}
}
