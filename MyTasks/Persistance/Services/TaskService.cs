using Microsoft.EntityFrameworkCore;
using MyTasks.Core.Models.Domains;
using Task = MyTasks.Core.Models.Domains.Task;

namespace MyTasks.Persistance.Services
{
    // Jeśli nasz aplikacja się rozrasta to logikę biznesową zamykamy właśnie w takich serwisach.
    // W Repozytoriach mamy operacje na bazie danych.
    // W UnitOfWork mamy pola z repozytoriami i funkcję do zapisywania zmian w bazie danych
    // Ta funkcja jest osobno, ponieważ jeśli chcielibyśmy wykonywać kilka różnych operacji na bazie danych
    // To nie zawsze trzeba zapisywać zmiany po każdej z nich. Czasami wystarczy tylko na końcu.
    // W serwisach natomiast piszemy logikę biznesową. Możemy dodać tu pole z UnitOfWork, żeby móc 
    // wykonywać operacje na bazie danych też tutaj. 
    // W każdym razie, implementacja nowych funkcjonalności naszej aplikacji odbywa się tutaj i w innych serwisach.

    // W kontrolerach natomiast ma być mało operacji: walidacja danych, wywołanie serwisów, stworzenie VM i zwrócenie widoku

    public class TaskService
    {
        private readonly UnitOfWork _unitOfWork;
        public TaskService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
        {
            return _unitOfWork.Task.Get(userId, isExecuted, categoryId, title);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Task.GetCategories();
        }

        public Task Get(int id, string userId)
        {
            return _unitOfWork.Task.Get(id, userId);
        }

        public void Add(Task task)
        {
            _unitOfWork.Task.Add(task);
            _unitOfWork.Complete();
        }

        public void Update(Task task)
        {
            _unitOfWork.Task.Update(task);
            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.Task.Delete(id, userId);
            _unitOfWork.Complete();
        }

        public void Finish(int id, string userId)
        {
            // Inna logika biznesowa. 
            _unitOfWork.Task.Finish(id, userId);
            
            // wysłanie emaila o ukończeniu zadania
            // ....


            _unitOfWork.Complete();
        }
    }
}
