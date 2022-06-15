using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.Interfaces
{
    public interface IRepository<TElement> where TElement:class
    {
        void Add(TElement element);
        void AddMany(IEnumerable<TElement> elements);
        void Update(TElement element);
        void Remove(TElement element);
        void RemoveMany(IEnumerable<TElement> elements);
        void Attach(TElement element);
        TElement Get(int id);
    }
}
