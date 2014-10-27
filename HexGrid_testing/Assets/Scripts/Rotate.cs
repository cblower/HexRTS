using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public Vector3 direction;
	void Update(){
		transform.Rotate(direction*Time.deltaTime);
	}
}
