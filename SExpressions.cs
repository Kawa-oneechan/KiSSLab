using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Kawa.SExpressions
{
	public class Symbol
	{
		public string Value { get; private set; }
		public Symbol(string value)
		{
			Value = value;
		}
		public override string ToString()
		{
			return Value;
		}
		public static implicit operator string(Symbol s)
		{
			return s.Value;
		}
		public static implicit operator Symbol(string s)
		{
			return new Symbol(s);
		}
		public override bool Equals(object obj)
		{
			if (obj is string)
				return Value == (string)obj;
			if (obj is Symbol)
				return Value == ((Symbol)obj).Value;
			return base.Equals(obj);
		}
		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
	}

	public class SExpression
	{
		public string Original { get; private set; }
		public object Data { get; private set; }
		public SExpression() { }
		public SExpression(string str)
		{
			Original = str;
			Data = Parse(str);
		}
		public override string ToString()
		{
			return ToSexpression(Data);
		}
		public string ToSexpression()
		{
			return ToSexpression(Data);
		}
		public string ToSexpression(object thing)
		{
			if (thing is string)
			{
				if (((string)thing).Contains(' ') || ((string)thing).Contains('\t') || ((string)thing).Contains('\n'))
					return string.Format("\"{0}\"", thing);
				else
					return thing.ToString();
			}
			if (thing is List<object>)
			{
				return string.Format("({0})", string.Join(" ", ((List<object>)thing).Select(x => ToSexpression(x))));
			}
			return thing.ToString();
		}
		private enum States { TokenStart, ReadQuotedString, ReadStringOrNumber, Comment }
		private int lastInt;
		private object Parse(string str)
		{
			var state = States.TokenStart;
			var commentState = state;
			var tokens = new List<object>();
			var word = new StringBuilder();
			foreach (var ch in str)
			{
				switch (state)
				{
					case States.TokenStart:
						if (ch == '(' || ch == ')' || ch == '\'')
						{
							tokens.Add(ch);
						}
						else if (char.IsWhiteSpace(ch))
						{
							//just eat it.
						}
						else if (ch == '\"')
						{
							state = States.ReadQuotedString;
							word.Clear();
						}
						else if (ch == ';')
						{
							commentState = state;
							state = States.Comment;
							word.Clear();
						}
						else
						{
							state = States.ReadStringOrNumber;
							word.Clear();
							word.Append(ch);
						}
						break;
					case States.ReadQuotedString:
						if (ch == '\"')
						{
							tokens.Add(word.ToString());
							state = States.TokenStart;
						}
						else
						{
							word.Append(ch);
						}
						break;
					case States.ReadStringOrNumber:
						if (char.IsWhiteSpace(ch))
						{
							tokens.Add(SymbolOrNumber(word.ToString()));
							state = States.TokenStart;
						}
						else if (ch == ')')
						{
							tokens.Add(SymbolOrNumber(word.ToString()));
							tokens.Add(')');
							state = States.TokenStart;
						}
						else
						{
							word.Append(ch);
						}
						break;
					case States.Comment:
						if (ch == '\r' || ch == '\n')
						{
							state = commentState;
						}
						break;
				}
			}
			var i = 0;
			return TokensToArray(tokens, ref i);
		}

		private object SymbolOrNumber(string word)
		{
			var i = 0;
			var f = 0.0f;
			if (int.TryParse(word, out i))
			{
				lastInt = i;
				return i;
			}
			else if (float.TryParse(word, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out f))
				return f;
			else
				return new Symbol(word);
		}

		private object TokensToArray(List<object> tokens, ref int index)
		{
			var result = new List<object>();
			while (index < tokens.Count)
			{
				if (tokens[index] is char && (char)tokens[index] == '\'')
				{
					index++;
					var quote = new List<object>();
					quote.Add(new Symbol("quote"));
					if (tokens[index] is char && (char)tokens[index] == '(')
					{
						index++;
						quote.Add(TokensToArray(tokens, ref index));
					}
					else
						quote.Add(new List<object>() { tokens[index] });
					result.Add(quote);
				}
				else if (tokens[index] is char && (char)tokens[index] == '(')
				{
					index++;
					result.Add(TokensToArray(tokens, ref index));
				}
				else if (tokens[index] is char && (char)tokens[index] == ')')
				{
					return result;
				}
				else
				{
					result.Add(tokens[index]);
				}
				index++;
			}
			return result;
		}
	}

	public static class SExtensions
	{
		public static string Stringify(this List<object> list)
		{
			var ret = new StringBuilder();
			ret.Append('(');
			var spacer = "";
			foreach (var item in list)
			{
				ret.Append(spacer);
				spacer = " ";
				if (item is List<object>)
					ret.Append(Stringify((List<object>)item));
				else if (item is string)
					ret.Append("\"" + item.ToString() + "\"");
				else if (item is int || item is Symbol)
					ret.Append(item.ToString());
			}
			ret.Append(')');
			return ret.ToString();
		}
	}
}

