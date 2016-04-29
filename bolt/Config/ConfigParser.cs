using System;
using System.Collections.Generic;

namespace bolt
{
	public class ConfigParser
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
					
				}
			}

			return null;
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

