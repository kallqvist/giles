using UnityEngine;
using UnityEngine.SceneManagement;

namespace GILES
{
    /**
	 *	A simple script to load Unity scenes.
	 */
#pragma warning disable IDE1006
    public class pb_LoadLevel : MonoBehaviour
	{
#pragma warning restore IDE1006
        // The name of the level to load.  Must be added to the build settings.
        public string levelName;

		public void Load()
		{
			SceneManager.LoadScene(levelName);
		}
	}
}
