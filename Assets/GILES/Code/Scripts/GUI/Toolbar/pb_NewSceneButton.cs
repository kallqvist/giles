namespace GILES
{
#pragma warning disable IDE1006
    public class pb_NewSceneButton : pb_ToolbarButton
	{
#pragma warning restore IDE1006

        public override string Tooltip { get { return "Discard changes and open new scene"; } }

		public void OpenNewScene()
		{
			pb_Scene.Instance.Clear();
		}
	}
}