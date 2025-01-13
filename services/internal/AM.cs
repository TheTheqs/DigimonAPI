using System.Diagnostics;
using System.Xml.Linq;

namespace DigimonAPI.services;
//This class is abble to manage de application with console comands.
public static class AM //Stands for App Manager
{
	//This funtion will restart the server
	public static void RestartServerWithLog()
	{
		try
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] App Manager: Start process configuration.");
			//Command arguments
			string command = "dotnet";
			string aruments = "run >> populatelog.txt";

			//Process config
			var processInfo = new ProcessStartInfo
			{
				FileName = command,
				Arguments = aruments,
				UseShellExecute = false, //This is needed because outrput is not going to be on console.
				CreateNoWindow = true, //Prevents terminal window to popup
			};
			//Start a new configured process
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] App Manager: Closing current application. Starting a new one in 5 seconds...");
			Task.Delay(5000).Wait(); //5 sec delay to make it safer.
			Process.Start(processInfo);
			//End the current process
			Environment.Exit(0);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] App Manager: Failed to Restart the server: " + ex.Message + ". Closing application.");
			Environment.Exit(0);
		}
	}
}