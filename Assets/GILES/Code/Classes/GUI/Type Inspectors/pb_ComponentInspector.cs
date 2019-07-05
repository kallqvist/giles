using UnityEngine;

namespace GILES.Interface
{
	/**
	 * Field editor for Component types.
	 */
	[pb_TypeInspector(typeof(Component))]
#pragma warning disable IDE1006

    public class pb_ComponentInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        public override void InitializeGUI()
		{}
			
		protected override void OnUpdateGUI()
		{}
	}
}