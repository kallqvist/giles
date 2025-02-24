using UnityEngine;

namespace GILES.Interface
{
#pragma warning disable IDE1006
    public interface pb_ICustomEditor
	{
#pragma warning restore IDE1006

        /**
		 * Instantiate a UI panel or layout group that will be added to the inspector window.
		 * \sa pb_TypeInspector, pb_Inspector, pb_ComponentEditor
		 */
        pb_ComponentEditor InstantiateInspector(Component component);
	}
}