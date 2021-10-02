using UnityEngine;
using System.Collections;
using System;

public class GameEventManager
{
	public static Action<Region> RegionConquered;

	public static void TriggerRegionConquered(Region region)
	{
		if (RegionConquered != null)
		{
			Debug.Log("RegionConquered");
			RegionConquered(region);
		}
	}
	public static void OnDestroy()
	{
		RegionConquered = null;
	}

}
