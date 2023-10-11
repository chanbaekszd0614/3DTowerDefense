using UnityEngine;
using System.Collections;

public partial class TurretConfig : GameConfigDataBase
{
	public string id;
	public string turretName;
	public string turretDescription;
	public int turretATK;
	public float turretSpeed;
	public int turretATKR;
	public int turretPrice;
	public string turretModel;
	public string turretTex;
	public string BulletPrefab;
	protected override string getFilePath ()
	{
		return "TurretConfig";
	}
}
