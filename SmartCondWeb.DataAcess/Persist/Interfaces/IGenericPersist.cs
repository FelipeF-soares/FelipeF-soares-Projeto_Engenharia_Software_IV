using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist.Interfaces;

public interface IGenericPersist<T> where T : class
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool SaveChange();
}
