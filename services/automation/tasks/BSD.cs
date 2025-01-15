using DigimonAPI.Services;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System.Xml.Linq;

namespace DigimonAPI.services;
//this class will build the description to the Special Move Table
public static class BSD // Stands for Build Special Move Description
{
	public static async Task<bool> GenerateSkillDescription(int sMoveId)
	{
		try
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Special Move Description Builder: Generating description for the id {sMoveId}");
			SpecialMove? relatedSM = await SDB.GetSpecialMoveById(sMoveId);
			if (relatedSM == null || relatedSM.Digimons == null || relatedSM.Name == null)
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Special Move Description Builder: Related Special Move is invalid. Id = {sMoveId}. ");
				return false;
			}

			string? description = await OpenAIService.GetSkillDescription(relatedSM.Name);
			if (description == null)
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Special Move Description Builder: Null description generated = {sMoveId}. ");
				return false;
			}
			relatedSM.Description = description;
			SpecialMove? updatedSm = await SDB.UpdateSpecialMoveDescription(relatedSM);
			if(updatedSm != null)
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Special Move Description Builder: Description was succefully generated:\n{updatedSm.ToString()}");
				return true;
			}
			throw new Exception("Something went wrong!!");
		}
		catch(Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Special Move Description Builder: ID = {sMoveId}, " + ex.Message);
			return false;
		}
	}
}