[System.Serializable]
public struct Int3 {
	public int x, y, z;

	public Int3(int x, int y, int z){
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public static Int3 operator + (Int3 a, Int3 b){
		a.x += b.x;
		a.y += b.y;
		a.z += b.z;
		return a;
	}
}
