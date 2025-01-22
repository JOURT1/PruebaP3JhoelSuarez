using PruebaP3JhoelSuarez.ViewModel;

namespace PruebaP3JhoelSuarez.View;

public partial class BDDJsuarez : ContentPage
{
	public BDDJsuarez()
	{
		InitializeComponent();
        BindingContext = new BDDJsuarezViewModel(App.Database);
    }

}