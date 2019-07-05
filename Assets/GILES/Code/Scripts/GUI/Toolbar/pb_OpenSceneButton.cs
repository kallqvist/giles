using UnityEngine.SceneManagement;

namespace GILES
{
    /**
	 * Open a Unity scene.
	 */
#pragma warning disable IDE1006
    public class pb_OpenSceneButton : pb_ToolbarButton
	{
#pragma warning restore IDE1006

        public string scene;

		public override string Tooltip { get { return "Open " + scene; } }

		public void OpenScene()
		{
			SceneManager.LoadScene(scene);
		}
	}
}
