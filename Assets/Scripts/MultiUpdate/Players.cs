using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
	public HT ht;
	public GameObject player;
	public void StartWarp() {
		ht = new HT();
		ht.construct(player);
	}
}

public class HT
{
	public LinkA[] data = new LinkA[31];
	public int Length = 31;
	public GameObject Player;

	public void construct(GameObject player){
		Player = player;
	}

	public int GetHash(string ip){
		string[] nums = ip.Split(".");
		string result = "";
		foreach(string num in nums){
			result += num;
		}
		Debug.Log(result);
		return int.Parse(result);
	}

	public int GetIndex(string ip){
		int hash = GetHash(ip);
		return hash % Length;
	}

	public void Insert(string ip){
		int i = GetIndex(ip);
		GameObject gm = GameObject.Instantiate(Player, new Vector3(0,0,0), Quaternion.identity);
		if(data[i] != null){
			if(data[i].key != ""){
				data[i].Insert(ip, gm);
			} else{
				data[i].key = ip;
				data[i].value = gm;
			}
		} else {
			data[i] = new LinkA();
			data[i].key = ip;
			data[i].value = gm;
		}
	}

	public GameObject Find(string ip){
		int i = GetIndex(ip);
		if(data[i] != null){
			if(data[i].key != ""){
				return data[i].Find(ip);
			} else {
				return null;
			}
		} else {
			return null;
		}
	}

	public GameObject Remove(string ip){
		int i = GetIndex(ip);
		if(data[i] != null){
			if(data[i].key != ""){
				return data[i].Remove(ip);
			} else {
				return null;
			}
		} else {
			return null;
		}
	}

	public void Symetric(){
		foreach(LinkA arr in data){
			if(arr != null){
				arr.ReadAll();
			}
		}
	}

	public bool IsExists(string key1){
		foreach(LinkA arr in data){
			if(arr != null){
				bool isEx = arr.IsExists(key1);
				if(isEx){
					return true;
				}
			}
		}
		return false;
	}
}

public class LinkA
{
	public LinkA next;
	public string key;
	public GameObject value;

	public void Insert(string key1, GameObject value1){
		if(next != null){
			next.Insert(key1, value1);
		} else {
			next = new LinkA();
			next.key = key1;
			next.value = value1;
			return;
		}
	}

	public GameObject Find(string key1){
		if(key1 == key){
			return value;
		} else {
			if(next != null){
				return next.Find(key1);
			} else {
				return null;
			}
		}
	}

	public GameObject Remove(string key1){
		if(key != key1){
			if(next != null){
				GameObject gm = next.Remove(key1);
				if(next != null){
					if(next.key == ""){
						next = null;
					}
				}
				return gm;
			} else {
				return null;
			}
		} else {
			if(next != null){
				LinkA temp = next;
				key = temp.key;
				GameObject gm = value;
				value = temp.value;
				next = temp.next;
				return gm;
			} else {
				key = "";
				GameObject gm = value;
				value = null;
				return gm;
			}
		}
	}

	public void ReadAll(){
		Debug.Log(key);
		if(next != null){
			next.ReadAll();
		}
	}

	public bool IsExists(string key1){
		Debug.Log(key);
		if(key != key1){
			if(next != null){
				return next.IsExists(key1);
			} else {
				return false;
			}
		} else {
			return true;
		}
		
	}


}