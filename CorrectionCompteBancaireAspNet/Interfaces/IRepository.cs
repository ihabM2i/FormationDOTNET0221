using System;
using System.Collections.Generic;
using System.Text;

namespace CorrectionCompteBancaireAspNet.Interfaces
{
    public interface IRepository<T>
    {
        T Create(T element);
        T FindElementById(int id);
        T Update(T element);
        List<T> FindAll();
    }
}
