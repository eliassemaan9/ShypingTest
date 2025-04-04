using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliasTestShyping.DTO
{
    public class OrderStatusDto
    {
        public string StatusCode { get; set; }
        public string SequenceNumber { get; set; }
        public List<CommentDto> Comments { get; set; } = new();
    }
}
