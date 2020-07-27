using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.Rd.EDA.BusinessLayer.OperationResults
{
    public class OperationResult
    {
        public bool Success { get; set; }

        public Exception Exception { get; set; }

        public string Message { get; set; }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }
    }
}