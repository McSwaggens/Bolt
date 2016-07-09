using System;
using System.Collections.Generic;

namespace bolt
{
	public class Parser
	{
		
		public static void Parse(string text, Settings settings)
		{
			Token[] tokens = Lexer.GenerateTokens(text);
			Parse(tokens, settings);
		}
		
		public static void Parse(Token[] _tokens, Settings settings) {
			
			Token[][] TokenLines = SeperateLines (_tokens);

			foreach (Token[] tokens in TokenLines) {
				if (tokens [0] is Keyword && ((Keyword)tokens [0]).keyword == EnumKeyword.SET
				     && tokens [1] is Word && settings.HasSetting ((string)((Word)tokens [1]).raw)) {
					string setting = (string)tokens [1].raw;
					object val = settings [setting].value;
						
					if (val is string) {
						if (tokens [2] is bolt.String) {
							settings [setting].value = ((String)tokens [2]).raw;
						} else {
							Notification.Push ($"Expected type string for setting {(string)tokens[1].raw}", NotificationType.ERROR);
						}
					} else if (val is bool) {
						if (tokens [2] is bolt.Boolean) {
							settings [setting].value = ((Boolean)tokens [2]).raw;
						} else {
							Notification.Push ($"Expected type boolean for setting {(string)tokens[1].raw}", NotificationType.ERROR);
						}
					} else if (val is int) {
						if (tokens [2] is bolt.Integer) {
							settings [setting].value = ((Integer)tokens [2]).raw;
						} else {
							Notification.Push ($"Expected type integer for setting {(string)tokens[1].raw}", NotificationType.ERROR);
						}
					}
				}
				else if (tokens[0] is Word)
				{
					Word wCommand = (Word)tokens[0];
					if (DefCommands.Contains((string)wCommand.raw))
					{
						List<Token> cargs = new List<Token>(tokens);
						cargs.RemoveAt(0);
						
						Command command = DefCommands.Get((string)wCommand.raw);
						command.action(cargs.ToArray());
					}
				}
				else
				{
					Notification.Push("Unknown command", NotificationType.ERROR);
				}
			}
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

