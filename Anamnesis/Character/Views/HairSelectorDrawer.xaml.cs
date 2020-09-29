﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace Anamnesis.Character.Views
{
	using System.Windows.Controls;
	using Anamnesis.GameData;
	using Anamnesis.Memory;
	using Anamnesis.Services;
	using PropertyChanged;

	/// <summary>
	/// Interaction logic for HairSelector.xaml.
	/// </summary>
	[AddINotifyPropertyChangedInterface]
	[SuppressPropertyChangedWarnings]
	public partial class HairSelectorDrawer : UserControl, IDrawer
	{
		private readonly Appearance.Genders gender;
		private readonly Appearance.Tribes tribe;

		private byte selected;
		private ICharaMakeCustomize? selectedItem;

		public HairSelectorDrawer(Appearance.Genders gender, Appearance.Tribes tribe, byte value)
		{
			this.InitializeComponent();

			this.gender = gender;
			this.tribe = tribe;

			this.ContentArea.DataContext = this;
			this.List.ItemsSource = GameDataService.CharacterMakeCustomize?.GetHair(tribe, gender);

			this.Selected = value;
		}

		public delegate void SelectorEvent(byte value);

		public event DrawerEvent? Close;
		public event SelectorEvent? SelectionChanged;

		public byte Selected
		{
			get
			{
				return this.selected;
			}

			set
			{
				this.selected = value;

				if (!this.IsLoaded)
					return;

				this.SelectedItem = GameDataService.CharacterMakeCustomize?.GetHair(this.tribe, this.gender, value);
				this.SelectionChanged?.Invoke(this.selected);
			}
		}

		public ICharaMakeCustomize? SelectedItem
		{
			get
			{
				return this.selectedItem;
			}

			set
			{
				if (this.selectedItem == value)
					return;

				this.selectedItem = value;

				if (value == null)
					return;

				this.Selected = value.FeatureId;
			}
		}
	}
}
