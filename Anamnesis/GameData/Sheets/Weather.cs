﻿// © Anamnesis.
// Licensed under the MIT license.

namespace Anamnesis.GameData.Sheets
{
	using System.Windows.Media;
	using Lumina.Data;
	using Lumina.Excel;

	[Sheet("Weather", 47129921u)]
	public class Weather : ExcelRow
	{
		public string Name { get; private set; } = string.Empty;
		public string Description { get; private set; } = string.Empty;
		public ushort WeatherId => (ushort)this.RowId;

		public ImageSource? Icon { get; private set; }

		public override void PopulateData(RowParser parser, Lumina.GameData gameData, Language language)
		{
			base.PopulateData(parser, gameData, language);
			this.Icon = parser.ReadImageReference<int>(0);
			this.Name = parser.ReadString(1) ?? $"Weather #{this.RowId}";
			this.Description = parser.ReadString(2) ?? string.Empty;
		}
	}
}
