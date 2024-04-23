using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockEditor : MonoBehaviour
{
    public GroupsOfBlocks[] blocks;
    public Transform player;
    public int HOT;
    public int HOTGroup;
    public Camera dot;
    public float l;
    public GameObject sel;
    public PosController Inspector;
    public GameObject[] allBlocks;
    public int iOfb;
    public int DeletingBlocks;

    public Image th;

    void Start(){
        dot = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 ro = dot.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 rd = transform.forward*l;
        Ray r = new Ray(ro, rd);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(r, out hit, l) && Input.GetMouseButtonDown(0)){
            sel = hit.collider.gameObject;
            Inspector.Read();
        }
        //Debug.DrawLine(ro, rd, Color.red);
        if(Input.GetMouseButtonDown(1)){
            if(DeletingBlocks == 0) {
                GameObject block = Instantiate(blocks[HOTGroup].blocks[HOT], transform.position, Quaternion.identity);
                allBlocks[iOfb] = block;
                iOfb += 1;
                block.transform.name = "" + (iOfb-1);
            } else {
                int j = 0;
                foreach(GameObject gm in allBlocks){
                    if(gm == null){
                        GameObject block = Instantiate(blocks[HOTGroup].blocks[HOT], transform.position, Quaternion.identity);
                        allBlocks[j] = block;
                        block.transform.name = "" + j;
                        DeletingBlocks -= 1;
                        break;
                    }
                    j += 1;
                }
            }
        }
        CheckHotkeyInput();
    }

    private void CheckHotkeyInput()
    {
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

}
