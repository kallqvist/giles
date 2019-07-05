namespace GILES
{
#pragma warning disable IDE1006
    public class pb_UndoButton : pb_ToolbarButton
	{
#pragma warning restore IDE1006
        public override string Tooltip
		{
			get
			{
				string cur = Undo.GetCurrentUndo();
				
				if(string.IsNullOrEmpty(cur))
					return "Nothing to Undo";
				else
					return "Undo: " + cur;
			}
		}

		protected override void Start()
		{
			base.Start();

			if(Undo.Instance.undoStackModified != null)	
				Undo.Instance.undoStackModified += UndoStackModified;
			else
				Undo.Instance.undoStackModified = UndoStackModified;

			pb_Scene.AddOnLevelLoadedListener( () => { Interactable = false; } );
			pb_Scene.AddOnLevelClearedListener( () => { Interactable = false; } );

			UndoStackModified();
		}

		public void DoUndo()
		{
			Undo.PerformUndo();
		}

		private void UndoStackModified()
		{
			Interactable = !string.IsNullOrEmpty(Undo.GetCurrentUndo());
		}
	}
}