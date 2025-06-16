using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class WebApiResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T? Value { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
