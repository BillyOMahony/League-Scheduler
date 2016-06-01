using System;
using System.Collections.Generic;

namespace N_League_Scheduler{

	public class Tournament{

		public static int NumTeams(){

			int numTeams = 0;

			Console.WriteLine("Enter number of teams:");
			
			string input = Console.ReadLine();

			try{
				numTeams = Convert.ToInt32(input);
			}catch{
				Console.WriteLine("Invalid Input");
				NumTeams();
			}

			//Should be more than two teams in a league
			if(numTeams <= 2){
				Console.WriteLine("Too few teams");
				NumTeams();
			}

			return numTeams;

		}


	
		public static List<string> TeamsList(){

			List<string> teams = new List<string>();

			int numTeams = NumTeams();

			Console.WriteLine("Rename Teams? Y/N");
			string input;

			input = Console.ReadLine();

			Console.WriteLine(input);

			while(input.ToLower() != "y" && input.ToLower() != "n"){
				Console.WriteLine(input);
				string thing = Console.ReadLine();
				input = thing;
			}

			if(input.ToLower() == "y"){
				for(int i = 0; i < numTeams; i++){
					Console.WriteLine(numTeams);
					Console.WriteLine("Enter name for team " + i);
					input = Console.ReadLine();
					teams.Add(input); 
				}

			}else if(input.ToLower() == "n"){
				Console.WriteLine("Default team names will be used");
				for(int i = 0; i < numTeams; i++){
					teams.Add("Team" +( i + 1)); 
				}

			}else{
				Console.WriteLine("Error, invalid input.");
				TeamsList();
			}

			return teams;
		}

		public static void GetFixtures(List<string> teams){
			
			string fixtures = ""; // Change this at some point to an array, or just write straight to file

			int numTeams = teams.Count;
			int numFixtures = 0;

			int count = numTeams -1;

			string tempString;

			while(count != 0){

				numFixtures	= numFixtures + count;

				count --;
			}

			if(numTeams % 2 == 0){

				Console.WriteLine(numFixtures);
				for(int y = 1; y < teams.Count; y++){
					// get the fixtures of each round
					fixtures = fixtures + "Round " + y + "\n";
					int z = numTeams - 1;
					for(int x = 0; x < teams.Count/2; x++){

						fixtures = fixtures + teams[x] + " VS " + teams[z] + "\n";
						z--;
					}

					tempString = teams[1];
					teams.RemoveAt(1);
					teams.Add(tempString);					

				}			

			}else{

				//If there is an odd number of teams, one team will not play a game per matchday.
				teams.Add("Bye");
				Console.WriteLine(teams.Count);
				
				for(int y = 1; y <= teams.Count; y++){
					// get the fixtures of each round
					fixtures = fixtures + "Round " + y + "\n";
					int z = numTeams;
					for(int x = 0; x < (teams.Count)/2; x++){
						if(teams[x] != "Bye" && teams[z] != "Bye"){
							fixtures = fixtures + teams[x] + " VS " + teams[z] + "\n";	
						}
						z --;

					}

					tempString = teams[1];
					teams.RemoveAt(1);
					teams.Add(tempString);					

				}

			}

			Console.WriteLine(fixtures);

		}

		public static void Main(){

			List<string> teams = TeamsList();

			foreach(string team in teams){
				Console.WriteLine(team);
			}

			GetFixtures(teams);

			Console.ReadKey();

		}

	}

}
