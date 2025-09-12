using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Tracker.Common.Models
{
    [ExcludeFromCodeCoverage]
    public class ErrorResultModel
    {
        public bool Result { get; set; }
        public string? Message { get; set; }
        public int? ErrorCode { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class FormulaValidationResponse
    {
        public string? FormulaValue { get; set; }
        public string? Result { get; set; }
        public string? Error { get; set; }
    }
}
