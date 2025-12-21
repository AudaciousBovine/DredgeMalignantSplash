using UnityEngine;
using Winch.Core;

namespace MalignantSplashes
{
	public class MalignantSplashes : MonoBehaviour
	{
		// Awake is called when the mod first loads
		public void Awake()
		{
			// Log to indicate that the mod has loaded
			WinchCore.Log.Debug($"{nameof(MalignantSplashes)} has loaded!");
		}
	}
}
