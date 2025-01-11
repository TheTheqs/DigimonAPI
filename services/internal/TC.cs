//This class is for tests only. It will create .txt documents with Strings.
public static class TC // Stands for Txt Creator
{
	//Directory where the txt document will be created
	private static String filePath = @"C:\Users\Matheqs\Desktop\DotNetClass\Studio\DigimonAPI\WebApplication1\documentation\output\output.txt";
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
			Console.WriteLine("[ERROR] TXT CREATOR: " + ex.Message);
			return false;
		}
	}
}