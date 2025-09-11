using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Tracker.Core.ServiceModel
{
    [ExcludeFromCodeCoverage]
    public class EntityServiceModel
    {       
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public long? RestoredBy { get; set; }
        public DateTime? RestoredAt { get; set; }
    }
}
