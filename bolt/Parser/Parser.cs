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

		public static Dictionary<string, object> Parse(Token[] tokens, Dictionary<string, object> possibleSettings) {
			
			Token[][] TokenLines = SeperateLines (tokens);

			foreach (Token[] tokenLine in TokenLines) {
				if (tokenLine [0] is Word && tokenLine [1] is Operator && ((Operator)tokenLine [1]).type == OperatorType.Equals && tokenLine [2] is ValueTokenType) {
					//(SETTING) = (VALUE)
					if (possibleSettings [((Word)tokenLine [0]).raw.ToString ()] != null)
						possibleSettings [((Word)tokenLine [0]).raw.ToString ()] = tokenLine [2].raw;
					else {
						Logger.LogError ("Unknown setting: \"" + ((Word)tokenLine [0]).raw.ToString () + "\" in config \"~/.bolt\"");
						Settings.LOAD_FAILED = true;
					}
				}
			}
			return possibleSettings;
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

