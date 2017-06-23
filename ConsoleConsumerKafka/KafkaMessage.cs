using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConsumerKafka
{
    class KafkaMessage
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public long Offset { get; set; }
        public int Partition { get; set; }
        public string Message { get; set; }
    }
}
