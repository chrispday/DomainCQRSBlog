using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ReadModel.Repository
{
	public interface IRepository<T>
	{
		void Save(T item);
		T Get(Guid id);
		IEnumerable<T> Get();
		void Delete(Guid id);
	}
}
