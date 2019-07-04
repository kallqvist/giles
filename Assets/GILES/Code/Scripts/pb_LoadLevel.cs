using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GILES
{
	/**
	 *	A simple script to load Unity scenes.
	 */
	public class pb_LoadLevel : MonoBehaviour
	{
		// The name of the level to load.  Must be added to the build settings.
		public string levelName;

		public void Load()
		{
			SceneManager.LoadScene(levelName);
		}
	}
}
