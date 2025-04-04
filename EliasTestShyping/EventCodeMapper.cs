using EliasTestShyping.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliasTestShyping
{
    public class EventCodeMapper
    {
        public string GetEventCode(OrderStatusDto orderStatus)
        {
            var commentDict = orderStatus.Comments?.ToDictionary(c => c.Name, c => c.Comment) ?? new Dictionary<string, string>();
            commentDict.TryGetValue("deliveredDataTag", out var deliveredTag);
            commentDict.TryGetValue("loadedDataTag", out var loadedTag);

            return orderStatus.StatusCode switch
            {
                "loaded" when loadedTag == "HeavyObject" => "2009",
                "loaded" when orderStatus.SequenceNumber == "1" => "2004",
                "loaded" when orderStatus.SequenceNumber == "2" => "2008",

                "inProgress" when orderStatus.SequenceNumber is "1" or "2" => "2000",

                "delivered" when deliveredTag == "DelayedDelivery" => "3001",
                "delivered" when deliveredTag == "Other" => "7000",
                "delivered" when orderStatus.SequenceNumber == "1" => "2003",
                "delivered" when orderStatus.SequenceNumber == "2" => "3000",

                "notDelivered" when deliveredTag == "NoAccess" => "6003",
                "notDelivered" when deliveredTag == "Other" => "7000",
                "notDelivered" when orderStatus.SequenceNumber is "1" or "2" => "5006",

                "notLoaded" when loadedTag == "NoAccess" => "6003",
                "notLoaded" when loadedTag == "Other" => "7000",
                "notLoaded" when orderStatus.SequenceNumber is "1" or "2" => "5006",

                _ => null
            };
        }
    }
}
