using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedShow : MonoBehaviour
{
	public GameObject GUIWindow;
	public GameObject[] functionsComp;
	public BlockEditor be;
	public TextureChanger tx;

	public void SetWindow(int n){
		GUIWindow.SetActive(true);
		ResetAllWindows();
		functionsComp[n].SetActive(true);
	}
	public void ResetWindow(int n){
		functionsComp[n].SetActive(false);
	}

	public void ResetAllWindows(){
		foreach(GameObject win in functionsComp){
			win.SetActive(false);
		}
	}

	public void CloseWindow(){
		ResetAllWindows();
		GUIWindow.SetActive(false);
	}

	public void ShowAdvancedWindow(){
		if(be.sel != null){
			Ids ids = be.sel.GetComponent<Ids>();
			if(ids.mainId == 0 && ids.groupId == 0){
				SetWindow(0);
				tx.ReadTexture();
			} else if(ids.groupId == 3){
				SetWindow(1);
			}
		}
	}
}
