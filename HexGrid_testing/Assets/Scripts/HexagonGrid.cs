using UnityEngine;
using System.Collections;

public class HexagonGrid : MonoBehaviour {
	public int size = 5;
	[Range(0, 1f)]
	public float randomHeight = 0;
	public Hex prefab1;
	public Hex prefab2;
	public bool usePlane;
	public Hex[,] grid;

	public void MakeGrid(){
		grid = new Hex[size,size];

		for(int x=0; x<grid.GetUpperBound(0)+1; x++){
			for(int z=0; z<grid.GetUpperBound(1)+1; z++){
				if(usePlane){
					this.grid[x,z] = MakeTile(x, z, prefab1);
				}
				else{
					this.grid[x,z] = MakeTile(x, z, prefab2);
				}
			}
		}
	}

	private Hex MakeTile(int i, int j, Hex tile){
		Hex temp;
		float h = Random.Range(0, randomHeight);
		if(j%2==0){
			temp = Instantiate(tile, new Vector3(i, h, -j*0.75f), Quaternion.identity) as Hex;
			if(i%2==0){
				temp.gameObject.renderer.material.color = new Color(0, 0.4f, 0, 0);
			}
		}
		else{
			temp = Instantiate(tile, new Vector3(i+0.5f, h, -j*0.75f), Quaternion.identity) as Hex;
			if(i%2==1){
				temp.gameObject.renderer.material.color = new Color(0, 0.4f, 0, 0);
			}
		}
		temp.name = string.Format("Hex: {0},{1}", i, j);
		temp.transform.parent = transform;
		temp.tilePos = new Vector2(i, j);
		temp.SetPosition();
		temp.ConnectManager();
		return temp;
	}
}

[System.Serializable]
public class HexInfo{
	public Vector2 whole = new Vector2(0.732f,1);
	public float h, s, r, a, b;

	private Vector3[] setInfoFromMesh(Mesh mesh){
		return mesh.vertices;
	}
	private void SetInfo(){
		r = whole.x*0.5f;
		a = whole.x;
		h = whole.y-r*2f;
		s = whole.y-(h*2f);
		b = whole.y;
	}
}
[System.Serializable]
public class HexGrid{
	public bool used;
	public Vector3[] points;
	public GameObject HexPlane;
	
	public void SetPointsArray(int amt){
		points = new Vector3[amt];
	}
	
}
