using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

using System.Windows;

namespace ChangeParametrOnShared
{
	[TransactionAttribute(TransactionMode.Manual)]
	[RegenerationAttribute(RegenerationOption.Manual)]
	public class Command : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData,
			ref string messege, ElementSet elements)
		{
			try
			{
				MVVM.ChangeIn.ChangeInWindow window = new MVVM.ChangeIn.ChangeInWindow()
				{
					DataContext = new MVVM.ChangeIn.ViewModelChangeIn(commandData)
				};
				window.ShowDialog();

				return Result.Succeeded;
			}
			catch (Autodesk.Revit.Exceptions.OperationCanceledException)
			{
				return Result.Cancelled;
			}
			catch (Exception ex)
			{
				messege = ex.Message;
				return Result.Failed;
			}
		}
	}
}