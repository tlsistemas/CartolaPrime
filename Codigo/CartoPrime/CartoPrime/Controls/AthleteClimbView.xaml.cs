using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CartoPrime.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AthleteClimbView : ContentView
	{
		#region Propertes
		private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = newValue as Element;
			if (element == null)
				return;
			element.Parent = (Element)bindable;
		}
		public static readonly BindableProperty PositionProperty =
							   BindableProperty.Create(nameof(Position),
							   typeof(string),
							   typeof(AthleteClimbView),
							   null,
							   propertyChanged: OnPropertyChanged);
		public string Position
		{
			get { return (string)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}




		public static readonly BindableProperty HeightAtProperty =
					   BindableProperty.Create(nameof(HeightAt),
					   typeof(int),
					   typeof(AthleteClimbView),
					   null,
					   propertyChanged: OnPropertyChanged);
		public int HeightAt
		{
			get { return (int)GetValue(HeightAtProperty); }
			set { SetValue(HeightAtProperty, value); }
		}


		public static readonly BindableProperty WidthAtProperty =
			   BindableProperty.Create(nameof(WidthAt),
			   typeof(int),
			   typeof(AthleteClimbView),
			   null,
			   propertyChanged: OnPropertyChanged);
		public int WidthAt
		{
			get { return (int)GetValue(WidthAtProperty); }
			set { SetValue(WidthAtProperty, value); }
		}
		#endregion
		public AthleteClimbView ()
		{
			InitializeComponent ();
		}
	}
}