namespace WestcoastAPI.ViewModels.Students;

    public class StudentPostViewModel : PersonViewModel
    {
        public IList<Guid> Courses { get; set; } = new List<Guid>();
    }
