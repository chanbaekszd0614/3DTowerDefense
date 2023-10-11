using UnityEngine;
using System.Collections;

public partial class ZombiConfig : GameConfigDataBase
{
	public string id;
	public string zombiName;
	public string zombiDescription;
	public int zombiHp;
	public string zombiModel;
	public int zombiGold;
	protected override string getFilePath ()
	{
		return "ZombiConfig";
	}
}
