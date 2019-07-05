using UnityEngine;
using System.Collections.Generic;

namespace GILES
{
#pragma warning disable IDE1006
    public class pb_DeleteButton : pb_ToolbarButton
	{
#pragma warning restore IDE1006

        protected override void Start()
		{
			base.Start();

			pb_Selection.AddOnSelectionChangeListener( OnSelectionChanged );
			OnSelectionChanged(null);
		}

		public override string Tooltip { get { return "Delete Selection"; } }

		public void DoDelete()
		{
			Undo.RegisterStates( new IUndo[] { new UndoDelete(pb_Selection.GameObjects), new UndoSelection() }, "Delete Selection" );
			pb_Selection.Clear();
		}

		private void OnSelectionChanged(IEnumerable<GameObject> go)
		{
			Interactable = pb_Selection.ActiveGameObject != null;
		}
	}
}