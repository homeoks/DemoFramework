using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Interface
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
