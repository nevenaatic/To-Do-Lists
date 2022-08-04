namespace ToDoCore
{
    public class ToDoList
    {
        public int Position { get; set; }
        public Guid Id { get; set; }
        public DateTime? ReminderDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsReminded { get; set; }
        public List<ToDoListItem> Items { get; set; } = new();

        public string Owner { get; set; } = string.Empty;

        public ToDoList() { }

        public ToDoList(Guid guid, DateTime reminderDate, string title, List<ToDoListItem> items)
        {
            this.Id = guid;
            ReminderDate = reminderDate;
            Title = title;
            this.Items = items;
        }
        public void Update(ToDoList list)
        {
            ReminderDate = list.ReminderDate;
            Title = list.Title;
        }
        public void UpdateItemPosition(ToDoListItem item, int position)
        {
            if (item.Position < position)
            {
                Items.Where(i => i.Position > item.Position && i.Position <= position).ToList().ForEach(i => i.Position--);
            }
            else
            {
                Items.Where(i => i.Position >= position && i.Position < item.Position).ToList().ForEach(i => i.Position++);
            }
            item.Position = position;
        }

        public void UpdatePositionsAfterDelete(ToDoListItem item)
        {
            Items.Where(i => i.Position > item.Position).ToList().ForEach(i => i.Position--);
        }

    }
}
