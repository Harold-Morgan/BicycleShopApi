using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{

    //Репозиторий очень условный, общий для всех, поддерживающий только асинхронные методы и никак не оптимизирующий (а в вызовах даже ухудшающий) EF-контекст
    //Сделан лишь для попытки более строго отделить слой бизнес логики от DAL
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity;
        Task<List<T>> ListAsync<T>() where T : BaseEntity;
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
    }
}
