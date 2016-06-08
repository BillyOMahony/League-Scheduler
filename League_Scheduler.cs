using System;
using System.IO;
using System.Collections.Generic;

namespace N_League{

	public class League{

		string name;
		List<string> teams = new List<string>();
		public Dictionary<string, string[]> rounds = new Dictionary<string, string[]>();

		public League(string name){
			this.name = name;
		}


		public void CreateCSV(){

			string path = AppDomain.CurrentDomain.BaseDirectory.ToString();
			Console.WriteLine(path);
			path = path + "League.csv";

			if(File.Exists(path)){
				File.Delete(path);
				Console.WriteLine("Old File deleted");
			}
			using (StreamWriter sw = File.CreateText(path)) 
	        {	
	        	int x = 1;
	            foreach (string[] round in rounds.Values){
					sw.WriteLine("\r\nRound " + x);
					foreach(string fixture in round){
						if(fixture != ""){
							sw.WriteLine(fixture);
						}
					}
					x++;
				}
	        }
		}

		/*
			SetTeams() allows user to state how many teams they want in their league and 
			allows them to rename these teams.
		*/
		public bool SetTeams(){

			int numTeams = 0;

			while(!(numTeams > 2)){

				Console.WriteLine("Enter number of teams, there must be at least 3 teams.");

				string inputNum = Console.ReadLine();
				
				Int32.TryParse(inputNum, out numTeams);

			}

			Console.WriteLine("Rename Teams or use default names? Type 'Y'/'N'");
			 
			string input = Console.ReadLine();

			//If user enters a value that is not "y" or "n" they will be required to re-enter
			//a value until input = "y" or "n"
			while(input.ToLower() != "y" && input.ToLower() != "n"){
				
				Console.WriteLine("Incorrect Input, type (Y)es or (N)o.");
				input = Console.ReadLine();

			}

			switch (input.ToLower()){
				//if "y" then the user will be asked to enter a name for each team
			    case "y":
			        for(int i = 0; i < numTeams; i++){
						Console.WriteLine(numTeams);
						Console.WriteLine("Enter name for team " + (i + 1));
						input = Console.ReadLine();
						teams.Add(input); 
					}
			        break;
			    
			    //if "n" then default team names will be used, e.g. Team1... Team5
			    case "n":
			       	Console.WriteLine("Default team names will be used");
					for(int i = 0; i < numTeams; i++){
						teams.Add("Team" +( i + 1)); 
					}
			        break;
			}

			//If there is an odd number of Teams the team "Bye" is added, Teams playing this "Bye"
			//do not show in the fixture list.
			if(teams.Count % 2 != 0){
				teams.Add("Bye");
			}

			Console.WriteLine("Home and Away fixtures? Type 'Y'/'N'");

			bool twoLegs = false;
			input = Console.ReadLine();

			while(input.ToLower() != "y" && input.ToLower() != "n"){
				
				Console.WriteLine("Incorrect Input, type (Y)es or (N)o.");
				input = Console.ReadLine();

			}

			switch (input.ToLower()){

			    case "y":
			        twoLegs = true;
			        break;
			    
			    case "n":
			       	twoLegs = false;
			        break;
			
			}

			return twoLegs;

		}


		public void ViewTeams(){
			
			Console.WriteLine("\nTeams in " + this.name);	

			foreach(string team in teams){
				Console.WriteLine(team);
			}

		}

		
		public void CreateFixtures(bool twoLegs){

			string fixture = "";
			string movingTeam = "";
			int numTeams = teams.Count;

			//if a team matches against bye, that team des not play this round.
			if(numTeams % 2 != 0){
				teams.Add("bye");
			}

			// numRounds = number of matchdays in half season
			int numRounds = numTeams;
			int fixturesPerRound;

			if(numTeams % 2 == 0){
				numRounds --;
				fixturesPerRound = numTeams/2;
			}else{
				fixturesPerRound = (numTeams - 1)/2;
			}

			int multiplier = 1;
			if(twoLegs == true){
				multiplier = 2;
			}

			for(int y = 1; y <= numRounds * multiplier; y++){

				rounds.Add(Convert.ToString("Round" + y), new string[fixturesPerRound]);

				// get the fixtures of each round
				int z = numRounds;
				for(int x = 0; x < (teams.Count)/2; x++){
					if(teams[x] != "Bye" && teams[z] != "Bye"){
						if(y <= numRounds){
							fixture = teams[x] + ",,VS,," + teams[z];
						}else{
							fixture = teams[z] + ",,VS,," + teams[x];
						}
						rounds["Round" + y][x] = fixture;

					}
					z --;
				}

				//	fixtures = fixtures + "\r\n";

				movingTeam = teams[1];
				teams.RemoveAt(1);
				teams.Add(movingTeam);					

			}

		}


		public void ViewFixtures(){
			//x is round number
			int x = 1;
			foreach (string[] round in rounds.Values){
				Console.WriteLine("\r\nRound " + x);
				foreach(string fixture in round){
					if(fixture != ""){
						Console.WriteLine(fixture);
					}
				}
				x++;
			}
		}
	}


	public class HasMain{

		public static void Main(){

			League league = new League("league");
			bool twoLegs = league.SetTeams();
			
			league.ViewTeams();
			league.CreateFixtures(twoLegs);
			league.ViewFixtures();

			Console.ReadKey();

			league.CreateCSV();

		}
	}
}