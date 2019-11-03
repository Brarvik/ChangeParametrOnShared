using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChangeParametrOnShared.MVVM
{
	class ViewModelBase : INotifyPropertyChanged
	{
		private RelayCommand close;
		public RelayCommand Close
		{
			get
			{
				return close ??
					  (close = new RelayCommand(obj =>
					  {
						  try
						  {
							  Window window = obj as Window;
							  window.Close();
						  }
						  catch (Exception ex)
						  {
							  MessageBox.Show(ex.Message);
						  }
					  },
					  obj =>
					  {
						  return true;
					  }));
			}
		}

		// INotifyPropertyChanged interface implementation
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}