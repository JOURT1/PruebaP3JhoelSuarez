using PruebaP3JhoelSuarez.ViewModel;

namespace PruebaP3JhoelSuarez.View;

public partial class APIJsuarez : ContentPage
{
	public APIJsuarez()
	{
		InitializeComponent();
        BindingContext = new APIJsuarezViewModel(App.Database);
    }
}