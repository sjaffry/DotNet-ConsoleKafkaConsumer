using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Confluent.Kafka.Serialization;
using ConsoleConsumerKafka;
using System.Configuration;
using Newtonsoft.Json;

namespace Confluent.Kafka.Examples.SimpleConsumer

{

    public class Program

    {
        public static void Main()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var broker = appSettings["kafkaBroker"];
            var topic = appSettings["kafkaTopic"];

            string brokerList = broker;
            var topics = new List<string>() { topic };

            var config = new Dictionary<string, object>
            {
                { "group.id", "simple-csharp-consumer" },
                { "bootstrap.servers", brokerList }
            };

            using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topics.First(), 0, 0) });

                while (true)
                {
                    Message<Null, string> msg;
                    if (consumer.Consume(out msg, TimeSpan.FromSeconds(1)))
                    {
                        WriteToDb(msg.Topic, msg.Partition, msg.Offset.Value, msg.Value);                        
                    }
                }
            }                     
        }

        public static void WriteToDb(string topic, int partition, long offset, string message)
        {
            using (var db = new KafkaMessageDbContext())
            {

                var kafkaMessage = new KafkaMessage
                {                    
                    Topic = topic,
                    Offset = offset,
                    Partition = partition,
                    Message = message
                };

                db.KafkaMessages.Add(kafkaMessage);
                db.SaveChanges();
            }
        }
    }

}

