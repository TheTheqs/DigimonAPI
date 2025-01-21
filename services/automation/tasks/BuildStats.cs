namespace DigimonAPI.services;
public static class StatsBuilder //this class will populate de StatsTable
{
	public static int[][] GetStats(int size, int totalPoints, int minValue, int maxValue)
	{
		var results = new List<int[]>();
		GenerateRecursive(new int[size], 0, totalPoints, minValue, maxValue, results);
		return results.ToArray();
	}

	public static void GenerateRecursive(int[] array, int index, int remainingPoints, int minValue, int maxValue, List<int[]> results)
	{
		if (index == array.Length)
		{
			if (remainingPoints == 0)
				results.Add((int[])array.Clone());
			return;
		}

		for (int value = minValue; value <= maxValue; value++)
		{
			if (remainingPoints - value >= 0)
			{
				array[index] = value;
				GenerateRecursive(array, index + 1, remainingPoints - value, minValue, maxValue, results);
			}
		}
	}
}