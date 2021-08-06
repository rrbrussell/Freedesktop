using System;
using System.Xml;
using System.Collections.Generic;

namespace XBEL
{
	/// <summary>
	/// Stores program specific information for a bookmark.
	/// </summary>
	public class Info
	{
		/// <summary>
		/// Store the Metadata items for the current bookmark element. These are stored as a string containing raw XML.
		/// This is done to preserve the contents for application specific manipulation.
		/// </summary>
		public List<string> Metadata = new(5);
		
		/// <summary>
		/// Is true if there is no metadata.
		/// </summary>
		public bool Empty { get => this.Metadata.Count == 0; }

		/// <summary>
		/// Load the information from an XMLReader
		/// </summary>
		/// <param name="reader">The source of the data.</param>
		public Info(XmlReader reader)
		{
			if (reader.ReadState != ReadState.Initial)
			{
				throw new InvalidOperationException("Info does not process entire trees.");
			}
			reader.ReadToDescendant("metadata");
			while (reader.ReadState == ReadState.Interactive)
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						if(!reader.Name.Equals("metadata"))
						{
							throw new ApplicationException(message: "Info received non-metadata XML element");
						}
						string temp = reader.ReadOuterXml();
						if (temp.Equals(string.Empty))
						{
							Console.WriteLine("Received an empty metadata element.");
						} else
						{
							this.Metadata.Add(temp);
						}
						break;
					default:
						break;
				}
			}
		}

		/// <summary>
		/// Provide a default constructor.
		/// </summary>
		public Info()
		{

		}
	}
}
