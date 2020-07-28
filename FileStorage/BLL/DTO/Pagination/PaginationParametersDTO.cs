using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO.Pagination
{
    public class PaginationParametersDTO
    {
        [Required] public int PageNumber { get; set; }

        [Required] public int PageSize { get; set; }
    }
}
