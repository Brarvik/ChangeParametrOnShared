using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ChangeParametrOnShared
{
	public class CommandAvailability : IExternalCommandAvailability
	{
		public bool IsCommandAvailable(UIApplication applicationData
			,CategorySet selectedCategories)
		{
			if (applicationData.ActiveUIDocument.Document.IsFamilyDocument)
				return true;
			return false;
		}
	}
}