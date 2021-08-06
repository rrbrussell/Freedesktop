// C# Support Library for Freedesktop.org specifications
// Copyright (c) 2021 Robert R. Russell
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of version 2.1 of the GNU Lesser General
// Public License as published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of version 2 of the GNU Library
// General Public License along with this program; if not, write to
// the Free Software Foundation, Inc., 51 Franklin Street,
// Fifth Floor, Boston, MA  02110-1301, USA.
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
			Load(reader);
		}

		/// <summary>
		/// Loads new data into this instance
		/// </summary>
		/// <param name="reader">The data to load.</param>
		public void Load(XmlReader reader)
		{
			if (reader.ReadState != ReadState.Initial)
			{
				throw new InvalidOperationException(message: "Info does not process entire trees.");
			}
			reader.ReadToDescendant("metadata");
			while (reader.ReadState == ReadState.Interactive)
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						if (!reader.Name.Equals("metadata"))
						{
							throw new ApplicationException(message: "Info received non-metadata XML element");
						}
						string temp = reader.ReadOuterXml();
						if (temp.Equals(string.Empty))
						{
							Console.WriteLine("Received an empty metadata element.");
						}
						else
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
