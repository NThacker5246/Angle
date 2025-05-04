using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class BlockEditor : MonoBehaviour
{
	[SerializeField] private GroupsOfBlocks[] blocks;
	[SerializeField] private Transform player;
	[SerializeField] private int HOT;
	[SerializeField] private int HOTGroup;
	[SerializeField] private float l;
	[SerializeField] private GameObject sel;
	[SerializeField] private PosController Inspector;

	[SerializeField] private int obj;

	[SerializeField] private Room[] rm = {new Room("1")};

	[SerializeField] private Image th;

	[SerializeField] private GameObject text;
	[SerializeField] private Transform tp;
	[SerializeField] private Transform hie;
	[SerializeField] private int currentRoom;
	[SerializeField] private GameObject roomBtn;
	[SerializeField] private byte mode;
	[SerializeField] private int idOfSetTrigger;

	[SerializeField] private int count;

	void Start(){
		initRoom();
	}

	public void AddRoom(){
		Room[] newRm = new Room[rm.Length+1];
		for(int i = 0; i < rm.Length; i++){
			newRm[i] = rm[i];
		}
		newRm[rm.Length] = new Room($"{rm.Length}");
		rm = newRm;

		GameObject txt = Instantiate(roomBtn, hie);
		txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y - 50, 0);
		txt.name = $"{rm.Length}";
		txt.transform.GetChild(0).GetComponent<Text>().text = $"{rm.Length}";
		roomBtn = txt;
		initRoom(rm.Length - 1);			
	}

	public Room AddRoom(string name){
		Room[] newRm = new Room[rm.Length+1];
		for(int i = 0; i < rm.Length; i++){
			newRm[i] = rm[i];
		}
		newRm[rm.Length] = new Room(name);
		rm = newRm;

		GameObject txt = Instantiate(roomBtn, hie);
		txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y - 50, 0);
		txt.name = name;
		if(txt.transform.GetComponent<Text>() != null){
			txt.transform.GetChild(0).GetComponent<Text>().text = name;
		}
		roomBtn = txt;
		initRoom(rm.Length - 1);	
		return newRm[rm.Length - 1];		
	}

	public void SelectRoom(GameObject i){
		currentRoom = int.Parse(i.name);
		Simetrical(currentRoom);

	}

	public void Set(InputField clicked){
		if(mode == 0){
			sel = getBlock(clicked.text, rm[currentRoom]).block;
			Inspector.Read();
		} else if(mode == 1){
			Inspector.WriteAct(getBlock(clicked.text, rm[currentRoom]).block);
		} else if(mode == 2){
			Inspector.ts.Select(getBlock(clicked.text, rm[currentRoom]).block, -1);
		} else if(mode == 3){
			Inspector.ts.Select(getBlock(clicked.text, rm[currentRoom]).block, idOfSetTrigger);
		}
	}

	public void Scroll(Scrollbar scroll){
		tp.position = new Vector3(tp.position.x, (scroll.value * obj * 70) + 540, 0);
		print(tp.position.y);
	}

	public void ReMaxName(InputField th){
		string nt = th.text;
		string ot = th.transform.name;
		renBlock(nt, ot, rm[currentRoom]);
		text = tp.GetChild(tp.childCount - 1).gameObject;
		Simetrical(currentRoom);
	}

	public void UIDel(InputField th){
		delBlock(th.text, rm[currentRoom]);
		text = tp.GetChild(tp.childCount - 1).gameObject;
		
		Simetrical(currentRoom);
	}
	
	void Update(){
		Vector3 ro = transform.position;
		Vector3 rd = transform.forward*l;
		Ray r = new Ray(ro, rd);
		RaycastHit hit = new RaycastHit();
		if(Physics.Raycast(r, out hit, l) && Input.GetMouseButtonDown(0) && !Cursor.visible){
			sel = hit.collider.gameObject;
			Inspector.Read();
			print("RKs");
		}
		if(Input.GetMouseButtonDown(1)){
			GameObject block = Instantiate(blocks[HOTGroup].blocks[HOT], transform.position, Quaternion.identity);
			
			string name = $"New Object {obj}";
			block.transform.name = name;
			obj += 1;
			addBlock(name, block, rm[currentRoom]);
			Simetrical(currentRoom);

		}
		CheckHotkeyInput();
	}

	private void CheckHotkeyInput(){
		for (int i = 1; i <= blocks[HOTGroup].blocks.Length; i++)
		{
			if (Input.GetKeyDown(KeyCode.Alpha0 + i))
			{
				HOT = i - 1;
			}
		}
		if(Input.GetKeyDown(KeyCode.Q)){
			HOTGroup -= 1;
			HOTGroup = Mathf.Clamp(HOTGroup, 0, blocks.Length-1);
		} else if(Input.GetKeyDown(KeyCode.E)){
			HOTGroup += 1;
			HOTGroup = Mathf.Clamp(HOTGroup, 0, blocks.Length-1);
		}

		HOT = Mathf.Clamp(HOT, 0, blocks[HOTGroup].blocks.Length-1);
		if(th.sprite != blocks[HOTGroup].sp[HOT]){
			th.sprite = blocks[HOTGroup].sp[HOT];
		}
	}

	public int HackerHash(string name){
		int result = 0;
		byte[] asciiBytes = Encoding.ASCII.GetBytes(name);
		foreach(byte btp in asciiBytes){
			result = result << 5 - result;
			result += btp;
		}

		return result;
	}

	public void initRoom(){
		rm[currentRoom].blk = new BlockE("Test", gameObject, HackerHash("Test"));
	}
	public void initRoom(int room){
		rm[room].blk = new BlockE("Test", gameObject, HackerHash("Test"));
	}

	public void addBlock(string name, GameObject block, Room cur){
	 	int hash = HackerHash(name); 
		BlockE stack = cur.blk;
		cur.count += 1;

		while(true){
			if(stack.hash < hash){
				if(stack.left == null){
					stack.left = new BlockE(name, block, hash);
					GameObject txt = Instantiate(text, tp);
					txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y - 70, 0);
					txt.name = name;
					if(txt.GetComponent<InputField>() != null){
						txt.GetComponent<InputField>().text = name;
					}
					text = txt;
					return;
				} else {
					stack = stack.left;
					continue;
				}
			} else if(stack.hash > hash){
				if(stack.right == null){
					stack.right = new BlockE(name, block, hash);
					GameObject txt = Instantiate(text, tp);
					txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y - 70, 0);
					txt.name = name;					
					if(txt.GetComponent<InputField>() != null){
						txt.GetComponent<InputField>().text = name;
					}
					text = txt;
					return;
				} else {
					stack = stack.right;
					continue;
				}
			} else {
				return;
			}
		}
	}

	public BlockE getBlock(string name, Room cur){
	 	int hash = HackerHash(name); 
		BlockE stack = cur.blk;

		while(true){
			if(stack.hash < hash){
				if(stack.left == null){
					return null;
				} else {
					stack = stack.left;
					continue;
				}
			} else if(stack.hash > hash){
				if(stack.right == null){
					return null;
				} else {
					stack = stack.right;
					continue;
				}
			} else {
				return stack;
			}
		}
	}

	public BlockE getMax(BlockE st){ 
		BlockE last = st; 
		while(last.right != null){
			last = last.right;
		} 
		return last; 
	}

	public GameObject delBlockWarp(string name, BlockE blk){
	 	int hash = HackerHash(name); 
		BlockE stack = blk;
		BlockE prev = stack;
		byte l = 0;
		GameObject temp = null;
		while(true){
			if(stack.hash < hash){
				if(stack.left == null){
					return null;
				} else {
					l = 1;
					prev = stack;
					stack = stack.left;
					continue;
				}
			} else if(stack.hash > hash){
				if(stack.right == null){
					return null;
				} else {
					l = 2;
					prev = stack;
					stack = stack.right;
					continue;
				}
			} else {
				if(l == 1){
					if(prev.left.left == null && prev.left.right == null){
						temp = stack.block;
						prev.left = null;
						stack = null;
						return temp;
					} else if(prev.left.left != null && prev.left.right != null){
						temp = stack.block;
						BlockE maxInLeft = getMax(prev.left.left);
						prev.left.hash = maxInLeft.hash;
						prev.left.name = maxInLeft.name;
						prev.left.block = maxInLeft.block;
						delBlockWarp(prev.left.name, prev.left.left);
					} else {
						if(prev.left.left != null){
							temp = stack.block;
							prev.left = prev.left.left;
							return temp;
						}
						if(prev.left.right != null){
							temp = stack.block;
							prev.right = prev.right.right;
							return temp;
						}
					}
					
				} else if(l == 2){
					if(prev.right.left == null && prev.right.right == null){
						temp = stack.block;
						prev.right = null;
						stack = null;
						return temp;
					} else if(prev.right.left != null && prev.right.right != null){
						temp = stack.block;
						BlockE maxInLeft = getMax(prev.right.left);
						prev.right.hash = maxInLeft.hash;
						prev.right.name = maxInLeft.name;
						prev.right.block = maxInLeft.block;
						delBlockWarp(prev.right.name, prev.right.left);
						return temp;
					} else {
						if(prev.right.left != null){
							temp = stack.block;
							prev.right = prev.right.left;
							return temp;
						}
						if(prev.right.right != null){
							temp = stack.block;
							prev.right = prev.right.right;
							return temp;
						}
					}
				} else {
					temp = stack.block;
					prev = null;
					stack = null;
					blk = null;
					return temp;
				}
			}
		}
	}

	public void delBlock(string name, Room cur){
		GameObject gm = delBlockWarp(name, cur.blk);
		Destroy(gm);
		Simetrical(currentRoom);
		cur.count -= 1;
	}

	public void renBlock(string Nname, string Oname, Room cur){
		GameObject gm = getBlock(Oname, cur).block;
		GameObject gm2 = Instantiate(gm, gm.transform.position, Quaternion.Euler(gm.transform.eulerAngles));
		delBlock(Oname, cur);
		addBlock(Nname, gm2, cur);
		Simetrical(currentRoom);
	}

	public void Simetrical(int room){
		for(int i = 1; i < tp.childCount; i++){
			Destroy(tp.GetChild(i).gameObject);
		}
		text = tp.GetChild(0).gameObject;
		SimUn(rm[room].blk);
	}

	public void SimUn(BlockE blk){
		if(blk.left != null){
			SimUn(blk.left);
		}
		GameObject txt = Instantiate(text, tp);
		txt.transform.position = new Vector3(txt.transform.position.x, txt.transform.position.y - 70, 0);
		txt.GetComponent<InputField>().text = blk.name;
		txt.name = blk.block.transform.name;
		text = txt;
		txt.SetActive(true);
		print(blk.block.transform.name);
		if(blk.right != null){
			SimUn(blk.right);
		}
	}

	//Функции доступа к приватным переменным
	public Room getCurrentRoom(){
		return rm[currentRoom];
	}

	public byte getMode(){
		return mode;
	}

	public void setMode(int tmode){
		mode = (byte) tmode;
	}

	public GameObject getSelectedBlock(){
		return sel;
	}

	public void setIdOfSet(int id_){
		idOfSetTrigger = id_;
	}

	//Функции подготовки данных к сохранению
	public string ToJSON(){
		count = 0;
		string[] rooms = new string[rm.Length];
		int k = 0;
		int linkerCount = 0;
		int roomCout = 0;
		foreach(Room theRoom in rm){
			string[] names = new string[theRoom.count];
			GameObject[] gameObjects = new GameObject[theRoom.count];
			GetData(theRoom.blk, names, gameObjects);
			string[] dtJ = new string[theRoom.count];
			for(int i = 0; i < theRoom.count; i++){
				InfoBlocks dt = new InfoBlocks();
				if(gameObjects[i] == null) continue;
				dt.name = names[i];

				print(i);
				print(gameObjects[i]);
				
				dt.position = gameObjects[i].transform.position;
				dt.rotation = gameObjects[i].transform.eulerAngles;
				dt.scale = gameObjects[i].transform.localScale;

				dt.group = gameObjects[i].GetComponent<Ids>().groupId;
				dt.block = gameObjects[i].GetComponent<Ids>().mainId;

				if(gameObjects[i].GetComponent<Button>() != null){
					dt.linkedPath = $"{roomCout}," + gameObjects[i].GetComponent<Button>().ActionObject.name;
					linkerCount += 1;
				} else if(gameObjects[i].GetComponent<ClicableButton>() != null){
					dt.linkedPath = $"{roomCout}," + gameObjects[i].GetComponent<ClicableButton>().ActionObject.name;
					linkerCount += 1;
				} else if(gameObjects[i].GetComponent<PlayerButton>() != null){
					dt.linkedPath = $"{roomCout}," + gameObjects[i].GetComponent<PlayerButton>().ActionObject.name;
					linkerCount += 1;
				}

				if(dt.group == 4){
					dt.triggerType = gameObjects[i].GetComponent<Trigger>().Type;
					dt.posInnactive = gameObjects[i].GetComponent<Trigger>().thisPosition;
					dt.posActive = gameObjects[i].GetComponent<Trigger>().newPosition;
					dt.speed = gameObjects[i].GetComponent<Trigger>().speed;
					if(dt.triggerType != "spawn"){
						dt.linkedPath =  roomCout + "," + gameObjects[i].GetComponent<Trigger>().gm.name;
					} else {
						int j = 0;
						foreach(GameObject trigger in gameObjects[i].GetComponent<Trigger>().triggers){
							dt.subTriggers[j] = roomCout + "," + trigger.name;
						}
					}
					linkerCount += 1;
				}

				if(dt.group == 0 && dt.block == 0){
					dt.color = gameObjects[i].GetComponent<Renderer>().material.color;
				}
				
				if(dt.group == 3){
					dt.intensity = (int) gameObjects[i].GetComponent<Light>().intensity;
					dt.spotAngle = gameObjects[i].GetComponent<Light>().spotAngle;
					dt.range = (int) gameObjects[i].GetComponent<Light>().range;
				}

				dtJ[i] = JsonUtility.ToJson(dt);
			}
			roomCout += 1;
			RMdata data = new RMdata();
			data.blocks = dtJ;
			data.name = theRoom.name;
			data.objs = theRoom.count;
			rooms[k] = JsonUtility.ToJson(data);
			k += 1;
		}

		FD fd = new FD();
		fd.rooms = rooms;
		fd.linkerCount = linkerCount;
		return JsonUtility.ToJson(fd);
	}

	public void GetData(BlockE current, string[] names, GameObject[] gameObjects){
		if(current.left != null) GetData(current.left, names, gameObjects);
		if(current.block.GetComponent<Camera>() == null){
			names[count] = current.name;
			gameObjects[count] = current.block;
			count += 1;
			print(current.block.name);
			print(current.name);
		}
		if(current.right != null) GetData(current.right, names, gameObjects);
	}

	public void WriteDataFromFile(string json, int modeF){
		rm = new Room[0];
		for(int v = 0; v < hie.childCount; v++){
			if(hie.GetChild(v).name != "Scroll" && hie.GetChild(v).name != "Scrollbar"){
				Destroy(hie.GetChild(v).gameObject);
			}
		}
		FD fd = JsonUtility.FromJson<FD>(json);
		InfoBlocks[] linked = new InfoBlocks[fd.linkerCount];
		GameObject[] gms = new GameObject[fd.linkerCount];
		int i = 0;
		foreach(string room in fd.rooms){
			RMdata dataRoom = JsonUtility.FromJson<RMdata>(room);
			Room current = AddRoom(dataRoom.name);
			current.count = 0;

			foreach(string block in dataRoom.blocks){
				if(block == "\\") continue;
				InfoBlocks blk = JsonUtility.FromJson<InfoBlocks>(block);

				GameObject gm = Instantiate(blocks[blk.group].blocks[blk.block], blk.position, Quaternion.Euler(blk.rotation));
				gm.transform.localScale = blk.scale;
				gm.transform.name = blk.name;

				if(blk.group == 0 && blk.block == 0){
					gm.GetComponent<Renderer>().material.color = blk.color;
				}

				if(blk.group == 3){
					gm.GetComponent<Light>().intensity = blk.intensity;
					gm.GetComponent<Light>().range = blk.range;
					gm.GetComponent<Light>().spotAngle = blk.spotAngle;
					if(modeF == 1){
						Destroy(gm.GetComponent<Renderer>());
					}
				}

				addBlock(blk.name, gm, current);
				if(blk.group == 1 || blk.group == 4){
					linked[i] = blk;
					gms[i] = gm;
					i += 1;
				}
			}

			/*
				if(gm.GetComponent<Button>() != null){
					
				} else if(gm.GetComponent<ClicableButton>() != null){
					
				} else if(gm.GetComponent<PlayerButton>() != null){
					
				}
			*/
		}

		i = 0;

		foreach(InfoBlocks bl in linked){
			string[] path = bl.linkedPath.Split(',');
			print(path[0]);
			print(path[1]);
			int rom = int.Parse(path[0]);
			GameObject acted = getBlock(path[1], rm[rom]).block;

			if(bl.group == 1){
				if(gms[i].GetComponent<Button>() != null){
					gms[i].GetComponent<Button>().ActionObject = acted.GetComponent<ActedObject>();
				} else if(gms[i].GetComponent<ClicableButton>() != null){
					gms[i].GetComponent<ClicableButton>().ActionObject = acted.GetComponent<ActedObject>();
				} else if(gms[i].GetComponent<PlayerButton>() != null){
					gms[i].GetComponent<PlayerButton>().ActionObject = acted.GetComponent<ActedObject>();
				}
			}

			if(bl.group == 4){
				Trigger tg = gms[i].GetComponent<Trigger>();
				tg.Type = bl.triggerType;
				switch(tg.Type){
					case "move":
						tg.thisPosition = bl.posInnactive;
						tg.newPosition = bl.posActive;
						tg.speed = bl.speed;
						tg.gm = acted;
						break;

					case "toggle":
						tg.gm = acted;
						break;

					case "spawn":
						int n = 0;
						tg.triggers = new GameObject[bl.subTriggers.Length];
						foreach(string pathes in bl.subTriggers){
							string[] path1 = bl.linkedPath.Split(',');
							tg.triggers[n] = getBlock(path1[1], rm[int.Parse(path1[0])]).block;
							n += 1;
						}
						break;

				}
			}
			i += 1;
		}

	}

}

