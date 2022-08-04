using System.Text.Json.Serialization;

namespace ToDoCore
{ 
    public class ToDoListItem
    {
        public int Position { get; set; }
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
        [JsonIgnore]
        public Guid ToDoListId { get; set; }
        [JsonIgnore]
        public ToDoList ToDoList { get; set; } = new();

        public ToDoListItem() { }

        public ToDoListItem(Guid id, string text, bool isChecked) {
            this.Id = id;
            this.Text = text;
            this.IsChecked = isChecked;
        }

        public void Update(ToDoListItem item)
        {
            Text = item.Text;
            IsChecked = item.IsChecked;
        }
    }
}
