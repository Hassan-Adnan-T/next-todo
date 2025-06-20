using Google.Cloud.Firestore;
using TodoApi.Models;

namespace TodoApi.Services{

    public class FirebaseService{
        private readonly FirestoreDb _db;

        public FirebaseService(IConfiguration configuration){
            _db = FirestoreDb.Create("todo-f7383");
        }

        public async Task<List<Todo>> GetAllTodosAsync(){
            var collection = _db.Collection("todos");
            var snapshot = await collection.GetSnapshotAsync();
            
            var todos = new List<Todo>();
            foreach (var document in snapshot.Documents)
            {
                var todo = document.ConvertTo<Todo>();
                todo.Id = document.Id;
                todos.Add(todo);
            }
            
            return todos.OrderByDescending(t => t.CreatedAt).ToList();
        }

        public async Task<Todo> GetTodoByIdAsync(string id){
            var document = _db.Collection("todos").Document(id);
            var snapshot = await document.GetSnapshotAsync();
            
            if (!snapshot.Exists)
                throw new KeyNotFoundException($"Todo with id {id} not found");
                
            var todo = snapshot.ConvertTo<Todo>();
            todo.Id = snapshot.Id;
            return todo;
        }

        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
            todo.CreatedAt = DateTime.UtcNow;
            var collection = _db.Collection("todos");
            var documentReference = await collection.AddAsync(todo);
            
            todo.Id = documentReference.Id;
            return todo;
        }

        public async Task<Todo> UpdateTodoAsync(string id, Todo todo)
        {
            todo.UpdatedAt = DateTime.UtcNow;
            var document = _db.Collection("todos").Document(id);
            await document.SetAsync(todo);
            
            todo.Id = id;
            return todo;
        }

        public async Task DeleteTodoAsync(string id)
        {
            var document = _db.Collection("todos").Document(id);
            await document.DeleteAsync();
        }
    }
}