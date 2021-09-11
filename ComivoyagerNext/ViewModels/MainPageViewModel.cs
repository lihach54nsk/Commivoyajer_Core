using System.Collections.ObjectModel;

namespace ComivoyagerNext.ViewModels
{
    class MainPageViewModel
    {
        public ObservableCollection<DotModel> Dots { get; } = new ObservableCollection<DotModel>();

        public void AddDot(double x, double y)
        {
            Dots.Add(new DotModel(Dots.Count, x, y));
        }

        public void ClearDots()
        {
            Dots.Clear();
        }
    }
}
