namespace GILES
{
#pragma warning disable IDE1006
    public class pb_ScaleToolButton : pb_ToolbarButton
	{
#pragma warning restore IDE1006

        public override string Tooltip { get { return "Scale Tool"; } }

		protected override void Start()
		{
			base.Start();

			if(pb_SelectionHandle.Instance.onHandleTypeChanged != null)
				pb_SelectionHandle.Instance.onHandleTypeChanged += OnHandleChange;
			else
				pb_SelectionHandle.Instance.onHandleTypeChanged = OnHandleChange;

			OnHandleChange();
		}

		public void DoSetHandle()
		{
			pb_SelectionHandle.Instance.SetTool(Tool.Scale);
		}

		private void OnHandleChange()
		{
			Interactable = !pb_SelectionHandle.Instance.GetIsHidden() && pb_SelectionHandle.Instance.GetTool() != Tool.Scale;
		}
	}
}