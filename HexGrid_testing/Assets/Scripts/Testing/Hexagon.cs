using UnityEngine;

public class Hexagon : MonoBehaviour {
	public int size = 5;
	public HexInfo info;
	public HexGrid[,] grid;

	public Vector3 GetPoint(int point, Vector3 offset){
		SetWhole();
		if(point == 0){
			return (new Vector3(offset.z, offset.y, offset.x+info.r));
		}
		else if(point == 1){
			return (new Vector3(offset.z+info.h, offset.y, offset.x+info.a));
		}
		else if(point == 2){
			return (new Vector3(offset.z+info.s+info.h, offset.y, offset.x+info.a));
		}
		else if(point == 3){
			return (new Vector3(offset.z+info.b, offset.y, offset.x+info.r));
		}
		else if(point == 4){
			return (new Vector3(offset.z+info.h+info.s, offset.y, offset.x));
		}
		else if(point == 5){
			return (new Vector3(offset.z+info.h, offset.y, offset.x));
		}
		else{
			return Vector3.zero;
		}
	}
	private void SetWhole(){
		info.r = info.whole.x*0.5f;
		info.a = info.whole.x;
		info.h = info.whole.y-info.r*2f;
		info.s = info.whole.y-(info.h*2f);
		info.b = info.whole.y;
	}
}