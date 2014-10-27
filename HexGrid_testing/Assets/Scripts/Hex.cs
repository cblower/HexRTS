using UnityEngine;
using System.Collections;

public class Hex : MonoBehaviour {
	public Vector2 tilePos;
	public Vector3 objectPos;
	public Material selected;
	public bool isUsed;

	private Material unselected;
	private GameManager manager;

	public void ConnectManager(){
		manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
	}
	public void SetPosition(){
		objectPos = this.transform.position;
	}
	void OnMouseEnter(){
		unselected = renderer.material;
		renderer.material = selected;
	}
	void OnMouseExit(){
		renderer.material = unselected;
	}
	void OnMouseDown(){
		Debug.Log(string.Format("Moving to Tile: {0} at 3D space: {1} inUse: {2}", tilePos, objectPos, isUsed));
		if(manager.gameInfo.selected != this){
			manager.MovePosition(tilePos);
			manager.gameInfo.selected = this;
		}
		else{
			Debug.Log(string.Format("Already at Tile: {0} at 3D space: {1} inUse: {2}", tilePos, objectPos, isUsed));
		}
	}
}
