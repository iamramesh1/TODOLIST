using TODOLIST.Models;

namespace TODOLIST.Services
{
    public class TodoRepository
    {
        private readonly List<TodoItem> _todoItems = new List<TodoItem>();
        private int _nextId = 1;

        public TodoRepository()
        {

            for (int i = 1; i <= 10; i++)
            {
                _todoItems.Add(new TodoItem
                {
                    Id = _nextId++,
                    Title = $"Todo Item {i}",
                    Description = $"Description for Todo Item {i}",
                    IsCompleted = i % 2 == 0
                });
            }
        }

        public IEnumerable<TodoItem> GetAll(int pageNumber, int pageSize)
        {
            return _todoItems.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public TodoItem GetById(int id) => _todoItems.FirstOrDefault(item => item.Id == id);

        public TodoItem Create(TodoItem item)
        {
            item.Id = _nextId++;
            _todoItems.Add(item);
            return item;
        }

        public bool Update(int id, TodoItem item)
        {
            var index = _todoItems.FindIndex(i => i.Id == id);
            if (index == -1) return false;
            item.Id = id;



            _todoItems[index] = item;
            return true;
        }

        public bool Delete(int id)
        {
            var index = _todoItems.FindIndex(i => i.Id == id);
            if (index == -1) return false;
            _todoItems.RemoveAt(index);
            return true;
        }

        public int Count() => _todoItems.Count;
    }
}
