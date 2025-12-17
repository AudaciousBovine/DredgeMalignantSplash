using UnityEngine;
using Winch.Core;

namespace MalignantSplashes
{
	public class MalignantSplashes : MonoBehaviour
	{
		public void Awake()
		{
			WinchCore.Log.Debug($"{nameof(MalignantSplashes)} has loaded!");
		}
	}
}
