﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Common.Wrapper
{
    public class ResponseWrapper<T>
    {
        public bool IsSuccessful { get; set; }
        public List<string> Messages { get; set; }
        public T Data { get; set; }

        public ResponseWrapper<T> Success(T data, string message = null)
        {
            IsSuccessful = true;
            Messages = [message];
            Data = data;

            return this;
        }

        public ResponseWrapper<T> Failed(string message)
        {
            IsSuccessful = false;
            Messages = [message];

            return this;
        }
    }
}
