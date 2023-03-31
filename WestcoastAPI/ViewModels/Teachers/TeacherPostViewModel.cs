namespace WestcoastAPI.ViewModels.Teachers;

    public class TeacherPostViewModel : PersonViewModel
    {
        public IList<string> Skills { get; set; } = new List<string>();
    }
