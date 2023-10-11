using UnityEngine;
using System.Collections;

public partial class LevelConfig : GameConfigDataBase
{
	public string id;
	public string levelName;
	public string levelDescription;
	public int waveCount;
	public int time;
	public int times;
	public int zombiCount;
	public string zombiType;
	protected override string getFilePath ()
	{
		return "LevelConfig";
	}
}
