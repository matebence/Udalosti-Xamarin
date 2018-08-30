namespace Udalosti.UWP
{
    public sealed partial class UdalostiUWP
    {
        public UdalostiUWP()
        {
            this.InitializeComponent();
            LoadApplication(new Udalosti.App());
        }
    }
}