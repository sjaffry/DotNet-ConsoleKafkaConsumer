using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConsumerKafka
{
    class KafkaMessageDbContext: DbContext
    {
        public KafkaMessageDbContext() : base("name=MsgDbContext")
        {
        }
        public DbSet<KafkaMessage> KafkaMessages { get; set; }
    }
}
