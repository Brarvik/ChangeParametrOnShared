using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ChangeParametrOnShared.Model
{
	class FamilyParametrs
	{
		Document doc;
		public FamilyParametrs(UIApplication uiapp)
		{
			doc = uiapp.ActiveUIDocument.Document;
			GetFamilyParameters(doc);
		}

		public string ActiveFamilyDocName
		{ get { return doc.PathName; } }

		public ObservableCollection<FamilyParameter> FamilyParameters { get; set; } =
			new ObservableCollection<FamilyParameter>();
		private void GetFamilyParameters(Document doc)
		{
			FamilyParameters.Clear();
			foreach (FamilyParameter fp in doc.FamilyManager.Parameters)
			{
				if ((fp.Definition as InternalDefinition).BuiltInParameter
					== BuiltInParameter.INVALID)
				{
					FamilyParameters.Add(fp);
				}
			}
		}
		public void ReFresh()
		{
			GetFamilyParameters(doc);
		}
	}
}