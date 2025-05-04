using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MrBeast : MonoBehaviour
{
	[HeaderAttribute ("Stats")]
	public Transform player;
	public float speed;
	public float stopDist;
	public float retreatDist;
	public float hp = 200;
	public GameObject TNT;
	public Vector3 sm;
	public float timeToDrop;

	[HeaderAttribute ("Behaviour")]
	public bool isAttack = false;
	public bool inColl;
	public bool isFirst;

	[HeaderAttribute ("Components")]
	private Animator anim;
	public PlayerHP HP;
	public Slider BossHP;
	public Fade TheEnd;
	public MusicMan sfx;

	private void Start(){
		anim = GetComponent<Animator>();
	}

	public void MoveToPlayer(){
		float dist = Vector3.Distance(transform.position, player.position);
		transform.eulerAngles = new Vector3(0f, player.eulerAngles.y-180f, 0f);
		//Debug.Log(dist);
		if(dist > stopDist){
			transform.position = Vector3.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
		} else if(dist < stopDist && dist > retreatDist){
			return;
		} else if(dist < stopDist && dist < retreatDist){
			transform.position = Vector3.MoveTowards(transform.position, player.position, -speed*Time.deltaTime);
		}
	}

	private void Update(){
		if(isAttack){
			MoveToPlayer();
			anim.SetBool("IsRun", true);
			if(inColl){
				HP.Damage(0.01f);
			}
			if(!isFirst){
				sfx.StopAllMusic();
				sfx.Replace();
				StartCoroutine("TNTDrop");
				isFirst = true;
			}
		} else {
			anim.SetBool("IsRun", false);
		}

		if(hp <= 0){
			TheEnd.FadeToLevel();
			Destroy(gameObject);
		}

		if(BossHP.value != (int) hp){
			BossHP.value = hp;
		}
	}

	private void OnTriggerEnter(Collider other){
		inColl = true;
	}

	private void OnTriggerExit(Collider other){
		inColl = false;
	}

	IEnumerator TNTDrop(){
		while(true){
			Instantiate(TNT, transform.position + new Vector3(Random.Range(0, sm.x), Random.Range(0, sm.y), Random.Range(0, sm.z)), Quaternion.identity);
			yield return new WaitForSeconds(timeToDrop);
		}
	}
}