[Serializable]

public class BlockE {
	public int hash;
	public string name;
	public GameObject block;

	public BlockE left;
	public BlockE right;

	public BlockE(string namep, GameObject blockp, int hashp){
		name = namep;
		block = blockp;
		hash = hashp;
	}
}

[Serializable]
public class Room {
	public string name;
	public BlockE blk;
	public int count;

	public Room(string name){
		this.name = name;
		this.blk = null;
		this.count = 0;
	}
}

public class StackNode {
	public StackNode prevNode;
	public BlockE data;
	public byte i;

	public StackNode(BlockE data, byte i){
		this.data = data;
		this.i = i;
	}
}

public class Stack {
	public StackNode last;

	public void push(BlockE data, byte i){
		StackNode newLast = new StackNode(data, i);
		newLast.prevNode = this.last;
		this.last = newLast;
	}

	public Stack(BlockE data, byte i){
		StackNode newLast = new StackNode(data, i);
		this.last = newLast;
	}

	public void pop(){
		if(last.prevNode != null){
			this.last = last.prevNode;
		} else {
			this.last = null;
		}
	}

	public StackNode get(){
		return this.last;
	}

	public bool NotEmpty(){
		if(last != null){
			return true;
		}

		return false;
	}
}

[Serializable]
public class InfoBlocks
{
	public string name;
	public Vector3 position;
	public Vector3 rotation;
	public Vector3 scale;
	public int group;
	public int block;

	public string linkedPath;
	
	public string triggerType;
	public Vector3 posInnactive;
	public Vector3 posActive;
	public int speed;
	public string[] subTriggers; //for spawn trigger

	public Color32 color;
	
	public int intensity;
	public int range;
	public float spotAngle;

}

[Serializable]
public class RMdata
{
	public string[] blocks;
	public string name;
	public int objs;
}

public class FD
{
	public string[] rooms;
	public int linkerCount; //Для O(N)
}