using System.Collections.ObjectModel;
using System.Linq;

namespace ComivoyagerNext.ViewModels
{
    class MainPageViewModel
    {
        public delegate void PathChangedEvent(object sender, PathEventInfo e);

        public readonly ref struct PathEventInfo {
            public DotModel[] Path { get; init; }
        };

        public ObservableCollection<DotModel> Dots { get; } = new();

        public event PathChangedEvent? OnPathChanged;

        public void AddDot(double x, double y)
        {
            Dots.Add(new DotModel(Dots.Count, x, y));

            OnPathChanged?.Invoke(this, new PathEventInfo { Path = Dots.ToArray() });
        }

        public void ClearDots()
        {
            Dots.Clear();
        }
    }
}
