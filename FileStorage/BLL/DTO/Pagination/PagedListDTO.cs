using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO.Pagination
{
    public class PagedListDTO<T>
	{
		public int CurrentPage { get; private set; }
		public int TotalPages { get; private set; }
		public int PageSize { get; private set; }
		public int TotalCount { get; private set; }

		public bool HasPrevious;
		public bool HasNext;

		public List<T> Data { get; set; }
	}
}
