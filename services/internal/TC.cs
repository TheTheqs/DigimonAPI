//This class is for tests only. It will create .txt documents with Strings.
namespace DigimonAPI.services;
public static class TC // Stands for Txt Creator
{
	//Directory where the txt document will be created
	private static String filePath = @"C:\Users\Matheqs\Desktop\DotNetClass\Studio\DigimonAPI\WebApplication1\documentation\output\output.txt";
	private static String failedPath = @"C:\Users\Matheqs\Desktop\DotNetClass\Studio\DigimonAPI\WebApplication1\documentation\output\failed.txt";
	public static bool GenerateTxt(String content)
	{
		try
		{
			//Make sure the directory exist
			Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
			//Try to wright the content into the file
			File.WriteAllText(filePath, content);
			return true;
		} catch(Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] TXT CREATOR: " + ex.Message);
			return false;
		}
	}

	public static void GerenatePopulateFailure(int index)
	{
		try
		{
			//making sure the document is there
			Directory.CreateDirectory(Path.GetDirectoryName(failedPath)!);
			//adding the line
			File.AppendAllText(failedPath, index.ToString() + Environment.NewLine);
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] TXT CREATOR: index {index} was successfully registered.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] TXT CREATOR: " + ex.Message);
		}
	}
}