using System;
using System.Windows;

using Autodesk.Revit.DB;

namespace ChangeParametrOnShared.Model
{
	class Changer
	{
		public Document Doc { get; set; }
		public FamilyParameter Parametr { get; set; }
		public ExternalDefinition SharedParametr { get; set; }

		public void Change()
		{
			using (Transaction t = new Transaction(Doc, "Changer"))
			{
				try
				{
					t.Start();
					FamilyParameter fp =
						Doc.FamilyManager.ReplaceParameter
						(Parametr
						, SharedParametr
						, Parametr.Definition.ParameterGroup
						, Parametr.IsInstance);
					t.Commit();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error");

				}
			}
		}
	}
}