﻿using FoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Common.DTOs
{
    public class ResultDTO<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ErrorCode ErrorCode { get; set; }

        public static ResultDTO<T> Sucess<T>(T data, string message = "")
        {
            return new ResultDTO<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                ErrorCode = ErrorCode.NoError,
            };
        }

        public static ResultDTO<T> Faliure(ErrorCode errorCode, string message)
        {
            return new ResultDTO<T>
            {
                IsSuccess = false,
                Data = default,
                Message = message,
                ErrorCode = errorCode,
            };
        }
    }
}
