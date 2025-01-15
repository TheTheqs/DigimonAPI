using Microsoft.Extensions.Hosting;
namespace DigimonAPI.services;
//This class can hold any kind of automatic task. It is suposed to be generic.
public class AutoTask : IHostedService, IDisposable //Stands for Hosted Service
{
	private Timer? timer;
	private readonly TimeSpan interval = TimeSpan.FromSeconds(15); //Period between every excecution. This can be edited
	private bool isRunning = false; //Conflit controler
	//The int below is temporary. It wil be used only at the DB population tasks.
	private int cindex =  0;
	private bool updatedIndex = false;

	public Task StartAsync(CancellationToken cancellationToken)
	{
		timer = new Timer(ExecuteTask, null, TimeSpan.Zero, interval); //Start timer. TimeSpan.Zero makes an execution at the Server Start.
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		timer?.Change(Timeout.Infinite, 0); //Stop Timer
		return Task.CompletedTask;
	}

	public void Dispose()
	{
		timer?.Dispose(); //Release timer memory usage
	}

	private async void ExecuteTask(object? state)
	{
		if (isRunning) return; //Conflit protection

		try
		{
			isRunning = true;
			timer?.Change(Timeout.Infinite, 0); //This will prevents the timer to keep going while the task is running. It is facultative
			Console.WriteLine($"[{ DateTime.Now:yyyy - MM - dd HH: mm: ss}][Automation] Auto Task: Starting Hosted Service.");
			await CurrentTask(); //The actual function to be called
			Console.WriteLine($"[{DateTime.Now:yyyy - MM - dd HH: mm: ss}][Automation] Auto Task: Ending Hosted Service.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy - MM - dd HH: mm: ss}][Automation] Auto Task: Hosted Service Error: {ex.Message}"); // Execution Log
		}
		finally
		{
			isRunning = false;
			timer?.Change(interval, interval); //Start the timer again. Necessary if you have choosen to stop it while task execution
		}
	}

	private async Task CurrentTask()
	{
		if(!updatedIndex)
		{
			cindex = await SDB.GetNextDescription();
			updatedIndex = true;
			Console.WriteLine($"[{DateTime.Now:yyyy - MM - dd HH: mm: ss}][Automation] Auto Task: Current index selected: {cindex}");

		};
		if(cindex > 2010)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy - MM - dd HH: mm: ss}][Automation] Auto Task: All data has been successfully added to the database. Please check the 'failed.txt' document for any missing entries. Closing the application...");
			Environment.Exit(0);
		}
		bool result = await BSD.GenerateSkillDescription(cindex); //Calling the current task
		if(result)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy - MM - dd HH: mm: ss}][Automation] Auto Task: Hosted Service task completed successfully.");
		}
		else
		{
			Console.WriteLine($"[{DateTime.Now:yyyy - MM - dd HH: mm: ss}][Automation] Auto Task: Hosted Service task failed. Restarting the server...");
			Environment.Exit(0);
		}
		cindex++;
	}
}