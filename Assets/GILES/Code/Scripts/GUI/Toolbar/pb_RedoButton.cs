namespace GILES
{
#pragma warning disable IDE1006
    public class pb_RedoButton : pb_ToolbarButton
	{
#pragma warning restore IDE1006

        public override string Tooltip
		{
			get
			{
				string cur = Undo.GetCurrentRedo();
				
				if(string.IsNullOrEmpty(cur))
					return "Nothing to Redo";
				else
					return "Redo: " + cur;
			}
		}

		protected override void Start()
		{
			base.Start();

			if(Undo.Instance.undoStackModified != null)	
				Undo.Instance.undoStackModified += RedoStackModified;
			else
				Undo.Instance.undoStackModified = RedoStackModified;

			if(Undo.Instance.redoStackModified != null)	
				Undo.Instance.redoStackModified += RedoStackModified;
			else
				Undo.Instance.redoStackModified = RedoStackModified;

			pb_Scene.AddOnLevelLoadedListener( () => { Interactable = false; } );
			pb_Scene.AddOnLevelClearedListener( () => { Interactable = false; } );

			RedoStackModified();
		}

		public void DoRedo()
		{
			Undo.PerformRedo();
		}

		private void RedoStackModified()
		{
			Interactable = !string.IsNullOrEmpty(Undo.GetCurrentRedo());
		}
	}
}