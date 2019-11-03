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
				if (commandData.Application.ActiveUIDocument.Document.IsFamilyDocument)
				{
					MVVM.ChangeIn.ChangeInWindow window = new MVVM.ChangeIn.ChangeInWindow()
					{
						DataContext = new MVVM.ChangeIn.ViewModelChangeIn(commandData)
					};
					window.ShowDialog();
				}
				else
				{
					MessageBox.Show("Please, Open Family Document from Edit Family mode"
				 , "ChangeParametrOnShared");
				}
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