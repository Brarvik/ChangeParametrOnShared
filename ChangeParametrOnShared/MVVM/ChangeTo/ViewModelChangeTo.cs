using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ChangeParametrOnShared.MVVM.ChangeTo
{
	class ViewModelChangeTo : ViewModelBase
	{
		Model.SharedParametrs sp;
		Model.Changer changer;
		public ViewModelChangeTo(UIApplication uiapp, Model.Changer changer)
		{
			sp = new Model.SharedParametrs(uiapp
				, changer.Parametr.Definition.ParameterType);
			this.changer = changer;
		}

		public string OpenSharedParameterFileName
		{ get { return sp.OpenSharedParameterFileName; } }

		public List<DefinitionGroup> OpenSharedParameterFileGroups
		{ get { return sp.OpenSharedParameterFileGroups; } }

		public ObservableCollection<Definition> DefinitionSelectedGroup
		{ get { return sp.DefinitionSelectedGroup; } }

		public DefinitionGroup SelectedGroup
		{
			set
			{
				sp.SelectedGroup = value;
				OnPropertyChanged("SelectedGroup");
			}
		}
		public ExternalDefinition SelectedSharedParametr
		{
			set
			{
				changer.SharedParametr = value;
				OnPropertyChanged("SelectedSharedParametr");
			}
		}
		private RelayCommand go;
		public RelayCommand Go
		{
			get
			{
				return go ??
					  (go = new RelayCommand(obj =>
					  {
						  try
						  {
							  changer.Change();
							  Window win = obj as Window;
							  win.Close();
						  }
						  catch (Exception ex)
						  {
							  MessageBox.Show(ex.Message, "Error");
						  }
					  },
					  obj =>
					  {
						  return true;
					  }));
			}
		}
	}
}