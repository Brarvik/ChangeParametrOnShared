using System;
using Autodesk.Revit.UI;

namespace ChangeParametrOnShared
{
	class App : IExternalApplication
	{
		static readonly string ExecutingAssemblyPath = System.Reflection.Assembly
			.GetExecutingAssembly().Location;

		public Result OnStartup(UIControlledApplication app)
		{
			RibbonPanel rvtRibbonPanel = app.CreateRibbonPanel("ChangeParameter");
			PushButton button = rvtRibbonPanel.AddItem
				(new PushButtonData("Button", "ChangeParameter"
				, ExecutingAssemblyPath, "ChangeParametrOnShared.Command")) as PushButton;

			button.LargeImage = new System.Windows.Media.Imaging.BitmapImage
				(new Uri("pack://application:,,,/ChangeParametrOnShared;component/ChangeParametrOnShared/icon.ico"
				, UriKind.Absolute));

			button.ToolTip =
				"Replace a Family Parameter to a Shared Parameter";

			return Result.Succeeded;
		}

		public Result OnShutdown(UIControlledApplication app)
		{
			return Result.Succeeded;
		}
	}
}	