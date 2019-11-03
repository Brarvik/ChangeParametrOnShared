using System;
using System.Collections.ObjectModel;
using System.Windows;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ChangeParametrOnShared.MVVM.ChangeIn
{
	class ViewModelChangeIn : ViewModelBase
	{
		UIApplication uiapp;
		Model.FamilyParametrs fp;
		Model.Changer changer;
		public ViewModelChangeIn(ExternalCommandData commandData)
		{
			uiapp = commandData.Application;
			fp = new Model.FamilyParametrs(uiapp);
			changer = new Model.Changer();
		}

		public string ActiveFamilyDocName
		{ get { return fp.ActiveFamilyDocName; } }

		public ObservableCollection<FamilyParameter> FamilyParameters
		{
			get { return fp.FamilyParameters; }
			set
			{
				fp.FamilyParameters = value;
				OnPropertyChanged("FamilyParameters");
			}
		}

		private FamilyParameter selectedFamilyParameter;
		public FamilyParameter SelectedFamilyParameter
		{
			get { return selectedFamilyParameter; }
			set
			{
				selectedFamilyParameter = value;
				OnPropertyChanged("SelectedFamilyParameter");
			}
		}

		private RelayCommand change;
		public RelayCommand Change
		{
			get
			{
				return change ??
					  (change = new RelayCommand(obj =>
					  {
						  try
						  {
							  changer.Doc = uiapp.ActiveUIDocument.Document;
							  changer.Parametr = SelectedFamilyParameter;
							  ChangeTo.ChangeToWindow win =
							  new ChangeTo.ChangeToWindow()
							  {
								  DataContext =
								  new ChangeTo.ViewModelChangeTo(uiapp, changer)
							  };
							  win.Closed += WinClosed;
							  win.ShowDialog();
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

		private void WinClosed(object sender, EventArgs e)
		{
			fp.ReFresh();
		}
	}
}