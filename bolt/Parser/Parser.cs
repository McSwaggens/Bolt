using System;
using System.Collections.Generic;

namespace bolt
{
	public class Parser
	{
		/*
		 * DUMB COMPILER
		 * DUMB CODE
		 * BEWARE
		 */

		public static void Parse(Token[] _tokens, Settings settings) {
			
			Token[][] TokenLines = SeperateLines (_tokens);

			foreach (Token[] tokens in TokenLines) {
				if 	(	tokens[0] is Keyword 	&& ((Keyword)tokens[0]).keyword == EnumKeyword.SET
					&& 	tokens[1] is Word 		&& settings.settings.ContainsKey((string)((Word)tokens[1]).raw)
					)
					{
						string setting 	= (string)tokens[1].raw;
						object val		= settings.settings[setting];
						
						if (val is string)
						{
							if (tokens[2] is bolt.String)
							{
								settings.settings[setting] = ((String)tokens[2]).raw;
							}
							else
							{
								ThrowError($"Expected type string for setting {(string)tokens[1].raw}");
							}
						}
						else if (val is bool)
						{
							if (tokens[2] is bolt.Boolean)
							{
								settings.settings[setting] = ((Boolean)tokens[2]).raw;
							}
							else
							{
								ThrowError($"Expected type boolean for setting {(string)tokens[1].raw}");
							}
						}
						else if (val is int)
						{
							if (tokens[2] is bolt.Integer)
							{
								settings.settings[setting] = ((Integer)tokens[2]).raw;
							}
							else
							{
								ThrowError($"Expected type integer for setting {(string)tokens[1].raw}");
							}
						}
					}
			}
		}
		
		private static void ThrowError(string error)
		{
			//TODO: Show exception
			throw new Exception(error);
		}

		private static void PrintSettings(Dictionary<string, object> settings) {
			foreach (KeyValuePair<string, object> pair in settings) {
				Console.WriteLine (pair.Key + "\t" + pair.Value);
			}
		}

		private static Token[][] SeperateLines(Token[] tokens) {
			List<Token[]> tokenLines = new List<Token[]> ();
			List<Token> currentTokenLine = new List<Token> ();
			foreach (Token token in tokens) {
				if (token is Operator && ((Operator)token).type == OperatorType.SemiColon) {
					tokenLines.Add (currentTokenLine.ToArray ());
				}
				else
					currentTokenLine.Add (token);
			}
			return tokenLines.ToArray ();
		}
	}
}

