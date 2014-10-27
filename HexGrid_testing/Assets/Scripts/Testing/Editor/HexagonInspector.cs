using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Hexagon))]
public class HexagonInspector : Editor{
	Hexagon hex;

	private void OnSceneGUI(){
		hex = target as Hexagon;

		for(int i=0; i<hex.size; i++){
			for(int j=0; j<hex.size; j++){
				if(j%2==0){
					DrawGrid(i*hex.info.whole.x, Random.Range(0,0.5f), j*(hex.info.h+hex.info.s), Color.green);
				}
				else{
					DrawGrid(i*hex.info.whole.x+hex.info.r, Random.Range(0,0.5f), j*(hex.info.h+hex.info.s)-hex.info.h+hex.info.h, Color.yellow);
				}
			}
		}
	}
	private void DrawGrid(float x, float y, float z,Color color){
		Vector3[] points = new Vector3[6];

		points[0] = hex.GetPoint(0, new Vector3(x, y, z));
		points[1] = hex.GetPoint(0, new Vector3(x, y, z));
		points[2] = hex.GetPoint(0, new Vector3(x, y, z));
		points[3] = hex.GetPoint(0, new Vector3(x, y, z));
		points[4] = hex.GetPoint(0, new Vector3(x, y, z));
		points[5] = hex.GetPoint(0, new Vector3(x, y, z));
		points[0] = hex.GetPoint(0, new Vector3(x, y, z));
		
		Handles.color = color;
		Handles.DrawPolyLine(points);
	}
}
