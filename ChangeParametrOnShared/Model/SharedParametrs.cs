using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ChangeParametrOnShared.Model
{
	class SharedParametrs
	{
		UIApplication uiapp;
		ParameterType pt;
		List<DefinitionGroup> openSharedParameterGroups = new List<DefinitionGroup>();

		public SharedParametrs(UIApplication uiapp, ParameterType pt)
		{
			this.uiapp = uiapp;
			this.pt = pt;
			foreach (DefinitionGroup df in uiapp.Application.OpenSharedParameterFile().Groups)
				openSharedParameterGroups.Add(df);
			openSharedParameterGroups.Sort(delegate (DefinitionGroup x, DefinitionGroup y)
			{
				if (x.Name == null && y.Name == null) return 0;
				else if (x.Name == null) return -1;
				else if (y.Name == null) return 1;
				else return x.Name.CompareTo(y.Name);
			});
		}

		public string OpenSharedParameterFileName
		{ get { return uiapp.Application.OpenSharedParameterFile().Filename; } }

		public List<DefinitionGroup> OpenSharedParameterFileGroups
		{
			get { return openSharedParameterGroups; }
		}

		public DefinitionGroup SelectedGroup { set { GetDefinitionSelectedGroup(value); } }

		public ObservableCollection<Definition> DefinitionSelectedGroup
		{ get; set; } = new ObservableCollection<Definition>();

		private void GetDefinitionSelectedGroup(DefinitionGroup dg)
		{
			DefinitionSelectedGroup.Clear();
			foreach (Definition d in dg.Definitions)
			{
				if (d.ParameterType == pt)
					DefinitionSelectedGroup.Add(d);
			}
		}
	}
}