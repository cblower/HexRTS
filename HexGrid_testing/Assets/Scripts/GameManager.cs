using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public CameraInfo camInfo;
	public GameInfo gameInfo;
	public GamePrefabs gamePrefabs;

	public List<GameObject> sceneObjects;
	private HexagonGrid grid;
	private float startTime, journeyLength;
	private Vector3 start, end;

	void Awake(){
		startTime = Time.time;
		grid = GetComponent<HexagonGrid>();
		grid.MakeGrid();
		PlaceStart(RandInt(0, grid.size), RandInt(0, grid.size));
	}

	void Update(){
		PlaceCam();
		CamInput();
		RightClick();
		float distCovered = (Time.time - startTime) * camInfo.speed;
		float fracJourney =  distCovered / journeyLength;
		Camera.main.transform.parent.parent.position = Vector3.Lerp(start, end, fracJourney);
	}
	public void RightClick(){
		if(Input.GetKeyDown(KeyCode.Mouse1) && gameInfo.selected == gameInfo.magiRing && !gameInfo.selected.isUsed){
			GameObject temp = Instantiate(gamePrefabs.entity, gameInfo.selected.objectPos, Quaternion.identity) as GameObject;
			sceneObjects.Add(temp);
			gameInfo.selected.isUsed = true;
		}
	}
	public void MovePosition(Vector2 pos){
		startTime = Time.time;
		start = Camera.main.transform.parent.parent.position;
		end = grid.grid[Mathf.RoundToInt(pos.x),Mathf.RoundToInt(pos.y)].objectPos;
		journeyLength = Vector3.Distance(start, end);
	}
	private void PlaceStart(int x, int z){
		gameInfo.magiRing = grid.grid[x, z];
		Vector3 pos = grid.grid[x,z].objectPos;
		pos.y += 0.05f;
		GameObject temp = Instantiate(gamePrefabs.magiRing, pos, Quaternion.identity) as GameObject;
		MovePosition(grid.grid[x,z].tilePos);
		gameInfo.selected = grid.grid[x,z];
		sceneObjects.Add(temp);
	}
	private void CamInput(){
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
			Camera.main.transform.parent.transform.Rotate(0,camInfo.rotateSpeed*Time.deltaTime,0);
		}
		else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
			Camera.main.transform.parent.transform.Rotate(0,-camInfo.rotateSpeed*Time.deltaTime,0);
		}
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && camInfo.offset.y > camInfo.zoomMin){
			camInfo.offset.y -= camInfo.zoomSpeed*Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && camInfo.offset.y < camInfo.zoomMax){
			camInfo.offset.y += camInfo.zoomSpeed*Time.deltaTime;
		}
	}
	private void PlaceCam(){
		Camera.main.transform.localPosition = camInfo.offset;
		Camera.main.transform.LookAt(Camera.main.transform.parent.position);
	}
	private int RandInt(int min, int max){
		return Mathf.RoundToInt(Random.Range(min, max));
	}
}
[System.Serializable]
public struct CameraInfo{
	public Vector3 offset;
	public float speed, zoomSpeed, rotateSpeed, zoomMin, zoomMax;
}
[System.Serializable]
public struct GameInfo{
	public Hex magiRing;
	public Hex selected;
}
[System.Serializable]
public struct GamePrefabs{
	public GameObject magiRing;
	public GameObject entity;
}