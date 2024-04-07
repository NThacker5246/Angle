using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwap : MonoBehaviour
{
	public GameObject load;
	public GameObject save;

	public void Swap(){
		load.SetActive(true);
	}
	public void UsSwap(){
		load.SetActive(false);
	}

	public void SwapL(){
		save.SetActive(true);
	}
	public void UsSwapL(){
		save.SetActive(false);
	}
}
