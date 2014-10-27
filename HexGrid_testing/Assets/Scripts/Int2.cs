[System.Serializable]
public struct Int2 {
	public int x, z;

	public Int2(int x, int z){
		this.x = x;
		this.z = z;
	}
	public static Int2 operator + (Int2 a, Int2 b){
		a.x += b.x;
		a.z += b.z;
		return a;
	}
}
