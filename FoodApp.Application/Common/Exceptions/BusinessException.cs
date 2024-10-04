using FoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Common.Exceptions
{
    public class BusinessException:Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public BusinessException(ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
